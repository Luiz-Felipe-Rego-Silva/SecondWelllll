using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Detailing;
using Hello_World.Detailing.Entities;
using Structures.Utilities;
using Structures.WellDetailer.Entities;
using System;
using System.Collections.Generic;

namespace Hello_World.Detailing
{
    class CircularBottomDetailing
    {
        public string Title { get; private set; }
        public double Cover { get; private set; }
        public double AnchorFactor { get; private set; }
        public int multiplier { get; set; }
        public bool HorizontalTopEngaged { get; private set; }
        public bool HorizontalInfEngaged { get; private set; }
        public bool VerticalTopEngaged { get; private set; }
        public bool VerticalInfEngaged { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        private double wallThickness;
        private List<Line> VerticalBaseLines;
        private List<Line> HorizontalBaseLines;
        private double horizontalQuantity;
        private double verticalQuantity;
        private double mediumVerticalLine;
        private double mediumHorizontalLine;
        //****************************************************//
        private const double MINIMUM_VAR_LENGTH = 60.0;
        private const double EXTRA_LENGTH = 10.0;


        public Bottom Bottom { get; private set; }

        public void SetHorizontalBorderConditions()
        {

        }
        public void SetVerticalBorderConditions()
        {

        }
        public void SetArmorInfo()
        {

        }
        public void SetGeometryInfo() { }
        public void DrawTitle(Point3d startPoint)
        {
            DBText title = new DBText()
            {
                Layer = "5",
                Height = 20,
                TextString = Title.ToUpper(),
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = startPoint
            };
            DBText esc_text = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = "ESC. 1/50",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 17.0, 0)
            };
            DBText dim_text = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = $"(Dext = {Bottom.Diameter:F0}, ESP = {Bottom.Thickness:F0})",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 51.0, 0)
            };

            DrawingUtilities.AddToDrawing(title);
            DrawingUtilities.AddToDrawing(esc_text);
            DrawingUtilities.AddToDrawing(dim_text);
        }
        public void DrawDetailment(Point3d BasePoint)
        {
            Point3d titlePoint = BasePoint;
            Point3d bottomCenter = new Point3d(BasePoint.X + 20.0 + 2 * StandardDistribuction.getAnchorLength(GaugeY) + 150.0, BasePoint.Y - 150.0 - 0.5 * Bottom.Diameter, 0);

            DrawTitle(titlePoint);
            Bottom.DrawBottomProjection(bottomCenter);
        }
        public int GetQuantity(double gauge, double spacing, bool engagedCondition)
        {
            int quantity;
            double offset = GetOffset(gauge, engagedCondition);
            quantity = (int)((Bottom.Diameter - 2 * Cover - 2 * offset) / spacing) + 1;
            return quantity;
        }
        private double GetOffset(double gauge, bool engagedCondition)
        {
            double minLength = 0.0;
            minLength += 2 * GetHookLength(engagedCondition, gauge) + EXTRA_LENGTH;
            if (MINIMUM_VAR_LENGTH > minLength)
                minLength = MINIMUM_VAR_LENGTH;

            double effectiveDiameter = Bottom.Diameter - 2 * Cover;
            double offset = (effectiveDiameter / 2.0) * (1 - Math.Sqrt(1 - Math.Pow(minLength / effectiveDiameter, 2)));
            return offset;
        }
        private double GetHookLength(bool isEngaged, double gauge)
        {
            double hookLength = 0.0;
            if (isEngaged)
            {
                hookLength += wallThickness - Cover + StandardDistribuction.getAnchorLength(gauge);
            }
            else
            {
                hookLength += wallThickness - 2 * Cover + StandardDistribuction.getAnchorLength(gauge);
            }
            return hookLength;
        }
        private void CreateVerticalLines(double gauge, double quantity, bool engagedCondition, Point3d startPoint)
        {
            VerticalBaseLines = new List<Line>();
            double offset = GetOffset(gauge, engagedCondition);
            double effectiveSpacing = (Bottom.Diameter - 2*Cover - 2*offset - quantity * gauge) / (quantity-1);
            double x0 = Bottom.Diameter * 0.5 - Cover - offset;
            for (int index = 0; index < quantity; index++)
            {
                double x = x0 - index * effectiveSpacing;
                double length = Math.Sqrt(Math.Pow(Bottom.Diameter - 2 * Cover - 2 * offset, 2) - Math.Pow(x, 2));
                Line auxiliarLine = new Line() { Layer = "4", StartPoint = new Point3d(startPoint.X - index*effectiveSpacing, startPoint.Y + length/2.0,0), EndPoint = new Point3d(startPoint.X - index * effectiveSpacing, startPoint.Y - length / 2.0, 0) };
                VerticalBaseLines.Add(auxiliarLine);
            }
            foreach(Line line in VerticalBaseLines) { DrawingUtilities.AddToDrawing(line);}
        }
        private void CreateHorizontalLines(double gauge, double quantity, bool engagedCondition, Point3d startPoint)
        {
            HorizontalBaseLines = new List<Line>();
            double offset = GetOffset(gauge, engagedCondition);
            double effectiveSpacing = (Bottom.Diameter - 2 * Cover - 2 * offset - quantity * gauge) / (quantity - 1);
            double x0 = Bottom.Diameter * 0.5 - Cover - offset;
            for (int index = 0; index < quantity; index++)
            {
                double x = x0 - index * effectiveSpacing;
                double length = Math.Sqrt(Math.Pow(Bottom.Diameter - 2 * Cover - 2 * offset, 2) - Math.Pow(x, 2));
                Line auxiliarLine = new Line() { Layer = "4", StartPoint = new Point3d(startPoint.X - length/2.0, startPoint.Y + index*effectiveSpacing, 0), EndPoint = new Point3d(startPoint.X + length / 2.0, startPoint.Y + index * effectiveSpacing, 0) };
                HorizontalBaseLines.Add(auxiliarLine);
            }
            foreach (Line line in HorizontalBaseLines) { DrawingUtilities.AddToDrawing(line); }
        }
        private void SetMediumLines() 
        {
            foreach (Line line in VerticalBaseLines) 
            {
                mediumVerticalLine += line.Length;
                mediumVerticalLine /= verticalQuantity;
            }
            foreach (Line line in HorizontalBaseLines)
            {
                mediumVerticalLine += line.Length;
                mediumVerticalLine /= horizontalQuantity;
            }
        }
        public void CreateVerticalVARPositive(Point3d startPoint) 
        {
            
        }
        public void CreateVerticalVARNegative(Point3d startPoint) 
        {
        
        }
        public void CreateHorizontalVARPositive(Point3d startPoint) 
        {
        
        }
        public void CreateHorizontalVARNegative(Point3d startPoint) 
        {
        
        }
    }

}
