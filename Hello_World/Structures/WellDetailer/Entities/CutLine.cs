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
        public char CutLetter{ get; private set; }

        public CutLine(char cutLetter)
        {
            CutLinePoints = new Point3d[10];
            CutLetter = cutLetter;
        }
         public void DrawCutLine(double length, Point3d Center)
         {
            DoPointsAtLineCut(Center, length);
            DrawLinesAtCutLine();
         }
         private void DoPointsAtLineCut(Point3d Center, double externalRadius)
         {
            CutLinePoints[0] = new Point3d(Center.X - externalRadius - CutLineShiftness - HorizontalOfssetCutLine - TriangleLength, Center.Y, 0);
            CutLinePoints[5] = new Point3d(Center.X + externalRadius + CutLineShiftness + HorizontalOfssetCutLine + TriangleLength, Center.Y, 0);
            
            CutLinePoints[1] = new Point3d(CutLinePoints[0].X + TriangleLength + HorizontalOfssetCutLine, CutLinePoints[0].Y, 0);
            CutLinePoints[6] = new Point3d(CutLinePoints[5].X - TriangleLength - HorizontalOfssetCutLine, CutLinePoints[0].Y, 0);
            
            CutLinePoints[2] = new Point3d(CutLinePoints[0].X, CutLinePoints[0].Y + VerticalOffsetCutLine, 0);
            CutLinePoints[7] = new Point3d(CutLinePoints[5].X, CutLinePoints[5].Y + VerticalOffsetCutLine, 0);
            
            CutLinePoints[3] = new Point3d(CutLinePoints[2].X + TriangleLength, CutLinePoints[2].Y, 0);
            CutLinePoints[8] = new Point3d(CutLinePoints[7].X - TriangleLength, CutLinePoints[7].Y, 0);
            
            CutLinePoints[4] = new Point3d(CutLinePoints[2].X + TriangleLength/2.0,CutLinePoints[2].Y + TriangleHeigth, 0);
            CutLinePoints[9] = new Point3d(CutLinePoints[7].X - TriangleLength/2.0,CutLinePoints[7].Y + TriangleHeigth, 0);
         }
         private void DrawLinesAtCutLine()
         {
             string layer = "2";
             DrawingShapes.DrawLine(CutLinePoints[0], CutLinePoints[1], layer);
             DrawingShapes.DrawLine(CutLinePoints[2], CutLinePoints[3], layer);
             DrawingShapes.DrawLine(CutLinePoints[3], CutLinePoints[4], layer);
             DrawingShapes.DrawLine(CutLinePoints[4], CutLinePoints[2], layer);
             DrawingShapes.DrawLine(CutLinePoints[5], CutLinePoints[6], layer);
             DrawingShapes.DrawLine(CutLinePoints[7], CutLinePoints[8], layer);
             DrawingShapes.DrawLine(CutLinePoints[8], CutLinePoints[9], layer);
             DrawingShapes.DrawLine(CutLinePoints[9], CutLinePoints[7], layer);
         }
    }
}