using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detailing
{
    class HorizontalVarBar : VariableDistribuction
    {
        
        public double MediumLength;
        public List<Line> barLines;

        public HorizontalVarBar(StandardDistribuction bar, double constantParts) : base(bar, constantParts)
        {
        }
        public void SetAnchor(double[] hookLength)
        {
            HookLengths = hookLength;
        }
        public void UpdateMediumLength()
        {
            MediumLength = Math.Floor(DrawingShapes.CalculateLinesMediumLength(barLines));
        }
        public void AddDetailingToDrawing(Point3d point)
        {
            DocumentLock documentLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
            Polyline polyline = new Polyline { Layer = "5" };

            BarDir = -1;
            if (isNegative) BarDir = 1;
            DrawBarLine(point, polyline);
            documentLock.Dispose();
        }
        public void DrawBarLine(Point3d basePoint, Polyline polyline)
        {
            double v0 = HookLengths[0];
            double v1 = HookLengths[1];
            double v2 = MediumLength;
            double v3 = HookLengths[2];
            double v4 = HookLengths[3];

            polyline.AddVertexAt(0, new Point2d(basePoint.X + v0, basePoint.Y + BarDir * v1), 0, 0, 0);
            polyline.AddVertexAt(1, new Point2d(basePoint.X, basePoint.Y + BarDir * v1), 0, 0, 0);
            polyline.AddVertexAt(2, new Point2d(basePoint.X, basePoint.Y), 0, 0, 0);
            polyline.AddVertexAt(3, new Point2d(basePoint.X + v2, basePoint.Y), 0, 0, 0);
            polyline.AddVertexAt(4, new Point2d(basePoint.X + v2, basePoint.Y + BarDir * v3), 0, 0, 0);
            polyline.AddVertexAt(5, new Point2d(basePoint.X + v2 - v4, basePoint.Y + BarDir * v3), 0, 0, 0);
            AddDimensions(polyline);
            Point3d middle = DrawingShapes.MiddlePoint(polyline.GetPoint3dAt(2), polyline.GetPoint3dAt(3));
            PrintDescriptionText(new Point3d(middle.X, middle.Y - BarDir * 10.0, middle.Z), 0.0, GetDescriptionText());
            polyline.XData = this.ExportResultBuffer();
            this.DrawingID = DrawingUtilities.DrawObject(polyline);
        }
        private void AddDimensions(Polyline polyline)
        {
            List<Point3d> pack = new List<Point3d>();
            for (int i = 0; i < 6; i++) pack.Add(polyline.GetPoint3dAt(i));

            string dimName = "FERRO 1-50";
            double offset = 20.0;
            if (BarDir == 1)
            {
                if (Math.Abs(HookLengths[0]) > 0.5) StandardDistribuction.AddDimension(dimName, pack[0], pack[1], 0, 0, GetDimensionStringX(0, pack));
                StandardDistribuction.AddDimension(dimName, pack[1], pack[2], 0, 0, GetDimensionStringY(1, pack));
                StandardDistribuction.AddDimension(dimName, pack[2], pack[3], 0, 0, "VAR");
                StandardDistribuction.AddDimension(dimName, pack[3], pack[4], offset, 0, GetDimensionStringY(3, pack));
                if (Math.Abs(HookLengths[3]) > 0.5) StandardDistribuction.AddDimension(dimName, pack[4], pack[5], 0, 0, GetDimensionStringX(4, pack));
            }
            else
            {
                if (Math.Abs(HookLengths[0]) > 0.5) StandardDistribuction.AddDimension(dimName, pack[0], pack[1], 0, -offset, GetDimensionStringX(0, pack));
                StandardDistribuction.AddDimension(dimName, pack[1], pack[2], 0, 0, GetDimensionStringY(1, pack));
                StandardDistribuction.AddDimension(dimName, pack[2], pack[3], 0, -offset, "VAR");
                StandardDistribuction.AddDimension(dimName, pack[3], pack[4], offset, 0, GetDimensionStringY(3, pack));
                if (Math.Abs(HookLengths[3]) > 0.5) StandardDistribuction.AddDimension(dimName, pack[4], pack[5], 0, -offset, GetDimensionStringX(4, pack));
            }
        }
        
        private string GetDimensionStringX(int index, List<Point3d> list)
        {
            double value = Math.Round(list[index + 1].X - list[index].X);
            if (value < 0) value = -value;

            if (Math.Abs(value) < 0.5)
                return " ";
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }
        private string GetDimensionStringY(int index, List<Point3d> list)
        {
            double value = Math.Round(list[index + 1].Y - list[index].Y);
            if (value < 0) value = -value;

            if (Math.Abs(value) < 0.5)
                return " ";
            return Convert.ToString(value);
        }

    }
}
