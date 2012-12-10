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

namespace GridMapper
{
	public partial class GridWindow : Form
	{
		Option _startUpOption;
		Execution _exe;

		// définition du delegate qui sera utilisé
		private delegate void UpdateDataGrid<T>( object sender, T e );
		// **************** 

		public GridWindow(Option StartUpOptions)
		{
			_startUpOption = StartUpOptions;
			_exe = new Execution( StartUpOptions );

			InitializeComponent();
			InitializeDataGridView();
		}

		void InitializeDataGridView()
		{
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.RowHeadersVisible = false;

			dataGridView1.ColumnCount = 3;
			dataGridView1.Columns[0].Name = "IPAddress";
			dataGridView1.Columns[1].Name = "MacAddress";
			dataGridView1.Columns[2].Name = "HostName";

			PingSender.PingCompleted += UpdateDataGridView;
			
		}

		public void UpdateDataGridView( object sender, PingCompletedEventArgs e )
		{
			dataGridView1.Invoke( new UpdateDataGrid<PingCompletedEventArgs>( UpdateDataGridView2 ), new object[]{ sender, e } );
		}

		public void UpdateDataGridView2( object sender, PingCompletedEventArgs e )
		{
			string[] row = { e.PingReply.Address.ToString(), "" , "" };
			dataGridView1.Rows.Add( row );
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
            Environment.Exit(1);
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
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt";
            saveFileDialog1.Title = "Save a Map File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                fs.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt";
            saveFileDialog1.Title = "Save a Map File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                fs.Close();
            }
        }

        private void startScanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		private void fastScanToolStripMenuItem_Click( object sender, EventArgs e )
		{
			_exe.StartScan();
		}

		private void fastScanToolStripMenuItem_Click_1( object sender, EventArgs e )
		{
			dataGridView1.DataSource = null;
			dataGridView1.DataMember = null;
			_exe.StartScan();
		}
	}
}
