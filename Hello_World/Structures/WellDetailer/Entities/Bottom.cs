using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDetailing.WellDetailer.Entities;

namespace Structures.WellDetailer.Entities
{
    public class Bottom
    {
        public double Diameter { private set; get; }
        public double InsideDiameter { private set; get; }
        private double WallThickness;
        public double Thickness { private set; get; }
        public double EdgeLength { set; get; }
        public LeanConcrete LeanConcrete { get; private set; }
        private Point3d[] BottomInCut;

        public Bottom()
        {
        }
        public Bottom(double insideDiameter, double wallThickness, double thickness, double edgeLength)
        {
            InsideDiameter = insideDiameter;
            WallThickness = wallThickness;
            EdgeLength = edgeLength;
            Thickness = thickness;
            Diameter = InsideDiameter + 2 * (WallThickness + EdgeLength);
            BottomInCut = new Point3d[8];
        }

        public void DrawBottomBluePrint(Point3d bluePrintCenter) 
        { 
            if(EdgeLength > 1) { DrawEdgeBluePrint(bluePrintCenter); }
        }

        public void DrawBottomBluePrintAnnotations(Point3d bluePrintCenter)
        {
            //Nome: {Fundo [1]}
            if (EdgeLength > 1) { DrawEdgeBluePrintAnnotations(bluePrintCenter); }
        }
        private void DrawEdgeBluePrint(Point3d bluePrintCenter)
        {
            string layer = "2";
            Utilities.DrawingShapes.DrawCircle(bluePrintCenter, Diameter, layer);
        }
        private void DrawEdgeBluePrintAnnotations(Point3d bluePrintCenter)
        {
            DrawingShapes.AddAlignedDimension("1-50", new Point3d(bluePrintCenter.X - 0.5 * Diameter * Math.Cos(DrawingShapes.DegrresToRadians(15.0)), 
                bluePrintCenter.Y + 0.5 * Diameter * Math.Sin(DrawingShapes.DegrresToRadians(15.0)), 0), 
                new Point3d(bluePrintCenter.X - (0.5 * Diameter - EdgeLength) * Math.Cos(DrawingShapes.DegrresToRadians(15.0)), 
                bluePrintCenter.Y + (0.5 * Diameter - EdgeLength) * Math.Sin(DrawingShapes.DegrresToRadians(15.0)), 0));
        }
        public void DrawAACut(Point3d startPointOfBottomCut)
        {
            DoPointsInBottomCut(startPointOfBottomCut);
            DrawLinesBottomInCut();
            LeanConcrete = new LeanConcrete(Diameter);
            LeanConcrete.DrawLeanConcreteInCut(new Point3d(startPointOfBottomCut.X - EdgeLength, startPointOfBottomCut.Y - Thickness,0));
        }

        private void DoPointsInBottomCut(Point3d startPointOfBottomCut) 
        {
                BottomInCut[0] = startPointOfBottomCut;
                BottomInCut[1] = new Point3d(BottomInCut[0].X - EdgeLength, BottomInCut[0].Y, 0);
                BottomInCut[2] = new Point3d(BottomInCut[1].X, BottomInCut[1].Y - Thickness, 0);
                BottomInCut[3] = new Point3d(BottomInCut[2].X + Diameter, BottomInCut[2].Y, 0);
                BottomInCut[4] = new Point3d( BottomInCut[3].X, BottomInCut[0].Y, 0);
                BottomInCut[5] = new Point3d(BottomInCut[4].X - EdgeLength, BottomInCut[4].Y, 0);

                BottomInCut[6] = new Point3d(BottomInCut[5].X - WallThickness, BottomInCut[5].Y, 0);
                BottomInCut[7] = new Point3d(BottomInCut[0].X + WallThickness, BottomInCut[6].Y, 0);
        }
        private void DrawLinesBottomInCut() 
        {
            string layer = "4";
            for (int i = 0; i < 5; i++) { Utilities.DrawingShapes.DrawLine(BottomInCut[i],BottomInCut[i+1], layer); }
            Utilities.DrawingShapes.DrawLine(BottomInCut[6], BottomInCut[7], layer);
        }
        public bool HasEdge() 
        { 
            if(EdgeLength > 1) { return true; }
            else { return false; }
        }
        public void DrawAACutAnnotations()
        {
            Polyline thiknessLine = new Polyline();
            thiknessLine.AddVertexAt(0, new Point2d(BottomInCut[1].X, BottomInCut[1].Y), 0, 0, 0);
            thiknessLine.AddVertexAt(1, new Point2d(BottomInCut[2].X, BottomInCut[2].Y), 0, 0, 0);
            Utilities.DrawingShapes.AddQuotesInPolylines(thiknessLine, - 15.0, 0);
        }
    }
}
