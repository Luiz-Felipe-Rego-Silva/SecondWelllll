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
            ElemSuperior = elemSuperior;
            ElemInferior = elemInferior;
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
            startPoint = new Point3d(startPoint.X + ExtesionThickness, startPoint.Y - ExtraLength - ExtesionThickness, 0.0);
            DrawLeftSideCut(startPoint);
            DrawMiddle(startPoint);
            startPoint = new Point3d(startPoint.X + (Extension - 2 * ElemSuperior), startPoint.Y , 0.0);
            DrawRigthSideCut(startPoint);
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
                DrawingShapes.DrawLine(upStart, new Point3d(upStart.X - ElemInferior, upStart.Y, 0), layer);
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
    }
}

