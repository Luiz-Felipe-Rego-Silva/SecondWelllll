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
using System.Windows.Forms;

namespace Detailing
{
    class VerticalVarBar : VariableDistribuction
    {
        public VerticalVarBar(StandardDistribuction bar, double constantParts) : base(bar, constantParts)
        {
        }
        public double[] HookLengths;
        private double _mediumLength;
        private int _quantity = 1;
        private double _sumOfConstantDimensions = 0.0;
        public int BarDir;
        private bool isNegative = false;
        List<Line> barLines;
        private void AddDetailingToDrawing(Point3d point, Point3d VARTablePosition)
        {
            DocumentLock documentLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
            Polyline polyline = new Polyline { Layer = "5" };
            BarDir = 1;
            if (isNegative) BarDir = -1;
            DrawBarLine(point, polyline);
            AddDimensions(polyline);
            _sumOfConstantDimensions = HookLengths[0] + HookLengths[1] + HookLengths[2] + HookLengths[3];
            barLines = VariableDistribuction.OrderLines(barLines);
            _mediumLength = Math.Floor(DrawingShapes.CalculateLinesMediumLength(barLines));
            _quantity = barLines.Count();
            this.DrawingID = DrawingUtilities.DrawObject(polyline);
            //Desenhar título pela orientação e ponto médio
            //this.AddMark(DrawingShapes.Move(basePoint, -BarDir * 15, -_mediumLength / 2), false);
            VarTable VARTable = new VarTable(barLines, Id, Convert.ToInt32(_sumOfConstantDimensions), AmendmentLength);
            VARTable.GenerateTable(VARTablePosition);
            documentLock.Dispose();
        }
        private void DrawBarLine(Point3d basePoint, Polyline polyline)
        {
            double v0 = HookLengths[0];
            double v1 = HookLengths[1];
            double v2 = _mediumLength;
            double v3 = HookLengths[2];
            double v4 = HookLengths[3];

            polyline.AddVertexAt(0, DrawingShapes.Move(basePoint, BarDir * v1, -v0).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(1, DrawingShapes.Move(basePoint, BarDir * v1, 0).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(2, basePoint.Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(3, DrawingShapes.Move(basePoint, 0, -v2).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(4, DrawingShapes.Move(basePoint, BarDir * v3, -v2).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(5, DrawingShapes.Move(basePoint, BarDir * v3, -v2 + v4).Convert2d(new Plane()), 0, 0, 0);

            polyline.XData = this.ExportResultBuffer();
        }
        private void AddDimensions(Polyline polyline)
        {
            List<Point3d> pack = new List<Point3d>();
            for (int index = 0; index < 6; index++) pack.Add(polyline.GetPoint3dAt(index));
            string dimName = "FERRO 1 - 50";
            double offset = 20.0;
            if (BarDir == 1)
            {
                StandardDistribuction.AddDimension(dimName, pack[0], pack[1], offset, 0, GetDimensionStringY(0, pack));
                StandardDistribuction.AddDimension(dimName, pack[1], pack[2], 0, 0, GetDimensionStringX(1, pack));
                StandardDistribuction.AddDimension(dimName, pack[2], pack[3], offset, 0, "VAR"/*GetDimensionStringY(2, pack)*/);
                StandardDistribuction.AddDimension(dimName, pack[3], pack[4], 0, -offset, GetDimensionStringX(3, pack));
                StandardDistribuction.AddDimension(dimName, pack[4], pack[5], offset, 0, GetDimensionStringY(4, pack));
            }
            else
            {
                StandardDistribuction.AddDimension(dimName, pack[0], pack[1], 0, 0, GetDimensionStringY(0, pack));
                StandardDistribuction.AddDimension(dimName, pack[1], pack[2], 0, 0, GetDimensionStringX(1, pack));
                StandardDistribuction.AddDimension(dimName, pack[2], pack[3], 0, 0, "VAR"/*GetDimensionStringY(2, pack)*/);
                StandardDistribuction.AddDimension(dimName, pack[3], pack[4], 0, -offset, GetDimensionStringX(3, pack));
                StandardDistribuction.AddDimension(dimName, pack[4], pack[5], 0, 0, GetDimensionStringY(4, pack));
            }
        }
        private string GetDimensionStringX(int index, List<Point3d> list)
        {
            double value = Math.Round(list[index + 1].X - list[index].X);
            if (value < 0) value = -value;

            if (Math.Abs(value) < 0.1)
                return " ";
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }
        private string GetDimensionStringY(int index, List<Point3d> list)
        {
            double value = Math.Round(list[index + 1].Y - list[index].Y);
            if (value < 0) value = -value;
            if (Math.Abs(value) < 0.1)
                return " ";

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }
    }
}
