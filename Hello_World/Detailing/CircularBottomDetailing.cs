using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Detailing;
using Hello_World.Detailing.Entities;
using Structures.Utilities;
using Structures.WellDetailer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Detailing.Entities;

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
        public VerticalCut verticalCut { get; set; }
        public HorizontalCut horizontalCut { get; set; }
        private double SupWallThickness;
        private double InfWallThickness;
        private List<Line> VerticalBaseLines;
        private List<Line> HorizontalBaseLines;
        List<StandardDistribuction> distribuctions = new List<StandardDistribuction>();
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
        public void SetGeometryInfo(double diameter, double thickness, double edgeLength)
        {
            Bottom = new Bottom(diameter - 2 * edgeLength - 2 * SupWallThickness, SupWallThickness, thickness, edgeLength);
        }
        public void SetBorderThickness(double _supWallThickness, double _infWallThickness)
        {
            SupWallThickness = _supWallThickness;
            InfWallThickness = _infWallThickness;
        }
        public void SetBorderShape(bool[] conditions)
        {
            verticalCut = new VerticalCut(SupWallThickness, InfWallThickness, Bottom.Diameter);
            verticalCut.ExtraLength = StandardDistribuction.getAnchorLength(GaugeY / 10.0, AnchorFactor) + 15.0;
            verticalCut.ExtesionThickness = Bottom.Thickness;
            verticalCut.AddBorder(conditions);

            horizontalCut = new HorizontalCut(SupWallThickness, InfWallThickness, Bottom.Diameter);
            horizontalCut.ExtraLength = StandardDistribuction.getAnchorLength(GaugeX / 10.0, AnchorFactor) + 15.0;
            horizontalCut.ExtesionThickness = Bottom.Thickness;
            horizontalCut.AddBorder(conditions);
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
            Point3d bottomCenter = new Point3d(BasePoint.X + 2 * StandardDistribuction.getAnchorLength(GaugeX/10.0, AnchorFactor) + 0.5 * Bottom.Diameter + 100.0, BasePoint.Y - 2 * StandardDistribuction.getAnchorLength(GaugeY / 10.0, AnchorFactor) - 100.0 - 0.5 * Bottom.Diameter, 0);
            Point3d verticalLinePoint = new Point3d(bottomCenter.X, bottomCenter.Y - Bottom.Diameter - 2 * StandardDistribuction.getAnchorLength(GaugeY / 10.0, AnchorFactor) - 200.0, 0);
            Point3d horizontalLinePoint = new Point3d(verticalLinePoint.X, verticalLinePoint.Y - (1.25 * Bottom.Diameter), 0);
            Point3d verticalBarsPoint = new Point3d(bottomCenter.X - 0.5*Bottom.Diameter - 2 * StandardDistribuction.getAnchorLength(GaugeY / 10.0, AnchorFactor) - 100.0, bottomCenter.Y, 0);
            Point3d horizontalBarsPoint = new Point3d(bottomCenter.X, bottomCenter.Y - 0.5 * Bottom.Diameter - 70.0, 0.0);
            Point3d verticalCutPosition = new Point3d(bottomCenter.X + 0.5 * Bottom.Diameter + 50.0, bottomCenter.Y + 0.5 * Bottom.Diameter, 0);
            Point3d horizontalCutPosition = new Point3d(bottomCenter.X - 0.5*Bottom.Diameter, bottomCenter.Y + 0.5*Bottom.Diameter + 50.0 + 2 * StandardDistribuction.getAnchorLength(GaugeY / 10.0, AnchorFactor), 0);
            Point3d VARTableStartPoint = new Point3d(verticalCutPosition.X + 2 * StandardDistribuction.getAnchorLength(GaugeY/10.0, AnchorFactor) + Bottom.Thickness + 30.0 + 50.0, verticalCutPosition.Y, 0);

            DrawTitle(titlePoint);
            Bottom.DrawBottomProjection(bottomCenter, InfWallThickness);
            CreateVerticalLines(GaugeY, GetQuantity(GaugeY, SpacingY), verticalLinePoint);
            CreateHorizontalLines(GaugeX, GetQuantity(GaugeX, SpacingX), horizontalLinePoint);
            DrawVerticalDistribuctions(verticalBarsPoint);
            DrawHorizontalDistribuction(horizontalBarsPoint);
            distribuctions = StandardDistribuction.CreateReIndexedBarsList(distribuctions);
            StandardDistribuction.UpdateBars(distribuctions);
            distribuctions.OrderBy(bar => bar.Id).ToList();
            DrawVARTables(VARTableStartPoint);
            DrawHorizontalReferenceBars(bottomCenter);
            DrawVerticalReferenceBars(bottomCenter);
            verticalCut.Draw(verticalCutPosition);
            horizontalCut.Draw(horizontalCutPosition);
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
            minLength += 2 * StandardDistribuction.getAnchorLength(gauge/10.0, AnchorFactor) + EXTRA_LENGTH;
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
            DrawingShapes.DrawCircle(startPoint, 2 * Math.Sqrt(Math.Pow(0.5 * Bottom.Diameter - Cover - gauge / 20.0, 2)), "3");
            DrawingShapes.DrawCircle(startPoint, Bottom.Diameter, "3");
            VerticalBaseLines = VariableDistribuction.OrderLines(VerticalBaseLines);
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
            DrawingShapes.DrawCircle(startPoint, 2 * Math.Sqrt(Math.Pow(0.5 * Bottom.Diameter - Cover - gauge / 20.0, 2)), "3");
            DrawingShapes.DrawCircle(startPoint, Bottom.Diameter, "3");
            HorizontalBaseLines = VariableDistribuction.OrderLines(HorizontalBaseLines);
            foreach (Line line in HorizontalBaseLines) { DrawingUtilities.AddToDrawing(line); }
        }
        private void DrawVerticalDistribuctions(Point3d startPointBars)
        {
            StandardDistribuction baseBar = new StandardDistribuction() { Id = 2, Gauge = GaugeY, Spacing = SpacingY, IsVariable = true, Quantity = GetQuantity(GaugeY, SpacingY), AmendmentLength = (int)(2 * StandardDistribuction.getAnchorLength(GaugeY/10.0, AnchorFactor)) };

            double[] hookVerticalNegative = GetAnchor(new bool[2] { VerticalNegativeTopEngaged, VerticalNegativeInfEngaged }, GaugeY, GaugeX);
            VerticalVarBar negativeVerticalBar = new VerticalVarBar(baseBar, DrawingShapes.Sum(hookVerticalNegative)) { isNegative = true };
            negativeVerticalBar.SetLengths(VerticalBaseLines);
            negativeVerticalBar.barLines = VerticalBaseLines;
            negativeVerticalBar.SetAnchor(hookVerticalNegative);
            negativeVerticalBar.ResgisterDistribuction(VerticalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeY/10.0, AnchorFactor)));

            baseBar.Id = 1;
            double[] hookVerticalPositive = GetAnchor(new bool[2] { VerticalPositiveTopEngaged, VerticalPositiveInfEngaged }, GaugeY, GaugeX);
            VerticalVarBar positiveVerticalBar = new VerticalVarBar(baseBar, DrawingShapes.Sum(hookVerticalPositive)) { isNegative = false };
            positiveVerticalBar.SetLengths(VerticalBaseLines);
            positiveVerticalBar.barLines = VerticalBaseLines;
            positiveVerticalBar.SetAnchor(hookVerticalPositive);
            positiveVerticalBar.ResgisterDistribuction(VerticalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeY/10.0, AnchorFactor)));
            positiveVerticalBar.UpdateMediumLength();
            negativeVerticalBar.UpdateMediumLength();
            Point3d negativeStartPoint = new Point3d(startPointBars.X + 2 * StandardDistribuction.getAnchorLength(GaugeY/10.0, AnchorFactor) + 50.0, startPointBars.Y + negativeVerticalBar._mediumLength / 2.0, startPointBars.Z);
            Point3d positiveStartPoint = new Point3d(startPointBars.X, startPointBars.Y + positiveVerticalBar._mediumLength / 2.0, startPointBars.Z);

            positiveVerticalBar.AddDetailingToDrawing(positiveStartPoint);
            negativeVerticalBar.AddDetailingToDrawing(negativeStartPoint);
            distribuctions.Add(positiveVerticalBar);
            distribuctions.Add(negativeVerticalBar);
        }
        private void DrawHorizontalDistribuction(Point3d startPointBars)
        {
            StandardDistribuction baseBar = new StandardDistribuction() { Id = 4, Gauge = GaugeX, Spacing = SpacingX, IsVariable = true, Quantity = GetQuantity(GaugeX, SpacingX), AmendmentLength = (int)(2 * StandardDistribuction.getAnchorLength(GaugeX/10.0, AnchorFactor)) };

            double[] hookHorizontalNegative = GetAnchor(new bool[2] { HorizontalNegativeLeftEngaged, HorizontalNegativeRigthEngaged }, GaugeX, 0.0);
            HorizontalVarBar negativeHorizontalBar = new HorizontalVarBar(baseBar, DrawingShapes.Sum(hookHorizontalNegative)) { isNegative = true };
            negativeHorizontalBar.SetLengths(HorizontalBaseLines);
            negativeHorizontalBar.barLines = HorizontalBaseLines;
            negativeHorizontalBar.SetAnchor(hookHorizontalNegative);
            negativeHorizontalBar.ResgisterDistribuction(HorizontalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeX/10.0, AnchorFactor)));

            baseBar.Id = 3;
            double[] hookVerticalPositive = GetAnchor(new bool[2] { HorizontalPositiveLeftEngaged, HorizontalPositiveRigthEngaged }, GaugeX, 0.0);
            HorizontalVarBar positiveHorizontalBar = new HorizontalVarBar(baseBar, DrawingShapes.Sum(hookVerticalPositive)) { isNegative = false };
            positiveHorizontalBar.SetLengths(HorizontalBaseLines);
            positiveHorizontalBar.barLines = HorizontalBaseLines;
            positiveHorizontalBar.SetAnchor(hookVerticalPositive);
            positiveHorizontalBar.ResgisterDistribuction(HorizontalBaseLines, (int)(2 * StandardDistribuction.getAnchorLength(GaugeX/10.0, AnchorFactor)));
            positiveHorizontalBar.UpdateMediumLength();
            negativeHorizontalBar.UpdateMediumLength();

            Point3d negativeStartPoint = new Point3d(startPointBars.X - positiveHorizontalBar.MediumLength / 2.0, startPointBars.Y - 2 * StandardDistribuction.getAnchorLength(GaugeX / 10.0, AnchorFactor) - 50.0, startPointBars.Z);
            Point3d positiveStartPoint = new Point3d(startPointBars.X - positiveHorizontalBar.MediumLength / 2.0, startPointBars.Y, startPointBars.Z);

            positiveHorizontalBar.AddDetailingToDrawing(positiveStartPoint);
            negativeHorizontalBar.AddDetailingToDrawing(negativeStartPoint);
            distribuctions.Add(positiveHorizontalBar);
            distribuctions.Add(negativeHorizontalBar);
        }
        private double[] GetAnchor(bool[] engagedConditions, double gauge, double offset)
        {
            double[] values = new double[4];
            if (engagedConditions[0])
            {
                values[0] = 0.0;
                values[1] = Math.Ceiling(Bottom.Thickness - Cover - offset / 10.0 + StandardDistribuction.getAnchorLength(gauge/10.0, AnchorFactor));
            }
            else
            {
                values[0] = StandardDistribuction.getAnchorLength(gauge/10.0, AnchorFactor) ;
                values[1] = Math.Floor(Bottom.Thickness - 2 * (Cover + offset / 10.0));
            }

            if (engagedConditions[1])
            {
                values[2] = Math.Ceiling(Bottom.Thickness - Cover - offset / 10.0 + StandardDistribuction.getAnchorLength(gauge/10.0, AnchorFactor) );
                values[3] = 0.0;
            }
            else
            {
                values[2] = Math.Floor(Bottom.Thickness - 2 * (Cover + offset / 10.0));
                values[3] = StandardDistribuction.getAnchorLength(gauge/10.0, AnchorFactor);
            }

            return values;
        }
        private void DrawVARTables(Point3d startPoint)
        {
            Point3d nowPosition = startPoint;
            int nowIndex = 0;
            foreach (VariableDistribuction bar in distribuctions)
            {
                if (bar.Id != nowIndex)
                {
                    VarTable VARTable = new VarTable(bar.LenghtOfLines, bar.Id, Convert.ToInt32(bar.constantParts), bar.AmendmentLength);
                    VARTable.GenerateTable(nowPosition);
                    nowIndex++;
                    nowPosition = new Point3d(nowPosition.X + 360.0 + 10.0, nowPosition.Y, 0.0);
                }
            }

        }
        private void DrawVerticalReferenceBars(Point3d bottomCenter)
        {
            string description = "N1";
            if (distribuctions[0].Id != distribuctions[1].Id)
                description += " + N2";

            double length = Math.Sqrt(Math.Pow(0.5 * Bottom.Diameter - Cover - GaugeY / 20.0, 2) - Math.Pow(0.15 * Bottom.Diameter, 2));
            Polyline verticalBars = new Polyline() { Layer = "4" };
            verticalBars.AddVertexAt(0, new Point2d(bottomCenter.X - 0.15 * Bottom.Diameter, bottomCenter.Y - length), 0, 0, 0);
            verticalBars.AddVertexAt(1, new Point2d(bottomCenter.X - 0.15 * Bottom.Diameter, bottomCenter.Y + length), 0, 0, 0);
            DrawingUtilities.AddToDrawing(verticalBars);
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(new Point3d(bottomCenter.X - 0.15 * Bottom.Diameter - 10.0, bottomCenter.Y - length, 0), new Point3d(bottomCenter.X - 0.15 * Bottom.Diameter - 10.0, bottomCenter.Y + length, 0)), description, Math.PI / 2.0);
        }
        private void DrawHorizontalReferenceBars(Point3d bottomCenter)
        {
            string description = "N" + distribuctions[2].Id.ToString();
            if (distribuctions[2].Id != distribuctions[3].Id)
                description += " + N" + distribuctions[3].Id.ToString();

            double length = Math.Sqrt(Math.Pow(0.5 * Bottom.Diameter - Cover - GaugeY / 20.0, 2) - Math.Pow(0.35 * Bottom.Diameter, 2));
            Polyline horizontalBars = new Polyline() { Layer = "4" };
            horizontalBars.AddVertexAt(0, new Point2d(bottomCenter.X - length, bottomCenter.Y + 0.35 * Bottom.Diameter), 0, 0, 0);
            horizontalBars.AddVertexAt(1, new Point2d(bottomCenter.X + length, bottomCenter.Y + 0.35 * Bottom.Diameter), 0, 0, 0);
            DrawingUtilities.AddToDrawing(horizontalBars);
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(new Point3d(bottomCenter.X - length, bottomCenter.Y + 0.35 * Bottom.Diameter + 10.0, 0), new Point3d(bottomCenter.X + length, bottomCenter.Y + 0.35 * Bottom.Diameter + 10.0, 0)), description, 0);

        }
     
    }

}
