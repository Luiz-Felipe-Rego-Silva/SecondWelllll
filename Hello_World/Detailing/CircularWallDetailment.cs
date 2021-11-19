using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.WellDetailer.Entities;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Detailing;

namespace Hello_World.Detailing
{
    class CircularWallDetailment
    {
        //Dados relativos a parede
        public CircularWall Wall { get; private set; }
        public double effectiveHeigth { private get; set; }
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double AnchorFactor { get; private set; }
        public double Cover { get; private set; }
        public int Multiplier { get; private set; }
        //Dados relativos aos contornos da parede
        private double edgeLength;
        private double passarela;
        private double bottomThickness;
        private double topThickness;
        public bool IsTopInternalEngaged { get; private set; }
        public bool IsTopExternalEngaged { get; private set; }
        public bool IsBottomInternalEngaged { get; private set; }
        public bool IsBottomExternalEngaged { get; private set; }
        //Bitolas para compatiblização
        public double TopOffset { get; private set; }
        public double BottomOffset { get; private set; }
        //Pontos  
        public Point3d BasePoint { get; set; }

        //Barras
        private Polyline VerticalExternalLine, VerticalInternalLine, HorizontalInternalLine, HorizontalExternalLine;
        private List<StandardDistribuction> distribuctions = new List<StandardDistribuction>();
        public string Title { get; set; }
        public void SetSteelInfo(double spacingX, double spacingY, double gaugeX, double gaugeY, double anchorFactor, double cover)
        {
            SpacingX = spacingX;
            SpacingY = spacingY;
            GaugeX = gaugeX;
            GaugeY = gaugeY;
            AnchorFactor = anchorFactor;
            Cover = cover;
        }
        public void SetGeometry(CircularWall wall, double topThickness, double bottomThickness)
        {
            Wall = wall;
            this.topThickness = topThickness;
            this.bottomThickness = bottomThickness;
        }
        public void SetBorderConditions(bool isTopInternalEngaged, bool isTopExternalEngaged, bool isBottomInternalEngaged, bool isBottomExternalEngaged)
        {
            IsTopExternalEngaged = isTopExternalEngaged;
            IsTopInternalEngaged = isTopInternalEngaged;
            IsBottomExternalEngaged = isBottomExternalEngaged;
            IsBottomInternalEngaged = isBottomInternalEngaged;
            effectiveHeigth = Wall.Heigth + topThickness + bottomThickness;
        }
        public void SetOffsets(double topOffset, double bottomOffset)
        {
            TopOffset = topOffset;
            BottomOffset = bottomOffset;
        }
        public void SetBorderConditions(double bottomThickness, double topThickness, double passarela, double edgeLength)
        {
            this.bottomThickness = bottomThickness;
            this.topThickness = topThickness;
            this.passarela = passarela;
            this.edgeLength = edgeLength;
            effectiveHeigth = Wall.Heigth + topThickness + bottomThickness;
        }
        public void setAnnotations(string title, int multiplier)
        {
            Title = title;
            Multiplier = multiplier;
        }
        private double getAnchorLength(double gauge)
        {
            return Math.Ceiling(gauge * AnchorFactor);
        }
        private void CreateVerticalExternalLineBar(string layer)
        {
            VerticalExternalLine = new Polyline();
            VerticalExternalLine.Layer = layer;
            Point2d externalBasePoint = new Point2d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + getAnchorLength(GaugeY)) + 90.0, BasePoint.Y - 150.0 - TopOffset);
            double heigth = (effectiveHeigth - (2 * Cover + TopOffset + BottomOffset));
            double totalLenth = 0.0;
            if (IsBottomExternalEngaged)
            {
                if (IsTopExternalEngaged)
                {
                    totalLenth += 2 * ((Wall.Thickness - Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (IsTopExternalEngaged)
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);
                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * ((Wall.Thickness - 2 * Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(5, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            int verticalQuantity = getVerticalQuantity(Math.PI * (Wall.ExternalDiameter - 2 * Cover), SpacingY);
            distribuctions.Add(new StandardDistribuction(1,
                VerticalExternalLine, GaugeY,
                SpacingY, verticalQuantity,
                getNumberOfAmendment(totalLenth, GaugeY),
                (GaugeY)));
        }
        private void CreateVerticalInternalLineBar(string layer)
        {
            VerticalInternalLine = new Polyline();
            VerticalInternalLine.Layer = layer;
            Point2d internalBasePoint = new Point2d(BasePoint.X + 20.0, BasePoint.Y - 150.0 - TopOffset);
            double heigth = (effectiveHeigth - (2 * Cover + TopOffset + BottomOffset));
            double totalLenth = 0.0;

            if (IsBottomInternalEngaged)
            {
                if (IsTopInternalEngaged)
                {
                    totalLenth += 2 * ((Wall.Thickness - Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (IsTopInternalEngaged)
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * ((Wall.Thickness - 2 * Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(5, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            int verticalQuantity = getVerticalQuantity(Math.PI * (Wall.ExternalDiameter - 2 * Cover), SpacingY);
            distribuctions.Add(new StandardDistribuction(2,
                VerticalInternalLine, GaugeY,
                SpacingY, verticalQuantity,
                getNumberOfAmendment(totalLenth, GaugeY),
                (GaugeY)));
        }
        private void CreateHorizontalInternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + getAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 210.0 - bottomThickness - topThickness - Wall.Heigth);
            HorizontalInternalLine = new Polyline();
            HorizontalInternalLine.Layer = layer;
            double baseLength = Math.Ceiling(Math.PI * (Wall.InternalDiameter + 2 * Cover + 2 * GaugeY)) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            HorizontalInternalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            HorizontalInternalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            int horizontalInternalQuantity = getHorizontalInternalQuantity();
            distribuctions.Add(new StandardDistribuction(4, HorizontalInternalLine, GaugeX, SpacingX, horizontalInternalQuantity, getNumberOfAmendment(baseLength, GaugeX) + 1, getAmendmentLength(GaugeX)));
        }
        private void CreateHorizontalExternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + getAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 260.0 - bottomThickness - topThickness - Wall.Heigth); ;
            HorizontalExternalLine = new Polyline();
            HorizontalExternalLine.Layer = layer;

            double baseLength = Math.Ceiling(Math.PI * (Wall.ExternalDiameter - 2 * Cover - 2 * GaugeY)) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            HorizontalExternalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            HorizontalExternalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            int horizontalExternalQuantity = getHorizontalExternalQuantity();
            distribuctions.Add(new StandardDistribuction(3, HorizontalExternalLine, GaugeX, SpacingX, horizontalExternalQuantity, getNumberOfAmendment(baseLength, GaugeX) + 1, getAmendmentLength(GaugeX)));
        }
        private int getNumberOfAmendment(double baseLength, double gauge)
        {
            int numberOfAmendment = (int)(baseLength / 1200.0);
            if (Math.Floor((baseLength + numberOfAmendment * getAmendmentLength(gauge)) / 1200.0) > numberOfAmendment)
                return numberOfAmendment + 1;
            else
                return numberOfAmendment;
        }
        public double getAmendmentLength(double gauge)
        {
            return Math.Ceiling(2.0 * gauge * AnchorFactor);
        }
        private void SetDistribuctions()
        {
            string layer = "5";
            CreateVerticalExternalLineBar(layer);
            CreateVerticalInternalLineBar(layer);
            CreateHorizontalExternalLineBar(layer);
            CreateHorizontalInternalLineBar(layer);
        }
        public void DrawDistribuctions(Point3d basePoint)
        {
            BasePoint = basePoint;
            Point3d wallBasePoint = new Point3d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + getAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 150.0, 0);
            Point3d tableBasePoint = new Point3d(wallBasePoint.X + Math.PI * Wall.ExternalDiameter + 300.0 + Wall.Thickness, wallBasePoint.Y - 50.0, 0);
            Point3d centerGuideReference = DrawingShapes.MiddlePoint(new Point3d(wallBasePoint.X + Math.PI * 0.5 * Wall.ExternalDiameter, wallBasePoint.Y - Wall.Heigth - topThickness - bottomThickness - 325.0, 0), new Point3d(wallBasePoint.X + Math.PI * 0.5 * Wall.ExternalDiameter, wallBasePoint.Y - Wall.Heigth - topThickness - bottomThickness - 325.0, 0));


            DrawTitle(basePoint);
            Wall.DrawPlanifiedWall(wallBasePoint, topThickness, bottomThickness);
            DrawGuideReferenceLine(centerGuideReference);
            SetDistribuctions();
            foreach (StandardDistribuction distribuction in distribuctions) { distribuction.Draw(); }
            distribuctions = StandardDistribuction.CreateReIndexedBarsList(distribuctions);
            StandardDistribuction.UpdateBars(distribuctions);
            DrawBarsInfo();
            QuoteExternalLine();
            QuoteInternalLine();
            DrawReferenceBars(wallBasePoint);
            SteelTable steelTable = new SteelTable(distribuctions, Multiplier, Title);
            steelTable.GenerateFullTable(tableBasePoint);
        }
        private int getVerticalQuantity(double length, double spacing)
        {
            return (int)Math.Ceiling(length / spacing);
        }
        private int getHorizontalExternalQuantity()
        {
            if (passarela > 1)
            {
                if (edgeLength > 1) { return (int)Math.Ceiling((Wall.Heigth) / SpacingX); }
                else { return (int)Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY) / SpacingX); }
            }
            else
            {
                if (edgeLength > 1) { return (int)Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY) / SpacingX); }
                else { return (int)Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX); }
            }
        }
        private int getHorizontalInternalQuantity()
        {
            if (topThickness > 1)
            {
                if (bottomThickness > 1) { return (int)Math.Ceiling((Wall.Heigth) / SpacingX); }
                else { return (int)Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY) / SpacingX); }
            }
            else
            {
                if (bottomThickness > 1) { return (int)Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY) / SpacingX); }
                else { return (int)Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX); }
            }
        }
        private void DrawTitle(Point3d startPoint)
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
            DBText infoProjection = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = "(VISTA DA PAREDE EM PROJEÇÃO)",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 17.0, 0)
            };
            DBText esc_txt = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = "ESC. 1/50",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 34.0, 0)
            };
            DBText dim_text = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = $"(h={Wall.Heigth + bottomThickness + topThickness}, esp={Wall.Thickness}, Dext={Wall.ExternalDiameter})",
                Justify = AttachmentPoint.BottomLeft,
                Rotation = 0,
                AlignmentPoint = new Point3d(startPoint.X, startPoint.Y - 51.0, 0)
            };

            DrawingUtilities.AddToDrawing(title);
            DrawingUtilities.AddToDrawing(infoProjection);
            DrawingUtilities.AddToDrawing(esc_txt);
            DrawingUtilities.AddToDrawing(dim_text);
        }
        private void DrawBarsInfo()
        {
            //Vertical Externa
            int startIndex = 1;
            if (Math.Abs(distribuctions[0].BarLine.GetPoint3dAt(startIndex).X - distribuctions[0].BarLine.GetPoint3dAt(startIndex + 1).X) > 0.5)
                startIndex += 1;
            Point3d textPoint = DrawingShapes.MiddlePoint(distribuctions[0].BarLine.GetPoint3dAt(startIndex), distribuctions[0].BarLine.GetPoint3dAt(startIndex + 1));
            distribuctions[0].PrintDescriptionText(new Point3d(textPoint.X + 10.0, textPoint.Y, 0), Math.PI / 2.0);

            //Vertical Interna
            startIndex = 1;
            if (Math.Abs(distribuctions[1].BarLine.GetPoint3dAt(startIndex).X - distribuctions[1].BarLine.GetPoint3dAt(startIndex + 1).X) > 0.5)
                startIndex += 1;
            textPoint = DrawingShapes.MiddlePoint(distribuctions[1].BarLine.GetPoint3dAt(startIndex), distribuctions[1].BarLine.GetPoint3dAt(startIndex + 1));
            distribuctions[1].PrintDescriptionText(new Point3d(textPoint.X - 10.0, textPoint.Y, 0), Math.PI / 2.0);

            //Horizontal Interna
            startIndex = 0;
            textPoint = DrawingShapes.MiddlePoint(distribuctions[2].BarLine.GetPoint3dAt(startIndex), distribuctions[2].BarLine.GetPoint3dAt(startIndex + 1));
            distribuctions[2].PrintDescriptionText(new Point3d(textPoint.X, textPoint.Y + 10.0, 0), 0);
            //Horizontal Externa
            startIndex = 0;
            textPoint = DrawingShapes.MiddlePoint(distribuctions[3].BarLine.GetPoint3dAt(startIndex), distribuctions[3].BarLine.GetPoint3dAt(startIndex + 1));
            distribuctions[3].PrintDescriptionText(new Point3d(textPoint.X, textPoint.Y + 10.0, 0), 0);

            StandardDistribuction.UpdateBars(distribuctions);
        }
        private void DrawGuideReferenceLine(Point3d centerPoint)
        {
            string lineType = "DASHDOT";
            DBText title = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = "ESQUEMA DAS LINHAS DE REFERÊNCIA",
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = 0,
                AlignmentPoint = new Point3d(centerPoint.X, centerPoint.Y + 175.0, 0)
            };
            DrawingUtilities.AddToDrawing(title);
            title.Dispose();


            string layer = "3";
            DrawingShapes.DrawCircle(centerPoint, 160.0, layer);
            DrawingShapes.DrawCircle(centerPoint, 200.0, layer);

            layer = "1";
            DrawingShapes.DrawLine(new Point3d(centerPoint.X - 100.0, centerPoint.Y, 0), new Point3d(centerPoint.X - 140.0, centerPoint.Y, 0), layer, lineType);
            DBText AAxis = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = "A",
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = 0,
                AlignmentPoint = new Point3d(centerPoint.X - 140.0, centerPoint.Y + 10.0, 0)
            };
            DrawingUtilities.AddToDrawing(AAxis);
            AAxis.Dispose();

            DrawingShapes.DrawLine(new Point3d(centerPoint.X, centerPoint.Y + 100.0, 0), new Point3d(centerPoint.X, centerPoint.Y + 140.0, 0), layer, lineType);
            DBText BAxis = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = "B",
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = 0,
                AlignmentPoint = new Point3d(centerPoint.X, centerPoint.Y + 150.0, 0)
            };
            DrawingUtilities.AddToDrawing(BAxis);
            BAxis.Dispose();

            DrawingShapes.DrawLine(new Point3d(centerPoint.X + 100.0, centerPoint.Y, 0), new Point3d(centerPoint.X + 140.0, centerPoint.Y, 0), layer, lineType);
            DBText CAxis = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = "C",
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = 0,
                AlignmentPoint = new Point3d(centerPoint.X + 140.0, centerPoint.Y + 10.0, 0)
            };
            DrawingUtilities.AddToDrawing(CAxis);
            CAxis.Dispose();

            DrawingShapes.DrawLine(new Point3d(centerPoint.X, centerPoint.Y - 100.0, 0), new Point3d(centerPoint.X, centerPoint.Y - 140.0, 0), layer, lineType);
            DBText DAxis = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = "D",
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = 0,
                AlignmentPoint = new Point3d(centerPoint.X, centerPoint.Y - 150.0, 0)
            };
            DrawingUtilities.AddToDrawing(DAxis);
            DAxis.Dispose();
        }
        private void QuoteExternalLine()
        {
            string dimensionStyle = "FERRO 1-50";
            if (IsBottomExternalEngaged)
            {
                if (IsTopExternalEngaged)
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(0),
                        VerticalExternalLine.GetPoint3dAt(1),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(0), VerticalExternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(1),
                        VerticalExternalLine.GetPoint3dAt(2),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(1), VerticalExternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(2),
                        VerticalExternalLine.GetPoint3dAt(3),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(2), VerticalExternalLine.GetPoint3dAt(3)).ToString());
                }
                else
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(0),
                        VerticalExternalLine.GetPoint3dAt(1),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(0), VerticalExternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(1),
                        VerticalExternalLine.GetPoint3dAt(2),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(1), VerticalExternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(2),
                        VerticalExternalLine.GetPoint3dAt(3),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(2), VerticalExternalLine.GetPoint3dAt(3)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(3),
                        VerticalExternalLine.GetPoint3dAt(4),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(3), VerticalExternalLine.GetPoint3dAt(4)).ToString());
                }
            }
            else
            {
                if (IsTopExternalEngaged)
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(0),
                        VerticalExternalLine.GetPoint3dAt(1),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(0), VerticalExternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(1),
                        VerticalExternalLine.GetPoint3dAt(2),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(1), VerticalExternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(2),
                        VerticalExternalLine.GetPoint3dAt(3),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(2), VerticalExternalLine.GetPoint3dAt(3)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(3),
                        VerticalExternalLine.GetPoint3dAt(4),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(3), VerticalExternalLine.GetPoint3dAt(4)).ToString());
                }
                else
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(0),
                        VerticalExternalLine.GetPoint3dAt(1),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(0), VerticalExternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(1),
                        VerticalExternalLine.GetPoint3dAt(2),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(1), VerticalExternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(2),
                        VerticalExternalLine.GetPoint3dAt(3),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(2), VerticalExternalLine.GetPoint3dAt(3)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(3),
                        VerticalExternalLine.GetPoint3dAt(4),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(3), VerticalExternalLine.GetPoint3dAt(4)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalExternalLine.GetPoint3dAt(4),
                        VerticalExternalLine.GetPoint3dAt(5),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalExternalLine.GetPoint3dAt(4), VerticalExternalLine.GetPoint3dAt(5)).ToString());
                }
            }
        }
        private void QuoteInternalLine()
        {
            string dimensionStyle = "FERRO 1-50";
            if (IsBottomInternalEngaged)
            {
                if (IsTopInternalEngaged)
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(0),
                        VerticalInternalLine.GetPoint3dAt(1),
                        0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(0), VerticalInternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(1),
                        VerticalInternalLine.GetPoint3dAt(2),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(1), VerticalInternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(2),
                        VerticalInternalLine.GetPoint3dAt(3),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(2), VerticalInternalLine.GetPoint3dAt(3)).ToString());
                }
                else
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(0),
                        VerticalInternalLine.GetPoint3dAt(1),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(0), VerticalInternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(1),
                        VerticalInternalLine.GetPoint3dAt(2),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(1), VerticalInternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(2),
                        VerticalInternalLine.GetPoint3dAt(3),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(2), VerticalInternalLine.GetPoint3dAt(3)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(3),
                        VerticalInternalLine.GetPoint3dAt(4),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(3), VerticalInternalLine.GetPoint3dAt(4)).ToString());
                }
            }
            else
            {
                if (IsTopInternalEngaged)
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(0),
                        VerticalInternalLine.GetPoint3dAt(1),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(0), VerticalInternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(1),
                        VerticalInternalLine.GetPoint3dAt(2),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(1), VerticalInternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(2),
                        VerticalInternalLine.GetPoint3dAt(3),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(2), VerticalInternalLine.GetPoint3dAt(3)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(3),
                        VerticalInternalLine.GetPoint3dAt(4),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(3), VerticalInternalLine.GetPoint3dAt(4)).ToString());
                }
                else
                {
                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(0),
                        VerticalInternalLine.GetPoint3dAt(1),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(0), VerticalInternalLine.GetPoint3dAt(1)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(1),
                        VerticalInternalLine.GetPoint3dAt(2),
                        0.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(1), VerticalInternalLine.GetPoint3dAt(2)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(2),
                        VerticalInternalLine.GetPoint3dAt(3),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(2), VerticalInternalLine.GetPoint3dAt(3)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(3),
                        VerticalInternalLine.GetPoint3dAt(4),
                        0.0, -20.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(3), VerticalInternalLine.GetPoint3dAt(4)).ToString());

                    StandardDistribuction.AddDimension(
                        dimensionStyle,
                        VerticalInternalLine.GetPoint3dAt(4),
                        VerticalInternalLine.GetPoint3dAt(5),
                        20.0, 0.0,
                        DrawingShapes.Distance(VerticalInternalLine.GetPoint3dAt(4), VerticalInternalLine.GetPoint3dAt(5)).ToString());
                }
            }
        }
        private void DrawReferenceBars(Point3d basePoint)
        {
            Point3d verticalBasePoint = new Point3d(basePoint.X + 0.575 * Math.PI * Wall.ExternalDiameter, basePoint.Y - Cover - TopOffset, 0);
            Point3d horizontalBasePoint = new Point3d(basePoint.X, basePoint.Y - 0.25 * Math.PI * effectiveHeigth, 0);
            DrawVerticalReferenceBars(verticalBasePoint);
            DrawHorizontalReferenceBars(horizontalBasePoint);
        }
        private void DrawVerticalReferenceBars(Point3d startPoint)
        {
            string description = "N1";
            if (distribuctions[0].Id != distribuctions[1].Id)
                description += " + N2";

            Polyline verticalBars = new Polyline() { Layer = "4" };
            verticalBars.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            verticalBars.AddVertexAt(1, new Point2d(startPoint.X, startPoint.Y - effectiveHeigth + 2 * Cover + BottomOffset), 0, 0, 0);
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(startPoint, new Point3d(startPoint.X, startPoint.Y - effectiveHeigth + 2 * Cover + BottomOffset, 0)), description, Math.PI / 2.0);
        }
        private void DrawHorizontalReferenceBars(Point3d startPoint)
        {
            string description = distribuctions[2].Id.ToString() + distribuctions[3].Id.ToString();
            Polyline horizontalBars = new Polyline() { Layer = "4" };
            horizontalBars.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            horizontalBars.AddVertexAt(1, new Point2d(startPoint.X + HorizontalExternalLine.Length, startPoint.Y), 0, 0, 0);
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(startPoint, new Point3d(startPoint.X + HorizontalExternalLine.Length, startPoint.Y, 0)), description, Math.PI / 2.0);
        }
    }
}
