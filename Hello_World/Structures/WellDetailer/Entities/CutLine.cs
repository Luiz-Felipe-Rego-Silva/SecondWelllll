using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;

namespace WellDetailing.WellDetailer.Entities
{
    public class CutLine
    {
        private const double CutLineShiftness = 52.0;
        private const double HorizontalOfssetCutLine = 12.0;
        private const double VerticalOffsetCutLine = 2.0;
        private const double TriangleLength = 24.0;
        private const double TriangleHeigth = 8;
        private const double TextSpacing = 2.0;
        private Point3d[] CutLinePoints;
        public char CutLetter { get; private set; }

        public CutLine(char cutLetter)
        {
            CutLinePoints = new Point3d[12];
            CutLetter = cutLetter;
        }
        public void DrawCutLine(double length, Point3d firstCenter, Point3d secondCenter)
        {
            DoPointsAtLineCut(firstCenter, secondCenter, length);
            DrawLinesAtCutLine();
        }
        private void DoPointsAtLineCut(Point3d firstCenter, Point3d secondCenter, double externalRadius)
        {
            CutLinePoints[0] = new Point3d(firstCenter.X - externalRadius - CutLineShiftness - HorizontalOfssetCutLine - TriangleLength, firstCenter.Y, 0);
            CutLinePoints[1] = new Point3d(CutLinePoints[0].X + TriangleLength + HorizontalOfssetCutLine, CutLinePoints[0].Y, 0);
            CutLinePoints[2] = new Point3d(CutLinePoints[0].X, CutLinePoints[0].Y + VerticalOffsetCutLine, 0);
            CutLinePoints[3] = new Point3d(CutLinePoints[2].X + TriangleLength, CutLinePoints[2].Y, 0);
            CutLinePoints[4] = new Point3d(CutLinePoints[2].X + TriangleLength / 2.0, CutLinePoints[2].Y + TriangleHeigth, 0);

            DrawingShapes.MiddlePoint(firstCenter, secondCenter);
            CutLinePoints[5] = new Point3d(DrawingShapes.MiddlePoint(firstCenter, secondCenter).X, CutLinePoints[1].Y, 0);
            CutLinePoints[6] = new Point3d(DrawingShapes.MiddlePoint(firstCenter, secondCenter).X, secondCenter.Y, 0);
            CutLinePoints[7] = new Point3d(secondCenter.X + externalRadius + CutLineShiftness, secondCenter.Y, 0);
            CutLinePoints[8] = new Point3d(CutLinePoints[7].X + HorizontalOfssetCutLine + TriangleLength, CutLinePoints[7].Y, 0);
            CutLinePoints[9] = new Point3d(CutLinePoints[8].X, CutLinePoints[8].Y + VerticalOffsetCutLine, 0);
            CutLinePoints[10] = new Point3d(CutLinePoints[9].X - TriangleLength, CutLinePoints[9].Y, 0);
            CutLinePoints[11] = new Point3d(CutLinePoints[9].X - TriangleLength / 2.0, CutLinePoints[9].Y + TriangleHeigth, 0);
        }
        private void DrawLinesAtCutLine()
        {
            string layerSymbol = "2";
            string layerTrace = "1";
            string lineTypeTrace = "DASHDOT";
            int lineTypeTraceScale = 25;
            DrawingShapes.DrawLine(CutLinePoints[0], CutLinePoints[1], layerSymbol);
            DrawingShapes.DrawLine(CutLinePoints[2], CutLinePoints[3], layerSymbol);
            DrawingShapes.DrawLine(CutLinePoints[3], CutLinePoints[4], layerSymbol);
            DrawingShapes.DrawLine(CutLinePoints[4], CutLinePoints[2], layerSymbol);

            DrawingShapes.DrawLine(CutLinePoints[1], CutLinePoints[5], layerTrace, lineTypeTrace, lineTypeTraceScale);
            DrawingShapes.DrawLine(CutLinePoints[5], CutLinePoints[6], layerTrace, lineTypeTrace, lineTypeTraceScale);
            DrawingShapes.DrawLine(CutLinePoints[6], CutLinePoints[7], layerTrace, lineTypeTrace, lineTypeTraceScale);

            DrawingShapes.DrawLine(CutLinePoints[7], CutLinePoints[8], layerSymbol);
            DrawingShapes.DrawLine(CutLinePoints[9], CutLinePoints[10], layerSymbol);
            DrawingShapes.DrawLine(CutLinePoints[10], CutLinePoints[11], layerSymbol);
            DrawingShapes.DrawLine(CutLinePoints[11], CutLinePoints[9], layerSymbol);
        }
    }
}