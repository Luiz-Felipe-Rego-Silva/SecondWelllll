using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;


namespace Detailing.Entities
{
    class HorizontalCut
    {
        public double ElemSuperior { get; set; }
        public double ElemInferior { get; set; }
        public double Extension { get; set; }
        public double ExtesionThickness { get; set; }
        public double ExtraLength { get; set; }
        private bool[] _configuration { get; set; } = new bool[4]; //0. Superior Interno, 1. Superior Externo, 2. Inferior Interno, 3. Inferior Externo

        public HorizontalCut(double elemSuperior, double elemInferior, double extension)
        {
            ElemSuperior = elemInferior;
            ElemInferior = elemSuperior;
            Extension = extension;
        }
        public void AddBorder(bool[] borders)
        {
            _configuration[0] = borders[2];
            _configuration[1] = borders[3];
            _configuration[2] = borders[1];
            _configuration[3] = borders[0];
        }
        public void Draw(Point3d startPoint)
        {
            startPoint = new Point3d(startPoint.X + ElemSuperior, startPoint.Y - ExtraLength - ExtesionThickness, 0.0);
            DrawTitle(new Point3d(startPoint.X + 10.0, startPoint.Y + ExtesionThickness + 35.0, 0.0));
            DrawLeftSideCut(startPoint);
            DrawMiddle(startPoint);
            startPoint = new Point3d(startPoint.X + (Extension - 2 * ElemSuperior), startPoint.Y, 0.0);
            DrawRigthSideCut(startPoint);
        }
        private void DrawTitle(Point3d startPoint)
        {
            DBText title = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = "CORTE A-A",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = startPoint
            };
            DrawingUtilities.AddToDrawing(title);
        }
        private void DrawRigthSideCut(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(new Point3d(startPoint.X + ElemSuperior, startPoint.Y, 0), new Point3d(startPoint.X + ElemSuperior, startPoint.Y + ExtesionThickness, 0), layer);
            if (_configuration[3])
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - ExtraLength, 0), layer);
                DrawingShapes.DrawLine(new Point3d(startPoint.X + ElemSuperior, startPoint.Y, 0), new Point3d(startPoint.X + ElemSuperior, startPoint.Y - ExtraLength, 0), layer);
                DrawCutSymbol(new Point3d(startPoint.X, startPoint.Y - ExtraLength, 0), new Point3d(startPoint.X + ElemSuperior, startPoint.Y - ExtraLength, 0));
            }
            else
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X + ElemSuperior, startPoint.Y, 0), layer);
            }

            Point3d upStart = new Point3d(startPoint.X - (ElemInferior - ElemSuperior), startPoint.Y + ExtesionThickness, 0);
            if (_configuration[2])
            {
                DrawingShapes.DrawLine(upStart, new Point3d(upStart.X, upStart.Y + ExtraLength, 0), layer);
                DrawingShapes.DrawLine(new Point3d(upStart.X + ElemInferior, upStart.Y, 0), new Point3d(upStart.X + ElemInferior, upStart.Y + ExtraLength, 0), layer);
                DrawCutSymbol(new Point3d(upStart.X, upStart.Y + ExtraLength, 0), new Point3d(upStart.X + ElemInferior, upStart.Y + ExtraLength, 0));
            }
            else
            {
                DrawingShapes.DrawLine(upStart, new Point3d(upStart.X + ElemInferior, upStart.Y, 0), layer);
            }
        }
        private void DrawLeftSideCut(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(new Point3d(startPoint.X - ElemSuperior, startPoint.Y, 0), new Point3d(startPoint.X - ElemSuperior, startPoint.Y + ExtesionThickness, 0), layer);
            if (_configuration[0])
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - ExtraLength, 0), layer);
                DrawingShapes.DrawLine(new Point3d(startPoint.X - ElemSuperior, startPoint.Y, 0), new Point3d(startPoint.X - ElemSuperior, startPoint.Y - ExtraLength, 0), layer);
                DrawCutSymbol(new Point3d(startPoint.X - ElemSuperior, startPoint.Y - ExtraLength, 0), new Point3d(startPoint.X, startPoint.Y - ExtraLength, 0));
            }
            else
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X - ElemSuperior, startPoint.Y, 0), layer);
            }

            Point3d upStart = new Point3d(startPoint.X + (ElemInferior - ElemSuperior), startPoint.Y + ExtesionThickness, 0);
            if (_configuration[1])
            {
                DrawingShapes.DrawLine(upStart, new Point3d(upStart.X, upStart.Y + ExtraLength, 0), layer);
                DrawingShapes.DrawLine(new Point3d(upStart.X - ElemInferior, upStart.Y, 0), new Point3d(upStart.X - ElemInferior, upStart.Y + ExtraLength, 0), layer);
                DrawCutSymbol(new Point3d(upStart.X - ElemInferior, upStart.Y + ExtraLength, 0), new Point3d(upStart.X, upStart.Y + ExtraLength, 0));
            }
            else
            {
                DrawingShapes.DrawLine(upStart, new Point3d(upStart.X - ElemInferior, upStart.Y, 0), layer);
            }
        }
        private void DrawMiddle(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X + (Extension - 2 * ElemSuperior), startPoint.Y, 0), layer);
            DrawingShapes.DrawLine(new Point3d(startPoint.X + (ElemInferior - ElemSuperior), startPoint.Y + ExtesionThickness, 0), new Point3d(startPoint.X + (ElemInferior - ElemSuperior) + (Extension - 2 * ElemInferior), startPoint.Y + ExtesionThickness, 0), layer);
        }
        private void DrawCutSymbol(Point3d startPoint, Point3d endPoint)
        {
            double cutSymbolLength = 6.0;
            double cutSymbolHeigth = 16.0;

            double length = DrawingShapes.Distance(startPoint, endPoint) + 10.0;
            Point3d middle = DrawingShapes.MiddlePoint(startPoint, endPoint);
            string layer = "1";

            DrawingShapes.DrawLine(new Point3d(startPoint.X - 5.0, startPoint.Y, 0), new Point3d(startPoint.X + (length - cutSymbolLength - 10.0) / 2.0, startPoint.Y, 0), layer);
            DrawingShapes.DrawLine(new Point3d(endPoint.X + 5.0, endPoint.Y, 0), new Point3d(endPoint.X - (length - cutSymbolLength - 10.0) / 2.0, endPoint.Y, 0), layer);

            Point3d middleDown = new Point3d(middle.X, middle.Y - cutSymbolHeigth / 2.0, 0);
            Point3d middleUp = new Point3d(middle.X, middle.Y + cutSymbolHeigth / 2.0, 0);

            DrawingShapes.DrawLine(new Point3d(startPoint.X + (length - cutSymbolLength - 10.0) / 2.0, startPoint.Y, 0), middleDown, layer);
            DrawingShapes.DrawLine(new Point3d(endPoint.X - (length - cutSymbolLength - 10.0) / 2.0, endPoint.Y, 0), middleUp, layer);
            DrawingShapes.DrawLine(middleDown, middleUp, layer);
        }
        public void DrawBarInCut(Point3d startPoint, VariableDistribuction bar, double offset)
        {
            bar.BarDir = -1;
            if (bar.isNegative) bar.BarDir = 1;

            Point3d basePoint;
            if (bar.isNegative) { basePoint = new Point3d(startPoint.X + offset, startPoint.Y + offset - ExtraLength - ExtesionThickness, 0); }
            else { basePoint = new Point3d(startPoint.X + offset, startPoint.Y + ExtesionThickness - offset - ExtraLength - ExtesionThickness, 0.0); }


            double v0 = bar.HookLengths[0];
            double v1 = bar.HookLengths[1];
            double v2 = Extension - 2 * offset;
            double v3 = bar.HookLengths[2];
            double v4 = bar.HookLengths[3];

            Polyline polyline = new Polyline() { Layer = "4" };
            polyline.AddVertexAt(0, new Point2d(basePoint.X + v0, basePoint.Y + bar.BarDir * v1), 0, 0, 0);
            polyline.AddVertexAt(1, new Point2d(basePoint.X, basePoint.Y + bar.BarDir * v1), 0, 0, 0);
            polyline.AddVertexAt(2, new Point2d(basePoint.X, basePoint.Y), 0, 0, 0);
            polyline.AddVertexAt(3, new Point2d(basePoint.X + v2, basePoint.Y), 0, 0, 0);
            polyline.AddVertexAt(4, new Point2d(basePoint.X + v2, basePoint.Y + bar.BarDir * v3), 0, 0, 0);
            polyline.AddVertexAt(5, new Point2d(basePoint.X + v2 - v4, basePoint.Y + bar.BarDir * v3), 0, 0, 0);

            DrawingUtilities.AddToDrawing(polyline);
            DrawLeader(basePoint, bar.Id, offset, bar.isNegative);
        }
        public void DrawBarSectioned(Point3d startPoint, VariableDistribuction bar, double cover, double gaugeCompat)
        {
            if (bar.isNegative) { startPoint = new Point3d(startPoint.X + cover + gaugeCompat / 10.0, startPoint.Y + cover - ExtraLength - ExtesionThickness, 0); }
            else { startPoint = new Point3d(startPoint.X + cover + gaugeCompat / 10.0, startPoint.Y + ExtesionThickness - cover - ExtraLength - ExtesionThickness, 0.0); }

            string layer = "4";
            double length = Extension - 2 * cover - 2 * gaugeCompat / 10.0;
            double effectiveSpacing = (length - bar.Gauge / 10.0) / (bar.Quantity - 1);
            for (int index = 0; index < bar.Quantity; index++) { DrawingShapes.DrawCircle(new Point3d(startPoint.X + 0.5 * bar.Gauge / 10.0 + index * effectiveSpacing, startPoint.Y - 0.5 * bar.BarDir * bar.Gauge / 10.0 - bar.BarDir * gaugeCompat / 10.0, 0), bar.Gauge / 10.0, layer); }

            string dimStyleName = "DIST 1-50";
            string content = $"N{bar.Id}";

            if (bar.isNegative)
            {
                Point3d start = new Point3d(startPoint.X, startPoint.Y + (ExtesionThickness - 2 * cover), 0.0);
                DrawingShapes.AddAlignedDimension(dimStyleName,
                    new Point3d(start.X, start.Y - bar.BarDir * gaugeCompat / 10.0, 0),
                    new Point3d(start.X + length, start.Y + bar.BarDir * gaugeCompat / 10.0, 0),
                    content, 0.0,
                    ExtesionThickness);
            }
            else
            {
                Point3d start = new Point3d(startPoint.X, startPoint.Y - (ExtesionThickness - 2 * cover), 0);
                DrawingShapes.AddAlignedDimension(dimStyleName,
                    new Point3d(start.X, start.Y - bar.BarDir * gaugeCompat / 10.0, 0),
                    new Point3d(start.X + length, start.Y - bar.BarDir * gaugeCompat / 10.0, 0),
                    content, 0.0,
                    -ExtesionThickness - 10.0);
            }
        }
        private void DrawLeader(Point3d startPoint, int ID, double cover, bool isNegative)
        {
            if (isNegative)
            {
                Point3d initPoint = new Point3d(startPoint.X + 0.67 * (Extension - 2 * cover), startPoint.Y, 0);
                Leader leader = new Leader() { Layer = "1", HasArrowHead = true };
                leader.AppendVertex(initPoint);
                leader.AppendVertex(new Point3d(initPoint.X - 20.0, initPoint.Y - 45.0, 0));
                DrawingUtilities.DrawText(new Point3d(initPoint.X - 30.0, initPoint.Y - 55.0, 0), $"N{ID}", 0.0);
                DrawingUtilities.AddToDrawing(leader);
            }
            else
            {
                Point3d initPoint = new Point3d(startPoint.X + 0.33 * (Extension - 2 * cover), startPoint.Y, 0);
                Leader leader = new Leader() { Layer = "1", HasArrowHead = true };
                leader.AppendVertex(initPoint);
                leader.AppendVertex(new Point3d(initPoint.X + 20.0, initPoint.Y + 45.0, 0));
                DrawingUtilities.DrawText(new Point3d(initPoint.X + 30.0, initPoint.Y + 55.0, 0), $"N{ID}", 0.0);
                DrawingUtilities.AddToDrawing(leader);
            }

        }
    }

}

