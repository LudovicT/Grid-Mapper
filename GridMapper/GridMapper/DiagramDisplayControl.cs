using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dataweb.NShape;
using Dataweb.NShape.Advanced;
using Dataweb.NShape.GeneralShapes;
using System.IO;
using System.Reflection;
using GridMapper.NetworkRepository;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;


namespace GridMapper
{
	/// <summary>
	/// Diagram display user control. Handles the display of data. Uses NShape.
	/// </summary>
	public partial class DiagramDisplayControl : UserControl
	{
		Dictionary<String, CaptionedShapeBase> shapeDict = new Dictionary<String, CaptionedShapeBase>();
		private Dataweb.NShape.Diagram _NShapeDiagram;

		private delegate void UpdateNshapeDisplay<T>(object sender, T e);

		public DiagramDisplayControl()
		{
			InitializeComponent();

			_xmlStore.DirectoryName = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			_xmlStore.FileExtension = ".nspj";

			_NShapeProject.Name = "GridMapper";
			_NShapeProject.AddLibrary(typeof(Ellipse).Assembly, false);
			_NShapeProject.Create();

			_NShapeDiagram = new Diagram("diagram");
			_NShapeDiagram.Height = _NShapeDisplay.Height;
			_NShapeDiagram.Width = _NShapeDisplay.Width;
			Repository.OnRepositoryUpdated += UpdateNshapeDisplay1;



			_NShapeDisplay.Diagram = _NShapeDiagram;
		}

		private void _NShapeDisplay_Layout(object sender, LayoutEventArgs e)
		{
			_NShapeDiagram.Height = _NShapeDisplay.Height + 1000;
			_NShapeDiagram.Width = _NShapeDisplay.Width + 1000;
		}

		private void _NShapeDisplay_Load(object sender, EventArgs e)
		{

		}

		public void LoadNetworkInfo()
		{
			
		}

		#region update display content
		
		public void UpdateNshapeDisplay1(object sender, RepositoryUpdatedEventArg e)
		{
			Invoke(new UpdateNshapeDisplay<RepositoryUpdatedEventArg>(UpdateNshapeDisplay2), new object[] { sender, e });
		}

		public void UpdateNshapeDisplay2(object sender, RepositoryUpdatedEventArg e)
		{

			foreach (INetworkDictionaryItem item in e.ReadOnlyRepository)
			{
				byte[] b = item.IPAddress.GetAddressBytes();
				IPAddressV4 ip = new IPAddressV4();
				string intIP = string.Empty;
				if (BitConverter.IsLittleEndian)
				{
					ip.B0 = b[3];
					ip.B1 = b[2];
					ip.B2 = b[1];
					ip.B3 = b[0];
				}
				else
				{
					ip.B0 = b[0];
					ip.B1 = b[1];
					ip.B2 = b[2];
					ip.B3 = b[3];
				}
				intIP = ((uint)ip.Address).ToString(); ;

				string IPAddressToString = string.Empty;
				IPAddressToString = item.IPAddress.ToString();

				string macToString = string.Empty;
				if (item.MacAddress != null && item.MacAddress != PhysicalAddress.None)
				{
					macToString = ToMac(item.MacAddress.ToString());
				}

				string hostNameString = string.Empty;
				if (item.HostEntry != null)
				{
					hostNameString = item.HostEntry.HostName.ToString();
				}

				string portToString = string.Empty;
				//pour l'affichage des virgule
				if (item.Ports.Count > 0)
				{
					portToString += item.Ports[0].ToString();
					for (int i = 1; i < item.Ports.Count; i++)
						portToString += ", " + item.Ports[i].ToString();
				}

				//bool found = false;
				int x = 0;
				int y = 0;
				//List<IPAddressV4> HostDone = new List<IPAddressV4>();

				//foreach (INetworkDictionaryItem item2 in e.ReadOnlyRepository)
				//{
					x += 100;
					y += 100;

					DrawEngine(hostNameString, x, y, shapeDict);

					//HostDone.Add(ip);
				//}
			}
		}
		#endregion

		static string ToMac(string ToTransform)
		{
			ToTransform = ToTransform.ToUpperInvariant();
			var list = Enumerable
				.Range(0, ToTransform.Length / 2)
				.Select(i => ToTransform.Substring(i * 2, 2));
			return string.Join(":", list);
		}

		public void DrawEngine(string s, int x, int y, Dictionary<String, CaptionedShapeBase> dShape)
		{
			CircleBase shape = (CircleBase)_NShapeProject.ShapeTypes["Circle"].CreateInstance();
			shape.Diameter = 130;
			shape.X = x;
			shape.Y = y;
			shape.SetCaptionText(0, s);

			if (!dShape.ContainsKey(s))
			{
				_NShapeDiagram.Shapes.Add(shape);
				if (!dShape.ContainsKey(s))
					dShape.Add(s, shape);
			}

		}
	}
}
