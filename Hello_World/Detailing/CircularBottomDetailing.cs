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
        public string Title { get; set; }
        public double Cover { get; private set; }
        public double AnchorFactor { get; private set; }
        public int multiplier { get; set; }
        public bool HorizontalPositiveLeftEngaged { get; private set; }
        public bool HorizontalPositiveRigthEngaged { get; private set; }
        public bool HorizontalNegativeLeftEngaged { get; private set; }
        public bool HorizontalNegativeRigthEngaged { get; private set; }
        public bool VerticalPositiveTopEngaged { get; private set; }
        public bool VerticalPositiveInfEngaged { get; private set; }
        public bool VerticalNegativeTopEngaged { get; private set; }
        public bool VerticalNegativeInfEngaged { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        private double wallThickness;
        private List<Line> VerticalBaseLines;
        private List<Line> HorizontalBaseLines;
        List<VariableDistribuction> distribuctions = new List<VariableDistribuction>();
        //****************************************************//
        private const double MINIMUM_VAR_LENGTH = 60.0;
        private const double EXTRA_LENGTH = 10.0;

        public Bottom Bottom { get; private set; }
        public void SetEngagedVerticalConditions(bool[] condtions)
        {
            VerticalNegativeTopEngaged = condtions[0];
            VerticalNegativeInfEngaged = condtions[1];
            VerticalPositiveTopEngaged = condtions[2];
            VerticalPositiveInfEngaged = condtions[3];
        }
        public void SetEngagedHorizontalConditions(bool[] condtions)
        {
            HorizontalNegativeLeftEngaged = condtions[0];
            HorizontalNegativeRigthEngaged = condtions[1];
            HorizontalPositiveLeftEngaged = condtions[2];
            HorizontalPositiveRigthEngaged = condtions[3];
        }
        public void SetArmorInfo(double gaugeX, double gaugeY, double spacingX, double spacingY, double anchorFactor, double cover)
        {
            Cover = cover;
            AnchorFactor = anchorFactor;
            GaugeX = gaugeX;
            GaugeY = gaugeY;
            SpacingX = spacingX;
            SpacingY = spacingY;
        }
        public void SetGeometryInfo(double diameter, double thickness, double edgeLength, double wallThickness)
        {
            Bottom = new Bottom(diameter - 2 * edgeLength - 2 * wallThickness, wallThickness, thickness, edgeLength);
        }
        public void SetBorderThickness(double _wallThickness)
        {
            wallThickness = _wallThickness;
        }
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
                TextString = $"(DIAMETRO = {Bottom.Diameter:F0}, ESP= {Bottom.Thickness:F0})",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 17.0, 0)
            };
            DBText dim_text = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = "ESC. 1/50",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 34.0, 0)
            };

            DrawingUtilities.AddToDrawing(title);
            DrawingUtilities.AddToDrawing(esc_text);
            DrawingUtilities.AddToDrawing(dim_text);
        }
        public void DrawDetailment(Point3d BasePoint)
        {
            Point3d titlePoint = BasePoint;
            Point3d bottomCenter = new Point3d(BasePoint.X + 150.0 + 2 * Math.Ceiling(StandardDistribuction.getAnchorLength(GaugeY)/10.0) + 0.5 * Bottom.Diameter, BasePoint.Y - 150.0 - 0.5 * Bottom.Diameter, 0);
            Point3d verticalLinePoint = new Point3d(bottomCenter.X, bottomCenter.Y - Bottom.Diameter - 200.0, 0);
            Point3d horizontalLinePoint = new Point3d(verticalLinePoint.X , verticalLinePoint.Y - (1.25 * Bottom.Diameter), 0);
            Point3d verticalBarsPoint = new Point3d(BasePoint.X, BasePoint.Y - 150.0 - 0.5 * Bottom.Diameter, 0);
            Point3d VARTableVerticalPoint = new Point3d(bottomCenter.X + 0.5 * Bottom.Diameter + 30.0, BasePoint.Y - 150.0, 0);

            DrawTitle(titlePoint);
            Bottom.DrawBottomProjection(bottomCenter);
            CreateVerticalLines(GaugeY, GetQuantity(GaugeY, SpacingY), verticalLinePoint);
            CreateHorizontalLines(GaugeX, GetQuantity(GaugeX, SpacingX), horizontalLinePoint);
            DrawVerticalDistribuctions(verticalBarsPoint, VARTableVerticalPoint);
        }
        public int GetQuantity(double gauge, double spacing)
        {
            int quantity;
            double offset = GetOffset(gauge);
            quantity = (int)Math.Ceiling((Bottom.Diameter - 2 * Cover - 2 * offset - gauge / 10.0) / spacing) + 1;
            return quantity;
        }
        private double GetOffset(double gauge)
        {
            double minLength = 0.0;
            minLength += 2 * StandardDistribuction.getAnchorLength(gauge) / 10.0 + EXTRA_LENGTH;
            if (MINIMUM_VAR_LENGTH > minLength)
                minLength = MINIMUM_VAR_LENGTH;

            double effectiveDiameter = Bottom.Diameter - 2 * Cover - gauge / 10.0;
            double offset = (effectiveDiameter / 2.0) * (1 - Math.Sqrt(1 - Math.Pow(minLength / effectiveDiameter, 2)));
            return offset;
        }
        private void CreateVerticalLines(double gauge, double quantity, Point3d startPoint)
        {
            VerticalBaseLines = new List<Line>();
            double offset = GetOffset(gauge);
            double effectiveSpacing = (Bottom.Diameter - 2 * Cover - 2 * offset - gauge / 10.0) / (quantity - 1);
            double x0 = -Bottom.Diameter * 0.5 + Cover + offset + gauge / 20.0;
            for (int index = 0; index < quantity * 0.5; index++)
            {
                double x = x0 + index * effectiveSpacing;
                double length = 2 * Math.Sqrt(Math.Pow(0.5 * Bottom.Diameter - Cover - gauge / 20.0, 2) - Math.Pow(x, 2));
                Line auxiliarLine = new Line() { Layer = "4", StartPoint = new Point3d((startPoint.X - index * effectiveSpacing) - x0, startPoint.Y + length / 2.0, 0), EndPoint = new Point3d((startPoint.X - index * effectiveSpacing) - x0, startPoint.Y - length / 2.0, 0) };
                Line mirrorLine = new Line() { Layer = "4", StartPoint = new Point3d((startPoint.X - index * effectiveSpacing) - x0 + 2 * x, startPoint.Y + length / 2.0, 0), EndPoint = new Point3d((startPoint.X - index * effectiveSpacing) - x0 + 2 * x, startPoint.Y - length / 2.0, 0) };
                VerticalBaseLines.Add(auxiliarLine);
                if (Math.Abs(x) > 0.5)
                    VerticalBaseLines.Add(mirrorLine);
            }
            foreach (Line line in VerticalBaseLines) { DrawingUtilities.AddToDrawing(line); }
        }
        private void CreateHorizontalLines(double gauge, double quantity, Point3d startPoint)
        {
            HorizontalBaseLines = new List<Line>();
            double offset = GetOffset(gauge);
            double effectiveSpacing = (Bottom.Diameter - 2 * Cover - 2 * offset - gauge / 10.0) / (quantity - 1);
            double y0 = -Bottom.Diameter * 0.5 + Cover + offset + gauge / 20.0;
            for (int index = 0; index < quantity * 0.5; index++)
            {
                double y = y0 + index * effectiveSpacing;
                double length = 2 * Math.Sqrt(Math.Pow(0.5 * Bottom.Diameter - Cover - gauge / 20.0, 2) - Math.Pow(y, 2));
                Line auxiliarLine = new Line() { Layer = "4", StartPoint = new Point3d(startPoint.X + length / 2.0, startPoint.Y - index * effectiveSpacing - y0, 0), EndPoint = new Point3d(startPoint.X - length / 2.0, startPoint.Y - index * effectiveSpacing - y0, 0) };
                Line mirrorLine = new Line() { Layer = "4", StartPoint = new Point3d(startPoint.X + length / 2.0, startPoint.Y - index * effectiveSpacing - y0 + 2 * y, 0), EndPoint = new Point3d(startPoint.X - length / 2.0, startPoint.Y - index * effectiveSpacing - y0 + 2 * y, 0) };
                HorizontalBaseLines.Add(auxiliarLine);
                if (Math.Abs(y) > 0.5)
                    HorizontalBaseLines.Add(mirrorLine);
            }
            foreach (Line line in HorizontalBaseLines) { DrawingUtilities.AddToDrawing(line); }
        }
        private void DrawVerticalDistribuctions(Point3d startPointBars, Point3d startPointTable)
        {
            StandardDistribuction baseBar = new StandardDistribuction() { Id = 2, Gauge = GaugeY, Spacing = SpacingY, IsVariable = true, Quantity = GetQuantity(GaugeY, SpacingY), AmendmentLength = (int) (2 * StandardDistribuction.getAnchorLength(GaugeY))};
            
            double[] hookVerticalNegative = GetAnchor(new bool[2] { VerticalNegativeTopEngaged, VerticalNegativeInfEngaged }, GaugeY, GaugeX);
            VerticalVarBar negativeVerticalBar = new VerticalVarBar(baseBar, DrawingShapes.Sum(hookVerticalNegative)) { isNegative = true };
            negativeVerticalBar.SetLengths(VerticalBaseLines);
            negativeVerticalBar.barLines = VerticalBaseLines;
            negativeVerticalBar.SetAnchor(hookVerticalNegative);
            negativeVerticalBar.ResgisterDistribuction(VerticalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeY)));

            baseBar.Id = 1;
            double[] hookVerticalPositive = GetAnchor(new bool[2] { VerticalPositiveTopEngaged, VerticalPositiveInfEngaged }, GaugeY, GaugeX);
            VerticalVarBar positiveVerticalBar = new VerticalVarBar(baseBar, DrawingShapes.Sum(hookVerticalPositive)) { isNegative = false };
            positiveVerticalBar.SetLengths(VerticalBaseLines);
            positiveVerticalBar.barLines = VerticalBaseLines;
            positiveVerticalBar.SetAnchor(hookVerticalPositive);
            positiveVerticalBar.ResgisterDistribuction(VerticalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeY)));
            positiveVerticalBar.UpdateMediumLength();
            negativeVerticalBar.UpdateMediumLength();
            Point3d negativeStartPoint = new Point3d(startPointBars.X + 2 * Math.Ceiling(2 * StandardDistribuction.getAnchorLength(GaugeY) / 10.0) + 20.0, startPointBars.Y + negativeVerticalBar._mediumLength/2.0, startPointBars.Z);
            Point3d positiveStartPoint = new Point3d(startPointBars.X , startPointBars.Y + positiveVerticalBar._mediumLength/2.0, startPointBars.Z);

            positiveVerticalBar.AddDetailingToDrawing(positiveStartPoint, startPointTable);
            negativeVerticalBar.AddDetailingToDrawing(negativeStartPoint, new Point3d(startPointTable.X + 360.0, startPointTable.Y, startPointTable.Z));
        }
        /*private void DrawHorizontalDistribuction(Point3d startPointBars, Point3d startPointTable) 
        {
            StandardDistribuction baseBar = new StandardDistribuction() { Id = 2, Gauge = GaugeX, Spacing = SpacingX, IsVariable = true, Quantity = GetQuantity(GaugeX, SpacingX), AmendmentLength = (int)(2 * StandardDistribuction.getAnchorLength(GaugeX)) };

            double[] hookHorizontalNegative = GetAnchor(new bool[2] { VerticalNegativeTopEngaged, VerticalNegativeInfEngaged }, GaugeY, GaugeX);
            VerticalVarBar negativeHorizontalBar = new VerticalVarBar(baseBar, DrawingShapes.Sum(hookHorizontalNegative)) { isNegative = true };
            negativeHorizontalBar.SetLengths(VerticalBaseLines);
            negativeHorizontalBar.barLines = VerticalBaseLines;
            negativeHorizontalBar.SetAnchor(hookHorizontalNegative);
            negativeHorizontalBar.ResgisterDistribuction(VerticalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeY)));

            baseBar.Id = 1;
            double[] hookVerticalPositive = GetAnchor(new bool[2] { VerticalPositiveTopEngaged, VerticalPositiveInfEngaged }, GaugeY, GaugeX);
            VerticalVarBar positiveHorizontalBar = new VerticalVarBar(baseBar, DrawingShapes.Sum(hookVerticalPositive)) { isNegative = false };
            positiveHorizontalBar.SetLengths(VerticalBaseLines);
            positiveHorizontalBar.barLines = VerticalBaseLines;
            positiveHorizontalBar.SetAnchor(hookVerticalPositive);
            positiveHorizontalBar.ResgisterDistribuction(VerticalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeY)));
            positiveHorizontalBar.UpdateMediumLength();
            negativeHorizontalBar.UpdateMediumLength();
            Point3d negativeStartPoint = new Point3d(startPointBars.X + 2 * Math.Ceiling(2 * StandardDistribuction.getAnchorLength(GaugeY) / 10.0) + 20.0, startPointBars.Y + negativeHorizontalBar._mediumLength / 2.0, startPointBars.Z);
            Point3d positiveStartPoint = new Point3d(startPointBars.X, startPointBars.Y + positiveHorizontalBar._mediumLength / 2.0, startPointBars.Z);

            positiveHorizontalBar.AddDetailingToDrawing(positiveStartPoint, startPointTable);
            negativeHorizontalBar.AddDetailingToDrawing(negativeStartPoint, new Point3d(startPointTable.X + 360.0, startPointTable.Y, startPointTable.Z));
        }*/
        private double[] GetAnchor(bool[] engagedConditions, double gauge, double offset)
        {
            double[] values = new double[4];
            if (engagedConditions[0])
            {
                values[0] = 0.0;
                values[1] = Math.Ceiling(Bottom.Thickness - Cover - offset/10.0 + Math.Ceiling(StandardDistribuction.getAnchorLength(gauge)/10.0));
            }
            else
            {
                values[0] = Math.Ceiling(StandardDistribuction.getAnchorLength(gauge)/10.0);
                values[1] = Math.Floor(Bottom.Thickness - 2 * (Cover + offset/10.0));
            }

            if (engagedConditions[1])
            {
                values[2] = Math.Ceiling(Bottom.Thickness - Cover - offset/10.0 + Math.Ceiling(StandardDistribuction.getAnchorLength(gauge)/10.0));
                values[3] = 0.0;
            }
            else
            {
                values[2] = Math.Floor(Bottom.Thickness - 2 * (Cover + offset/10.0));
                values[3] = Math.Ceiling(StandardDistribuction.getAnchorLength(gauge)/10.0);
            }

            return values;
        }
        private void DrawHorizontalCut(double elem1, double elem2, Point3d point3D)
        {


        }
        private void DrawLeftSideCut(double elem1, Point3d startPoint) { }
        private void DrawHorizontalMiddle(Point3d startPoint) { }
        private void DrawRigthSideCut(double elem2, Point3d startPoint) { }
        private void DrawVerticalCut()
        {

        }
        private void DrawUpSideCut(double elem3, Point3d startPoint) { }
        private void DrawVerticalMiddle(Point3d startPoint) { }
        private void DrawDownSideCut(double elem4, Point3d startPoint) { }

    }

}
