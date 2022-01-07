using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;

namespace Model.Detailing.Entities.Cut
{
    static class CutInfo
    {
        public static void DrawHorizontalCutSign(Point3d centerPoint, double extension)
        {
            Polyline leftLine = new Polyline() { Layer = "1" };
            Point3d startPoint = new Point3d(centerPoint.X - 0.5 * extension, centerPoint.Y, 0.0);

            leftLine.AddVertexAt(0, new Point2d(startPoint.X + 20.0, startPoint.Y), 0, 0, 0);
            leftLine.AddVertexAt(1, new Point2d(startPoint.X - 20.0, startPoint.Y), 0, 0, 0);
            leftLine.AddVertexAt(2, new Point2d(startPoint.X - 20.0, startPoint.Y - 10.0), 0, 0, 0);

            DrawingUtilities.DrawText(new Point3d(startPoint.X - 20.0, startPoint.Y - 20.0, 0), "A", 0.0);

            Polyline rigthLine = new Polyline() { Layer = "1" };
            startPoint = new Point3d(startPoint.X + extension, startPoint.Y, 0);
            rigthLine.AddVertexAt(0, new Point2d(startPoint.X - 20.0, startPoint.Y), 0, 0, 0);
            rigthLine.AddVertexAt(1, new Point2d(startPoint.X + 20.0, startPoint.Y), 0, 0, 0);
            rigthLine.AddVertexAt(2, new Point2d(startPoint.X + 20.0, startPoint.Y - 10.0), 0, 0, 0);
            DrawingUtilities.AddToDrawing(rigthLine, leftLine);
            DrawingUtilities.DrawText(new Point3d(startPoint.X + 20.0, startPoint.Y - 20.0, 0), "A", 0.0);
        }
        public static void DrawVerticalCutSign(Point3d centerPoint, double extension)
        {
            Polyline supLine = new Polyline() { Layer = "1" };
            Point3d startPoint = new Point3d(centerPoint.X, centerPoint.Y + 0.5 * extension, 0.0);

            supLine.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y - 20.0), 0, 0, 0);
            supLine.AddVertexAt(1, new Point2d(startPoint.X, startPoint.Y + 20.0), 0, 0, 0);
            supLine.AddVertexAt(2, new Point2d(startPoint.X + 10.0, startPoint.Y + 20.0), 0, 0, 0);
            DrawingUtilities.DrawText(new Point3d(startPoint.X + 20.0, startPoint.Y + 20.0, 0), "B", 0.0);

            Polyline infLine = new Polyline() { Layer = "1" };
            startPoint = new Point3d(startPoint.X, startPoint.Y - extension, 0);
            infLine.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y + 20.0), 0, 0, 0);
            infLine.AddVertexAt(1, new Point2d(startPoint.X, startPoint.Y - 20.0), 0, 0, 0);
            infLine.AddVertexAt(2, new Point2d(startPoint.X + 10.0, startPoint.Y - 20.0), 0, 0, 0);
            DrawingUtilities.AddToDrawing(infLine, supLine);
            DrawingUtilities.DrawText(new Point3d(startPoint.X + 20.0, startPoint.Y - 20.0, 0), "B", 0.0);
        }
        public static void DrawCutSigns(Point3d centerPoint, double extension)
        {
            DrawVerticalCutSign(centerPoint, extension);
            DrawHorizontalCutSign(centerPoint, extension);
        }
    }
}
