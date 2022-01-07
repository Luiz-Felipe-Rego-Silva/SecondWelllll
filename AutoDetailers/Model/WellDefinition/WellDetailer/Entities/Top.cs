using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using WellDetailing.WellDetailer.Entities;

namespace Structures.WellDetailer.Entities
{
    public class Top
    {
        public double Thickness { get; private set; }
        public double InternalDiameter { get; private set; }
        public double Passarela { get; private set; }
        public Hood Hood { get; set; }
        private Point3d[] TopInCut;
        private double LengthSide;
        public double WallThickness { private get; set; }
        public Terrain Terrain { get; private set; }

        public Top(double thickness, double internalDiameter, double passarela, Hood hood, double wallThickness)
        {
            Thickness = thickness;
            InternalDiameter = internalDiameter;
            Passarela = passarela;
            Hood = hood;
            WallThickness = wallThickness;
            LengthSide = (InternalDiameter - Hood.InternalShiftness) * 0.5 - Hood.HorizontalGap;
            TopInCut = new Point3d[16];
        }
        public Top()
        {
            TopInCut = new Point3d[16];
        }

        public void DrawTopBluePrint(Point3d TopCenterPoint)
        {
            //Doesn't anything to implement
        }

        public void DrawTopBluePrintAnnotations(Point3d TopCenterPoint)
        {
            //Doesn't anything to implement
        }

        public void DrawTopUpperView(Point3d TopCenterPoint)
        {
            Point3d hoodCenterPoint = new Point3d(TopCenterPoint.X + Hood.Position.X, TopCenterPoint.Y + Hood.Position.Y, 0);
            Hood.DrawHoodUpperView(hoodCenterPoint);
            Utilities.DrawingShapes.AddDiameterDimension(TopCenterPoint, InternalDiameter + 2 * WallThickness + 2 * Passarela, 60.0);
            
        }
        public void DrawTopUpperViewAnnotations(Point3d TopCenterPoint)
        {
            Point3d hoodCenterPoint = new Point3d(TopCenterPoint.X + Hood.Position.X, TopCenterPoint.Y + Hood.Position.Y, 0);
            Hood.DrawHoodUpperViewAnnotations(hoodCenterPoint);
        }

        public void DrawTopAACut(Point3d TopStartPoint)
        {
            DoPointsAtTopInCut(TopStartPoint);
            DrawLinesTopInCut();
        }
        private void DoPointsAtTopInCut(Point3d startPoint)
        {
            TopInCut[0] = startPoint;
            TopInCut[1] = new Point3d(TopInCut[0].X - Passarela, TopInCut[0].Y, 0);
            TopInCut[2] = new Point3d(TopInCut[1].X, TopInCut[1].Y + Thickness, 0);
            TopInCut[3] = new Point3d(TopInCut[2].X + LengthSide + Passarela + WallThickness, TopInCut[2].Y, 0);
            TopInCut[4] = new Point3d(TopInCut[3].X, TopInCut[3].Y - Hood.VerticalGap, 0);
            TopInCut[5] = new Point3d(TopInCut[4].X + Hood.HorizontalGap, TopInCut[4].Y, 0);
            TopInCut[6] = new Point3d(TopInCut[5].X, TopInCut[0].Y, 0);
            TopInCut[7] = new Point3d(TopInCut[0].X + WallThickness, TopInCut[0].Y, 0);

            TopInCut[8] = new Point3d(TopInCut[7].X + InternalDiameter + WallThickness, TopInCut[0].Y, 0);
            TopInCut[9] = new Point3d(TopInCut[8].X + Passarela, TopInCut[8].Y, 0);
            TopInCut[10] = new Point3d(TopInCut[9].X, TopInCut[9].Y + Thickness, 0);
            TopInCut[11] = new Point3d(TopInCut[10].X - Passarela - WallThickness - LengthSide, TopInCut[10].Y, 0);
            TopInCut[12] = new Point3d(TopInCut[11].X, TopInCut[11].Y - Hood.VerticalGap, 0);
            TopInCut[13] = new Point3d(TopInCut[12].X - Hood.HorizontalGap, TopInCut[12].Y, 0);
            TopInCut[14] = new Point3d(TopInCut[13].X, TopInCut[0].Y, 0);
            TopInCut[15] = new Point3d(TopInCut[8].X - WallThickness, TopInCut[0].Y, 0);
        }
        private void DrawLinesTopInCut()
        {
            try
            {
                string layer = "4";
                for (int i = 0; i < 7; i++) { Utilities.DrawingShapes.DrawLine(TopInCut[i], TopInCut[i + 1], layer); }
                for (int i = 8; i < 15; i++) { Utilities.DrawingShapes.DrawLine(TopInCut[i], TopInCut[i + 1], layer); }

                layer = "2";
                Utilities.DrawingShapes.DrawLine(TopInCut[3], TopInCut[11], layer);
                Utilities.DrawingShapes.DrawLine(TopInCut[5], TopInCut[13], layer);
                Utilities.DrawingShapes.DrawLine(TopInCut[6], TopInCut[14], layer);
            }
            catch (System.Exception e) { _ = e.Message; }
        }
        public void DrawTopAACutAnnotation(double edgeLength) 
        {
            Polyline thicknessLine = new Polyline();
            thicknessLine.AddVertexAt(0, new Point2d(TopInCut[1].X, TopInCut[1].Y), 0, 0, 0);
            thicknessLine.AddVertexAt(1, new Point2d(TopInCut[2].X, TopInCut[2].Y), 0, 0, 0);
            Utilities.DrawingShapes.AddQuotesInPolylines(thicknessLine, -15.0 - edgeLength, 0.0);

            if (Hood.VerticalGap > 5)
            {
                Polyline gapYLine = new Polyline();
                gapYLine.AddVertexAt(0, new Point2d(TopInCut[3].X, TopInCut[3].Y), 0, 0, 0);
                gapYLine.AddVertexAt(1, new Point2d(TopInCut[4].X, TopInCut[4].Y), 0, 0, 0);
                Utilities.DrawingShapes.AddQuotesInPolylines(gapYLine, -15.0 - LengthSide - WallThickness - Passarela, 0.0);
            }
        }
        private void DrawProjectionShiftnessLines()
        {

        }

    }
}
