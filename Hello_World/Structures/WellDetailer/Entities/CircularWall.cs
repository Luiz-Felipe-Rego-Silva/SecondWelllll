using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using WellDetailing.WellDetailer.Entities;

namespace Structures.WellDetailer.Entities
{
    public class CircularWall
    {
        public double InternalDiameter { private set; get; }
        public double ExternalDiameter { private set; get; }
        public double Thickness { private set; get; }
        public double Heigth { private set; get; }
        private Point3d[] WallInCut;
        private const double MinimumProjectonLinesSpacing = 5.0;
        public CircularWall(double internalDiameter, double thickness, double heigth)
        {
            InternalDiameter = internalDiameter;
            Thickness = thickness;
            ExternalDiameter = InternalDiameter + 2 * Thickness;
            Heigth = heigth;
            WallInCut = new Point3d[8];
        }
        public void DrawWallBluePrint(Point3d bluePrintCenter)
        {
            string layer = "4";
            Utilities.DrawingShapes.DrawCircle(bluePrintCenter, InternalDiameter, layer); //InsideWall
            Utilities.DrawingShapes.DrawCircle(bluePrintCenter, ExternalDiameter, layer); //OutsideWall
        }
        public void DrawWallBluePrintAnnotations(Point3d bluePrintCenter)
        {
            Utilities.DrawingShapes.AddDiameterDimension(bluePrintCenter, InternalDiameter, 45.0);
            Utilities.DrawingShapes.AddDiameterDimension(bluePrintCenter, ExternalDiameter, 60.0);
            //Nome: Parede {i}
        }
        public void DrawWallUpperView(Point3d upperViewCenter)
        {
            string insideLayer = "1";
            string lineType = "DASHED";
            Utilities.DrawingShapes.DrawCircle(upperViewCenter, InternalDiameter, insideLayer, lineType); //InsideWall colocar tracejado

            string outsideLayer = "3";
            Utilities.DrawingShapes.DrawCircle(upperViewCenter, InternalDiameter + 2 * Thickness, outsideLayer); //OutsideWall
        }
        public void DrawWallUpperViewAnnotations(Point3d upperViewCenter)
        {
            Utilities.DrawingShapes.AddDiameterDimension(upperViewCenter, InternalDiameter, 45.0);
        }
        public void DrawAACut(Point3d startPointOfWallCut)
        {
            DoPointsAtWallInCut(startPointOfWallCut);
            DrawLinesWallInCut();
        }
        private void DoPointsAtWallInCut(Point3d startPoint)
        {
            //Consultar DWG de referência
            WallInCut[0] = startPoint;
            WallInCut[1] = new Point3d(WallInCut[0].X, WallInCut[0].Y - Heigth, 0);

            WallInCut[2] = new Point3d(WallInCut[0].X + ExternalDiameter, WallInCut[0].Y, 0);
            WallInCut[3] = new Point3d(WallInCut[2].X, WallInCut[1].Y, 0);

            WallInCut[4] = new Point3d(WallInCut[0].X + Thickness, WallInCut[0].Y, 0);
            WallInCut[5] = new Point3d(WallInCut[4].X, WallInCut[1].Y, 0);

            WallInCut[6] = new Point3d(WallInCut[4].X + InternalDiameter, WallInCut[4].Y, 0);
            WallInCut[7] = new Point3d(WallInCut[6].X, WallInCut[1].Y, 0);
        }
        private void DrawLinesWallInCut()
        {
            string layer = "4";
            try
            {
                Utilities.DrawingShapes.DrawLine(WallInCut[0], WallInCut[1], layer);
                Utilities.DrawingShapes.DrawLine(WallInCut[2], WallInCut[3], layer);
                Utilities.DrawingShapes.DrawLine(WallInCut[4], WallInCut[5], layer);
                Utilities.DrawingShapes.DrawLine(WallInCut[6], WallInCut[7], layer);
            }
            catch (System.Exception e) { _ = e.Message; }
        }
        public void DrawProjectionLinesInCut(Misla misla, Top top, Point3d CenterPoint)
        {
        }
        private int CalculateNumberOfProjectionLines()
        {
            double estimateNumberOfLines = Math.Log(1 / MinimumProjectonLinesSpacing) / Math.Log(2.0);
            int numberOfLines = (int)estimateNumberOfLines;
            return numberOfLines;
        }
        public void DrawAACutAnnotations(double edgeLength)
        {
            Polyline externalWall = new Polyline();
            externalWall.AddVertexAt(0, new Point2d(WallInCut[0].X, WallInCut[0].Y), 0, 0, 0);
            externalWall.AddVertexAt(1, new Point2d(WallInCut[1].X, WallInCut[1].Y), 0, 0, 0);

            Utilities.DrawingShapes.AddQuotesInPolylines(externalWall, -15.0 - edgeLength, 0.0);
        }
        public void DrawPlanifiedWall(Point3d startPoint, double topThickness, double bottomThickness)
        {
            string layer = "3";
            string lineType = "DASHED";
            Utilities.DrawingShapes.DrawRectangle(Math.PI * ExternalDiameter, (Heigth + topThickness + bottomThickness), layer, startPoint);
            layer = "1";
            if (topThickness > 0)
            {
                Point3d initPoint = new Point3d(startPoint.X, startPoint.Y - topThickness, 0);
                Utilities.DrawingShapes.DrawHorizontalLine(initPoint, Math.PI * ExternalDiameter, layer, lineType);
            }
            if (bottomThickness > 0)
            {
                Point3d initPoint = new Point3d(startPoint.X, startPoint.Y - Heigth - bottomThickness, 0);
                Utilities.DrawingShapes.DrawHorizontalLine(initPoint, Math.PI * ExternalDiameter, layer, lineType);
            }
            DrawAxisLines(startPoint, topThickness, bottomThickness);
        }
        private void DrawAxisLines(Point3d startPoint, double topThickness, double bottomThickness)
        {
            string layer = "1";
            string lineType = "DASHDOT";
            Point3d effectiveStartPoint = new Point3d(startPoint.X, startPoint.Y + 25.0, 0);
            Utilities.DrawingShapes.DrawVerticalLine(effectiveStartPoint, -25.0, layer, lineType);
            //Texto
            effectiveStartPoint = new Point3d(startPoint.X + 0.25 * Math.PI * ExternalDiameter, startPoint.Y + 25.0, 0);
            Utilities.DrawingShapes.DrawVerticalLine(effectiveStartPoint, -(Heigth + topThickness + bottomThickness)-25.0, layer, lineType);
            //Texto
            effectiveStartPoint = new Point3d(startPoint.X + 0.50 * Math.PI * ExternalDiameter, startPoint.Y + 25.0, 0);
            Utilities.DrawingShapes.DrawVerticalLine(effectiveStartPoint, -(Heigth + topThickness + bottomThickness)-25.0, layer, lineType);
            //Texto
            effectiveStartPoint = new Point3d(startPoint.X + 0.75 * Math.PI * ExternalDiameter, startPoint.Y + 25.0, 0);
            Utilities.DrawingShapes.DrawVerticalLine(effectiveStartPoint, -(Heigth + topThickness + bottomThickness)-25.0, layer, lineType);
            //Texto
            effectiveStartPoint = new Point3d(startPoint.X + Math.PI * ExternalDiameter, startPoint.Y + 25.0, 0);
            Utilities.DrawingShapes.DrawVerticalLine(effectiveStartPoint, -25.0, layer, lineType);
            //Texto
        }
        private void DrawCutInDetailment(Point3d topLeftPoint,double topThickness, double bottomThickness, bool[] boundaries) 
        { 

        
        }
    }
}