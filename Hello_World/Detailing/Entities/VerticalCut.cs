using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;

namespace Detailing.Entities
{
    class VerticalCut
    {
        public double  ElemSuperior { get; set; }
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
            for(int index = 0; index < borders.Length; index++ ) 
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
            DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - (Extension -  2 *  ElemInferior) , 0), layer);
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

    }
}
