using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;

namespace Detailing.Entities
{
    class VerticalCut
    {
        public double ElemSuperior { get; set; }
        public double ElemInferior { get; set; }
        public double Extension { get; set; }
        public double ExtesionThickness { get; set; }
        public double ExtraLength { get; set; }
        private bool[] _configuration { get; set; } = new bool[4]; //0. Superior Interno, 1. Superior Externo, 2. Inferior Interno, 3. Inferior Externo


        public VerticalCut(double elemSuperior, double elemInferior, double extension)
        {
            ElemSuperior = elemSuperior;
            ElemInferior = elemInferior;
            Extension = extension;
        }
        public void AddBorder(bool[] borders)
        {
            for (int index = 0; index < borders.Length; index++)
            {
                _configuration[index] = borders[index];
            }
        }
        public void Draw(Point3d startPoint)
        {
            startPoint = new Point3d(startPoint.X + ExtraLength, startPoint.Y - ElemInferior, 0.0);
            DrawUpSideCut(startPoint);
            DrawMiddle(startPoint);
            startPoint = new Point3d(startPoint.X, startPoint.Y - (Extension - 2 * ElemInferior), 0.0);
            DrawDownSideCut(startPoint);
        }
        private void DrawDownSideCut(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y - ElemInferior, 0), new Point3d(startPoint.X + ExtesionThickness, startPoint.Y - ElemInferior, 0), layer);
            if (_configuration[2])
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X - ExtraLength, startPoint.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y - ElemInferior, 0), new Point3d(startPoint.X - ExtraLength, startPoint.Y - ElemInferior, 0), layer);
                DrawCutSymbol(new Point3d(startPoint.X - ExtraLength, startPoint.Y - ElemInferior, 0), new Point3d(startPoint.X - ExtraLength, startPoint.Y, 0));
            }
            else
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - ElemInferior, 0), layer);
            }

            Point3d rightStart = new Point3d(startPoint.X + ExtesionThickness, startPoint.Y + (ElemSuperior - ElemInferior), 0);
            if (_configuration[3])
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X + ExtraLength, rightStart.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(rightStart.X, rightStart.Y - ElemSuperior, 0), new Point3d(rightStart.X + ExtraLength, rightStart.Y - ElemSuperior, 0), layer);
                DrawCutSymbol(new Point3d(rightStart.X + ExtraLength, rightStart.Y - ElemSuperior, 0), new Point3d(rightStart.X + ExtraLength, rightStart.Y, 0));
            }
            else
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X, rightStart.Y - ElemSuperior, 0), layer);
            }

        }
        private void DrawUpSideCut(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y + ElemInferior, 0), new Point3d(startPoint.X + ExtesionThickness, startPoint.Y + ElemInferior, 0), layer);
            if (_configuration[0])
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X - ExtraLength, startPoint.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y + ElemInferior, 0), new Point3d(startPoint.X - ExtraLength, startPoint.Y + ElemInferior, 0), layer);
                DrawCutSymbol(new Point3d(startPoint.X - ExtraLength, startPoint.Y, 0), new Point3d(startPoint.X - ExtraLength, startPoint.Y + ElemInferior, 0));
            }
            else
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y + ElemInferior, 0), layer);
            }
            Point3d rightStart = new Point3d(startPoint.X + ExtesionThickness, startPoint.Y - (ElemSuperior - ElemInferior), 0);
            if (_configuration[1])
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X + ExtraLength, rightStart.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(rightStart.X, rightStart.Y + ElemSuperior, 0), new Point3d(rightStart.X + ExtraLength, rightStart.Y + ElemSuperior, 0), layer);
                DrawCutSymbol(new Point3d(rightStart.X + ExtraLength, rightStart.Y, 0), new Point3d(rightStart.X + ExtraLength, rightStart.Y + ElemSuperior, 0));
            }
            else
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X, rightStart.Y + ElemSuperior, 0), layer);
            }
        }
        private void DrawMiddle(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - (Extension - 2 * ElemInferior), 0), layer);
            DrawingShapes.DrawLine(new Point3d(startPoint.X + ExtesionThickness, startPoint.Y - (ElemSuperior - ElemInferior), 0), new Point3d(startPoint.X + ExtesionThickness, startPoint.Y - (ElemSuperior - ElemInferior) - (Extension - 2 * ElemSuperior), 0), layer);
        }
        private void DrawCutSymbol(Point3d startPoint, Point3d endPoint)
        {
            double cutSymbolLength = 6.0;
            double cutSymbolHeigth = 16.0;
            double length = DrawingShapes.Distance(startPoint, endPoint) + 10.0;
            Point3d middle = DrawingShapes.MiddlePoint(startPoint, endPoint);
            string layer = "1";
            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y - 5.0, 0), new Point3d(startPoint.X, startPoint.Y + (length - cutSymbolLength - 10.0) / 2.0, 0), layer);
            DrawingShapes.DrawLine(new Point3d(endPoint.X, endPoint.Y + 5.0, 0), new Point3d(endPoint.X, endPoint.Y - (length - cutSymbolLength - 10.0) / 2.0, 0), layer);

            Point3d middlLeft = new Point3d(middle.X - cutSymbolHeigth / 2.0, middle.Y, 0);
            Point3d middleRigth = new Point3d(middle.X + cutSymbolHeigth / 2.0, middle.Y, 0);

            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y + (length - cutSymbolLength - 10.0) / 2.0, 0), middlLeft, layer);
            DrawingShapes.DrawLine(new Point3d(endPoint.X, endPoint.Y - (length - cutSymbolLength - 10.0) / 2.0, 0), middleRigth, layer);
            DrawingShapes.DrawLine(middlLeft, middleRigth, layer);
        }
        public void DrawBarInCut(Point3d startPoint, VariableDistribuction bar, double offset, double gaugeCompat)
        {
            Point3d basePoint;
            if (bar.isNegative) { basePoint = new Point3d(startPoint.X + ExtraLength + ExtesionThickness - offset - gaugeCompat / 10.0, startPoint.Y - offset, 0); }
            else { basePoint = new Point3d(startPoint.X + ExtraLength + offset + gaugeCompat / 10.0, startPoint.Y - offset, 0.0); }


            double v0 = bar.HookLengths[0];
            double v1 = bar.HookLengths[1];
            double v2 = Extension - 2 * offset;
            double v3 = bar.HookLengths[2];
            double v4 = bar.HookLengths[3];

            Polyline polyline = new Polyline() { Layer = "4" };
            polyline.AddVertexAt(0, DrawingShapes.Move(basePoint, bar.BarDir * v1, -v0).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(1, DrawingShapes.Move(basePoint, bar.BarDir * v1, 0).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(2, basePoint.Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(3, DrawingShapes.Move(basePoint, 0, -v2).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(4, DrawingShapes.Move(basePoint, bar.BarDir * v3, -v2).Convert2d(new Plane()), 0, 0, 0);
            polyline.AddVertexAt(5, DrawingShapes.Move(basePoint, bar.BarDir * v3, -v2 + v4).Convert2d(new Plane()), 0, 0, 0);

            DrawingUtilities.AddToDrawing(polyline);
        }
        public void DrawBarSectioned(Point3d startPoint, VariableDistribuction bar, double cover)
        {
            if (bar.isNegative) { startPoint = new Point3d(startPoint.X + ExtraLength + ExtesionThickness - cover, startPoint.Y - cover, 0); }
            else { startPoint = new Point3d(startPoint.X + ExtraLength + cover, startPoint.Y - cover, 0.0); }

            string layer = "4";
            double length = Extension - 2 * cover;
            double effectiveSpacing = (length - bar.Gauge / 10.0) / (bar.Quantity - 1);
            for (int index = 0; index < bar.Quantity; index++) { DrawingShapes.DrawCircle(new Point3d(startPoint.X - 0.5 * bar.BarDir * bar.Gauge / 10.0, startPoint.Y - 0.5 * bar.Gauge / 10.0 - index * effectiveSpacing, 0), bar.Gauge / 10.0, layer); }
            //string dimStyleName = "DIST 1-50";
            //string content = $"N{bar.Id}";
            //DrawingShapes.AddAlignedDimension(dimStyleName, new Point3d(startPoint.X + cover, startPoint.Y, 0), new Point3d(startPoint.X + cover, startPoint.Y - length, 0), content, 20.0, 0);
        }

    }
}
