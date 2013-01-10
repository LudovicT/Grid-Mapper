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


namespace GridMapper
{
	/// <summary>
	/// Diagram display user control. Handles the display of data. Uses NShape.
	/// </summary>
	public partial class DiagramDisplayControl : UserControl
	{
		Dictionary<String, CaptionedShapeBase> shapeDict = new Dictionary<String, CaptionedShapeBase>();
		private Dataweb.NShape.Diagram _NShapeDiagram;

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

		public void DrawEngine(string s, int x, int y, Dictionary<String, CaptionedShapeBase> dShape)
		{
			CircleBase shape = (CircleBase)_NShapeProject.ShapeTypes["Circle"].CreateInstance();
			shape.Diameter = 100;
			shape.X = x;
			shape.Y = y;
			shape.SetCaptionText(0, s);

			_NShapeDiagram.Shapes.Add(shape);
			if (!dShape.ContainsKey(s))
				dShape.Add(s, shape);
		}
	}
}
