using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GridMapper.NetworkRepository;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;

namespace GridMapper
{
	public partial class GridWindow : Form
	{
		Option _startUpOption;
		Execution _exe;
        int OperationLeft = 0;

		// définition du delegate qui sera utilisé pour traiter les events
		private delegate void UpdateDataGrid<T>( object sender, T e );
		// **************** 

		public GridWindow(Option StartUpOptions)
		{
			_startUpOption = StartUpOptions;
			_exe = new Execution( StartUpOptions );
			_exe.TaskCompleted += ProgressChanged;
			//_exe.IsFinished += new EventHandler( FinishedExecution );
			InitializeComponent();
			InitializeDataGridView();
		}

		private void FinishedExecution( object sender, EventArgs e )
		{
			timer1.Stop();
		}

		private void ProgressChanged( object sender, TaskCompletedEventArgs e )
		{
			if ( e != null && OperationLeft == 0 )
			{
				FinishedExecution(this, null);
			}
			else if ( e!= null)
			{
				if ( OperationLeft >= e.TaskCompleted )
				{
					Interlocked.Add(ref OperationLeft, - e.TaskCompleted);
				}
				else throw new ConstraintException( " wrong number of operation " );
			}
		}

		void InitializeDataGridView()
		{
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.AllowUserToAddRows = false;

			dataGridView1.ColumnCount = 5;
			dataGridView1.Columns[0].DataPropertyName = "Id";
			dataGridView1.Columns[0].Name = "Id";
			dataGridView1.Columns[0].Visible = false;
			dataGridView1.Columns[1].DataPropertyName = "IPAddress";
			dataGridView1.Columns[1].Name = "IPAddress";
			dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
			dataGridView1.Columns[2].DataPropertyName = "MacAddress";
			dataGridView1.Columns[2].Name = "MacAddress";
			dataGridView1.Columns[3].DataPropertyName = "HostName";
			dataGridView1.Columns[3].Name = "HostName";
			dataGridView1.Columns[4].DataPropertyName = "Ports";
			dataGridView1.Columns[4].Name = "Ports";

			//PingSender.PingCompleted += UpdateDataGridView;
			//ARPSender.MacCompleted += UpdateDataGridView;
			Repository.OnRepositoryUpdated += UpdateDataGridView;
			
		}

		public void UpdateDataGridView( object sender, RepositoryUpdatedEventArg e )
		{
			dataGridView1.Invoke( new UpdateDataGrid<RepositoryUpdatedEventArg>( UpdateDataGridView2 ), new object[] { sender, e } );
		}

		public void UpdateDataGridView2( object sender, RepositoryUpdatedEventArg e )
		{
			dataGridView1.DataSource = null;
			dataGridView1.DataMember = null;
			foreach( INetworkDictionaryItem item in e.ReadOnlyRepository )
			{
				byte[] b = item.IPAddress.GetAddressBytes();
				IPAddressV4 ip = new IPAddressV4();
				ip.B0 = b[0];
				ip.B1 = b[1];
				ip.B2 = b[2];
				ip.B3 = b[3];
				string portToString = "";
				//pour l'affichage des virgule
				if( item.Ports.Count > 0 )
				{
					portToString += item.Ports[0].ToString();
					for( int i = 1 ; i < item.Ports.Count ; i++ )
						portToString += ", " + item.Ports[i].ToString();
				}

				if ( item.MacAddress != null && item.MacAddress != PhysicalAddress.None && item.HostEntry != null )
				{
                    string[] row = { ip.Address.ToString(), item.IPAddress.ToString(), ToMac(item.MacAddress.ToString()), item.HostEntry.HostName.ToString(), portToString };
					dataGridView1.Rows.Add( row );
				}
				else if( item.MacAddress != null && item.MacAddress != PhysicalAddress.None )
				{
                    string[] row = { ip.Address.ToString(), item.IPAddress.ToString(), ToMac(item.MacAddress.ToString()), "", portToString };
					dataGridView1.Rows.Add( row );
				}
				else if( item.HostEntry != null )
				{
					string[] row = { ip.Address.ToString(), item.IPAddress.ToString(), "", item.HostEntry.HostName.ToString(), portToString };
					dataGridView1.Rows.Add( row );
				}
				else
				{
					string[] row = { ip.Address.ToString(), item.IPAddress.ToString(), "", "", portToString };
					dataGridView1.Rows.Add( row );
				}
			}
		}
                
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void ipAddressControl1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt|Other XML File|*.*";
            openFileDialog1.Title = "Select a Map File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.Cursor = new Cursor(openFileDialog1.OpenFile());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // take a look
            //System.Diagnostics.Process.Start(@"C:\Windows");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt|Other XML File|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.Cursor = new Cursor(openFileDialog1.OpenFile());
            }
        }

        private void SaveScan_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the file
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt";
            saveFileDialog1.Title = "Save a Map File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the file via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

				if ( saveFileDialog1.FilterIndex == 0 || saveFileDialog1.FilterIndex == 1 )
				{
					_exe.SaveRepoXml(fs);
				}

                fs.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
			SaveScan_Click( sender, e );
        }

        private void startScanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		private void fastScanToolStripMenuItem_Click( object sender, EventArgs e )
		{
			OperationLeft = _startUpOption.IPToTestCount * _startUpOption.OperationCount;
			_exe.StartScan();
			timer1.Start();
		}

		private void fastScanToolStripMenuItem_Click_1( object sender, EventArgs e )
		{
			dataGridView1.DataSource = null;
			dataGridView1.DataMember = null;
			OperationLeft = _startUpOption.IPToTestCount * _startUpOption.OperationCount;
			_exe.StartScan();
			timer1.Start();
		}


		private void timer1_Tick( object sender, EventArgs e )
		{
			int i = 0;
			i = Convert.ToInt32( Math.Round( 100 - (double)OperationLeft / ( _startUpOption.IPToTestCount * _startUpOption.OperationCount ) * 100 ) );
			ProgressScan.Value = i;
		}

		private void GridWindow_FormClosed( object sender, FormClosedEventArgs e )
		{
			
		}

        private void advancedOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Option_window Option = new Option_window();
            Option.ShowDialog();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataMember = null;
			OperationLeft = _startUpOption.IPToTestCount * _startUpOption.OperationCount;
            _exe.StartScan();
			timer1.Start();
        }

        static string ToMac(string ToTransform)
        {
            string Transform = string.Empty;
            string Substring = string.Empty;
            int i = 0;
            int j = 0;

            while (i < ToTransform.Length / 2)
            {
                Substring = ToTransform.Substring(j, 2);
                Transform += Substring;
                ++i;
                j = j + 2;
                if (i <= 5)
                {
                    Transform += ":";
                }
            }
            return Transform;
        }
	}
}
