using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.WellDetailer.Entities;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using Model.Detailing.Entities.SteelBars;
using Model.Detailing.Entities.Tables;

namespace Controllers
{
    class CircularWallDetailment
    {
        //Dados relativos a parede
        public CircularWall Wall { get; private set; }
        public double EffectiveHeigth { private get; set; }
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double AnchorFactor { get; private set; }
        public double Cover { get; private set; }
        public int Multiplier { get; private set; }
        //Dados relativos aos contornos da parede
        private double bottomThickness;
        private double topThickness;
        public bool IsTopInternalEngaged { get; private set; }
        public bool IsTopExternalEngaged { get; private set; }
        public bool IsBottomInternalEngaged { get; private set; }
        public bool IsBottomExternalEngaged { get; private set; }
        public bool TopExternal { get; private set; }
        public bool TopInternal { get; private set; }
        public bool BottomExternal { get; private set; }
        public bool BottomInternal { get; private set; }
        //Bitolas para compatiblização
        public double TopOffset { get; private set; }
        public double BottomOffset { get; private set; }
        //Pontos  
        public Point3d BasePoint { get; set; }

        //Barras
        private Polyline VerticalExternalLine, VerticalInternalLine, HorizontalInternalLine, HorizontalExternalLine;
        private List<StandardDistribuction> distribuctions = new List<StandardDistribuction>();
        public string Title { get; set; }
        public void SetBorder(bool topExternal, bool topInternal, bool bottomExternal, bool bottomInternal)
        {
            TopExternal = topExternal;
            TopInternal = topInternal;
            BottomExternal = bottomExternal;
            BottomInternal = bottomInternal;
        }
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
            IsTopExternalEngaged = isTopInternalEngaged;
            IsTopInternalEngaged = isTopExternalEngaged;
            IsBottomExternalEngaged = isBottomInternalEngaged;
            IsBottomInternalEngaged = isBottomExternalEngaged;
            EffectiveHeigth = Wall.Heigth + topThickness + bottomThickness;
        }
        public void SetOffsets(double topOffset, double bottomOffset)
        {
            TopOffset = topOffset;
            BottomOffset = bottomOffset;
        }
        public void SetAnnotations(string title, int multiplier)
        {
            Title = title;
            Multiplier = multiplier;
        }
        private double GetAnchorLength(double gauge)
        {
            return Math.Ceiling(gauge * AnchorFactor);
        }
        private void CreateVerticalExternalLineBar(string layer)
        {
            VerticalExternalLine = new Polyline
            {
                Layer = layer
            };
            Point2d externalBasePoint = new Point2d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + GetAnchorLength(GaugeY)) + 90.0, BasePoint.Y - 150.0 - TopOffset);
            double heigth = (EffectiveHeigth - (2 * Cover + TopOffset + BottomOffset));
            double totalLenth = 0.0;
            if (IsBottomExternalEngaged)
            {
                if (IsTopExternalEngaged)
                {
                    totalLenth += 2 * ((Wall.Thickness - Cover) + GetAnchorLength(GaugeY)) + heigth;

                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * GetAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;

                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (IsTopExternalEngaged)
                {
                    totalLenth += 2 * GetAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * ((Wall.Thickness - 2 * Cover) + GetAnchorLength(GaugeY)) + heigth;

                    VerticalExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalExternalLine.AddVertexAt(5, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            int verticalQuantity = GetVerticalQuantity(Math.PI * (Wall.ExternalDiameter - 2 * Cover), SpacingY);
            distribuctions.Add(new StandardDistribuction(1,
                VerticalExternalLine, GaugeY,
                SpacingY, verticalQuantity,
                GetNumberOfAmendment(totalLenth, GaugeY),
                (int) GetAmendmentLength(GaugeY)));
        }
        private void CreateVerticalInternalLineBar(string layer)
        {
            VerticalInternalLine = new Polyline
            {
                Layer = layer
            };
            Point2d internalBasePoint = new Point2d(BasePoint.X + 20.0, BasePoint.Y - 150.0 - TopOffset);
            double heigth = (EffectiveHeigth - (2 * Cover + TopOffset + BottomOffset));
            double totalLenth = 0.0;

            if (IsBottomInternalEngaged)
            {
                if (IsTopInternalEngaged)
                {
                    totalLenth += 2 * ((Wall.Thickness - Cover) + GetAnchorLength(GaugeY)) + heigth;
  
                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * GetAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;


                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (IsTopInternalEngaged)
                {
                    totalLenth += 2 * GetAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;

                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * ((Wall.Thickness - 2 * Cover) + GetAnchorLength(GaugeY)) + heigth;

                    VerticalInternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    VerticalInternalLine.AddVertexAt(5, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            int verticalQuantity = GetVerticalQuantity(Math.PI * (Wall.ExternalDiameter - 2 * Cover), SpacingY);
            distribuctions.Add(new StandardDistribuction(2,
                VerticalInternalLine, GaugeY,
                SpacingY, verticalQuantity,
                GetNumberOfAmendment(totalLenth, GaugeY),
                (int) GetAmendmentLength(GaugeY)));
        }
        private void CreateHorizontalInternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + GetAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 210.0 - bottomThickness - topThickness - Wall.Heigth);
            HorizontalInternalLine = new Polyline
            {
                Layer = layer
            };
            double baseLength = Math.Ceiling(Math.PI * (Wall.InternalDiameter + 2 * Cover + 2 * GaugeY)) + GetAmendmentLength(GaugeX);
            double totalLength = baseLength;

            HorizontalInternalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            HorizontalInternalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            int horizontalInternalQuantity = GetHorizontalInternalQuantity();
            distribuctions.Add(new StandardDistribuction(4, HorizontalInternalLine, GaugeX, SpacingX, horizontalInternalQuantity, GetNumberOfAmendment(baseLength, GaugeX) + 1, (int) GetAmendmentLength(GaugeX)));
        }
        private void CreateHorizontalExternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + GetAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 260.0 - bottomThickness - topThickness - Wall.Heigth); ;
            HorizontalExternalLine = new Polyline
            {
                Layer = layer
            };

            double baseLength = Math.Ceiling(Math.PI * (Wall.ExternalDiameter - 2 * Cover - 2 * GaugeY)) + GetAmendmentLength(GaugeX);
            double totalLength = baseLength;

            HorizontalExternalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            HorizontalExternalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            int horizontalExternalQuantity = GetHorizontalExternalQuantity();
            distribuctions.Add(new StandardDistribuction(3, HorizontalExternalLine, GaugeX, SpacingX, horizontalExternalQuantity, GetNumberOfAmendment(baseLength, GaugeX) + 1, (int) GetAmendmentLength(GaugeX)));
        }
        private int GetNumberOfAmendment(double baseLength, double gauge)
        {
            int numberOfAmendment = (int)(baseLength / 1200.0);
            if (Math.Floor((baseLength + numberOfAmendment * GetAmendmentLength(gauge)) / 1200.0) > numberOfAmendment)
                return numberOfAmendment + 1;
            else
                return numberOfAmendment;
        }
        public double GetAmendmentLength(double gauge)
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
        public void DrawDetailment(Point3d basePoint)
        {
            BasePoint = basePoint;
            Point3d wallBasePoint = new Point3d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + GetAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 150.0, 0);
            Point3d centerGuideReference = DrawingShapes.MiddlePoint(new Point3d(wallBasePoint.X + Math.PI * 0.5 * Wall.ExternalDiameter, wallBasePoint.Y - Wall.Heigth - topThickness - bottomThickness - 325.0, 0), new Point3d(wallBasePoint.X + Math.PI * 0.5 * Wall.ExternalDiameter, wallBasePoint.Y - Wall.Heigth - topThickness - bottomThickness - 325.0, 0));
            Point3d startPointCut = new Point3d(wallBasePoint.X + Wall.ExternalDiameter * Math.PI + 200.0, wallBasePoint.Y, 0);
            Point3d starCutNotation = new Point3d(wallBasePoint.X + 0.375 * Math.PI * Wall.ExternalDiameter, wallBasePoint.Y, 0);
            Point3d tableBasePoint = new Point3d(wallBasePoint.X + Math.PI * Wall.ExternalDiameter + 300 + Wall.Thickness + 2 * (GetAnchorLength(GaugeY) + 15.0), wallBasePoint.Y, 0);
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
            DrawCutProjection(startPointCut);
            DrawCutLine(starCutNotation);
            SteelTable steelTable = new SteelTable(distribuctions, Multiplier, Title);
            steelTable.GenerateFullTable(tableBasePoint);
        }
        private int GetVerticalQuantity(double length, double spacing)
        {
            return (int)Math.Ceiling(length / spacing);
        }
        private int GetHorizontalExternalQuantity()
        {
            if (TopExternal)
            {
                if (BottomExternal) { return (int)(Math.Ceiling((Wall.Heigth) / SpacingX) + 1); }
                else { return (int)(Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY - BottomOffset) / SpacingX) + 1); }
            }
            else
            {
                if (BottomExternal) { return (int)(Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY - TopOffset) / SpacingX) + 1); }
                else { return (int)(Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX) + 1); }
            }
        }
        private int GetHorizontalInternalQuantity()
        {
            if (TopInternal)
            {
                if (BottomInternal) { return (int)(Math.Ceiling((Wall.Heigth) / SpacingX) + 1); }
                else { return (int)(Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY - BottomOffset) / SpacingX) + 1); }
            }
            else
            {
                if (BottomInternal) { return (int)(Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY - TopOffset) / SpacingX) + 1); }
                else { return (int)(Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX) + 1); }
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

            StandardDistribuction.UpdateBars(distribuctions);

            //Horizontal Interna
            startIndex = 0;
            textPoint = DrawingShapes.MiddlePoint(distribuctions[2].BarLine.GetPoint3dAt(startIndex), distribuctions[2].BarLine.GetPoint3dAt(startIndex + 1));
            distribuctions[2].PrintDescriptionText(new Point3d(textPoint.X, textPoint.Y + 10.0, 0), 0, GetHorizontalDescriptionText(2));
            //Horizontal Externa
            startIndex = 0;
            textPoint = DrawingShapes.MiddlePoint(distribuctions[3].BarLine.GetPoint3dAt(startIndex), distribuctions[3].BarLine.GetPoint3dAt(startIndex + 1));
            distribuctions[3].PrintDescriptionText(new Point3d(textPoint.X, textPoint.Y + 10.0, 0), 0, GetHorizontalDescriptionText(3));
        }
        private string GetHorizontalDescriptionText(int index)
        {
            string result;
            if (index == 2)
            {
                result = distribuctions[2].GetDescriptionText();
                result += " - FACE EXTERNA";
            }
            else
            {
                result = distribuctions[3].GetDescriptionText();
                result += " - FACE INTERNA";
            }
            return result;
        }
        private void DrawGuideReferenceLine(Point3d centerPoint)
        {
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

            layer = "EIXOS";
            DrawingShapes.DrawLine(new Point3d(centerPoint.X - 100.0, centerPoint.Y, 0), new Point3d(centerPoint.X - 140.0, centerPoint.Y, 0), layer);
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

            DrawingShapes.DrawLine(new Point3d(centerPoint.X, centerPoint.Y + 100.0, 0), new Point3d(centerPoint.X, centerPoint.Y + 140.0, 0), layer);
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

            DrawingShapes.DrawLine(new Point3d(centerPoint.X + 100.0, centerPoint.Y, 0), new Point3d(centerPoint.X + 140.0, centerPoint.Y, 0), layer);
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

            DrawingShapes.DrawLine(new Point3d(centerPoint.X, centerPoint.Y - 100.0, 0), new Point3d(centerPoint.X, centerPoint.Y - 140.0, 0), layer);
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
            Point3d horizontalBasePoint = new Point3d(basePoint.X, basePoint.Y - 0.25 * Math.PI * EffectiveHeigth, 0);
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
            verticalBars.AddVertexAt(1, new Point2d(startPoint.X, startPoint.Y - EffectiveHeigth + 2 * Cover + BottomOffset), 0, 0, 0);
            DrawingUtilities.AddToDrawing(verticalBars);
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(new Point3d(startPoint.X - 10.0, startPoint.Y, 0), new Point3d(startPoint.X - 10.0, startPoint.Y - EffectiveHeigth + 2 * Cover + BottomOffset, 0)), description, Math.PI / 2.0);
        }
        private void DrawHorizontalReferenceBars(Point3d startPoint)
        {
            string description = $"N{distribuctions[2].Id} + N{distribuctions[3].Id}";
            Polyline horizontalBars = new Polyline() { Layer = "4" };
            horizontalBars.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            horizontalBars.AddVertexAt(1, new Point2d(startPoint.X + Math.PI * Wall.ExternalDiameter + GetAmendmentLength(GaugeX), startPoint.Y), 0, 0, 0);
            DrawingUtilities.AddToDrawing(horizontalBars);
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(new Point3d(startPoint.X + 0.25 * Math.PI * Wall.ExternalDiameter, startPoint.Y + 10.0, 0), new Point3d(startPoint.X + 0.5 * Math.PI * Wall.ExternalDiameter + GetAmendmentLength(GaugeX), startPoint.Y + 10.0, 0)), description, 0);
            string amendmentLength = $"EMENDA DE {GetAmendmentLength(GaugeX)} CM";
            DrawingUtilities.DrawText(DrawingShapes.MiddlePoint(new Point3d(startPoint.X + Math.PI * Wall.ExternalDiameter, startPoint.Y + 10.0, 0), new Point3d(startPoint.X + Math.PI * Wall.ExternalDiameter + GetAmendmentLength(GaugeX), startPoint.Y + 10.0, 0)), amendmentLength, 0);
        }
        private void DrawCutProjection(Point3d startPoint)
        {
            Point3d topAndWallStartPoint = new Point3d(startPoint.X, startPoint.Y - topThickness, 0);
            Point3d bottomStartPoint = new Point3d(topAndWallStartPoint.X, topAndWallStartPoint.Y - Wall.Heigth, 0);
            Point3d startHorizontalExternal = new Point3d(startPoint.X + Wall.Thickness - Cover - GaugeY, startPoint.Y - topThickness, 0);
            Point3d startHorizontalInternal = new Point3d(startPoint.X + Cover + GaugeY, startPoint.Y - topThickness, 0);

            DrawTitleCut(new Point3d(startPoint.X, startPoint.Y + 10.0, 0.0));
            DrawTopCutProjection(topAndWallStartPoint);
            DrawInternalWallInCut(topAndWallStartPoint);
            DrawBottomCutProjection(bottomStartPoint);
            DrawBarNotationsInCut(topAndWallStartPoint);
            DrawVerticalExternalLineBar(startPoint);
            DrawVerticalInternalLineBar(startPoint);
            DrawHorizontalExternalBarsInCut(startHorizontalExternal, GetHorizontalExternalQuantity());
            DrawHorizontalInternalBarsInCut(startHorizontalInternal, GetHorizontalInternalQuantity());
        }
        private void DrawInternalWallInCut(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - Wall.Heigth, 0), layer);
            DrawingShapes.DrawLine(new Point3d(startPoint.X + Wall.Thickness, startPoint.Y, 0), new Point3d(startPoint.X + Wall.Thickness, startPoint.Y - Wall.Heigth, 0), layer);
        }
        private void DrawTopCutProjection(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y + topThickness, 0), new Point3d(startPoint.X + Wall.Thickness, startPoint.Y + topThickness, 0), layer);
            if (TopInternal)
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X - 65.0, startPoint.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y + topThickness, 0), new Point3d(startPoint.X - 65.0, startPoint.Y + topThickness, 0), layer);
                DrawCutSymbol(new Point3d(startPoint.X - 65.0, startPoint.Y, 0), new Point3d(startPoint.X - 65.0, startPoint.Y + topThickness, 0));
            }
            else
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y + topThickness, 0), layer);
            }
            Point3d rightStart = new Point3d(startPoint.X + Wall.Thickness, startPoint.Y, 0);
            if (TopExternal)
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X + 65.0, rightStart.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(rightStart.X, rightStart.Y + topThickness, 0), new Point3d(rightStart.X + 65.0, rightStart.Y + topThickness, 0), layer);
                DrawCutSymbol(new Point3d(rightStart.X + 65.0, rightStart.Y, 0), new Point3d(rightStart.X + 65.0, rightStart.Y + topThickness, 0));
            }
            else
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X, rightStart.Y + topThickness, 0), layer);
            }
        }
        private void DrawBottomCutProjection(Point3d startPoint)
        {
            string layer = "3";
            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y - bottomThickness, 0), new Point3d(startPoint.X + Wall.Thickness, startPoint.Y - bottomThickness, 0), layer);
            if (BottomInternal)
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X - 65.0, startPoint.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y - bottomThickness, 0), new Point3d(startPoint.X - 65.0, startPoint.Y - bottomThickness, 0), layer);
                DrawCutSymbol(new Point3d(startPoint.X - 65.0, startPoint.Y - bottomThickness, 0), new Point3d(startPoint.X - 65.0, startPoint.Y, 0));
            }
            else
            {
                DrawingShapes.DrawLine(startPoint, new Point3d(startPoint.X, startPoint.Y - bottomThickness, 0), layer);
            }
            Point3d rightStart = new Point3d(startPoint.X + Wall.Thickness, startPoint.Y, 0);
            if (BottomExternal)
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X + 65.0, rightStart.Y, 0), layer);
                DrawingShapes.DrawLine(new Point3d(rightStart.X, rightStart.Y - bottomThickness, 0), new Point3d(rightStart.X + 65.0, rightStart.Y - bottomThickness, 0), layer);
                DrawCutSymbol(new Point3d(rightStart.X + 65.0, rightStart.Y - bottomThickness, 0), new Point3d(rightStart.X + 65.0, rightStart.Y, 0));
            }
            else
            {
                DrawingShapes.DrawLine(rightStart, new Point3d(rightStart.X, rightStart.Y - bottomThickness, 0), layer);
            }
        }
        private void DrawCutSymbol(Point3d startPoint, Point3d endPoint)
        {
            double cutSymbolLength = 6.0;
            double cutSymbolHeigth = 16.0;
            double length = DrawingShapes.Distance(startPoint, endPoint) + 10.0;
            Point3d middle = DrawingShapes.MiddlePoint(startPoint, endPoint);
            string layer = "1";
            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y - 5.0, 0), new Point3d(startPoint.X, startPoint.Y + (length - cutSymbolLength - 10.0) / 2.0, 0), layer);
            DrawingShapes.DrawLine(new Point3d(endPoint.X, endPoint.Y + 5.0, 0), new Point3d(endPoint.X, endPoint.Y - (length - cutSymbolLength - 10.0) / 2.0, 0), layer);

            Point3d middlLeft = new Point3d(middle.X - cutSymbolHeigth / 2.0, middle.Y, 0);
            Point3d middleRigth = new Point3d(middle.X + cutSymbolHeigth / 2.0, middle.Y, 0);

            DrawingShapes.DrawLine(new Point3d(startPoint.X, startPoint.Y + (length - cutSymbolLength - 10.0) / 2.0, 0), middlLeft, layer);
            DrawingShapes.DrawLine(new Point3d(endPoint.X, endPoint.Y - (length - cutSymbolLength - 10.0) / 2.0, 0), middleRigth, layer);
            DrawingShapes.DrawLine(middlLeft, middleRigth, layer);
        }
        private void DrawVerticalExternalLineBar(Point3d basePoint)
        {
            Polyline ExternalLine = new Polyline
            {
                Layer = "4"
            };
            Point2d externalBasePoint = new Point2d(basePoint.X + Wall.Thickness - Cover, basePoint.Y - Cover - TopOffset);
            double heigth = (EffectiveHeigth - (2 * Cover + TopOffset + BottomOffset));
            if (IsBottomExternalEngaged)
            {
                if (IsTopExternalEngaged)
                {
                    ExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    ExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    ExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    ExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    ExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (IsTopExternalEngaged)
                {
                    ExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - GetAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    ExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    ExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    ExternalLine.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    ExternalLine.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    ExternalLine.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    ExternalLine.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    ExternalLine.AddVertexAt(5, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            DrawingUtilities.AddToDrawing(ExternalLine);
        }
        private void DrawVerticalInternalLineBar(Point3d basePoint)
        {
            Polyline InternalLine = new Polyline
            {
                Layer = "4"
            };
            Point2d internalBasePoint = new Point2d(basePoint.X + Cover, basePoint.Y - Cover - TopOffset);
            double heigth = (EffectiveHeigth - (2 * Cover + TopOffset + BottomOffset));

            if (IsBottomInternalEngaged)
            {
                if (IsTopInternalEngaged)
                {
                    InternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    InternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    InternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    InternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    InternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (IsTopInternalEngaged)
                {
                    InternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + GetAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    InternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    InternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    InternalLine.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - GetAnchorLength(GaugeY)), 0, 0, 0);
                    InternalLine.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    InternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    InternalLine.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    InternalLine.AddVertexAt(5, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + GetAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            DrawingUtilities.AddToDrawing(InternalLine);
        }
        private void DrawCutLine(Point3d startPoint)
        {
            Polyline supLine = new Polyline() { Layer = "1" };

            supLine.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y - 20.0), 0, 0, 0);
            supLine.AddVertexAt(1, new Point2d(startPoint.X, startPoint.Y + 20.0), 0, 0, 0);
            supLine.AddVertexAt(2, new Point2d(startPoint.X + 10.0, startPoint.Y + 20.0), 0, 0, 0);
            DrawingUtilities.AddToDrawing(supLine);
            DrawingUtilities.DrawText(new Point3d(startPoint.X + 20.0, startPoint.Y + 20.0, 0), "A", 0.0);

            Polyline infLine = new Polyline() { Layer = "1" };
            startPoint = new Point3d(startPoint.X, startPoint.Y - EffectiveHeigth, 0);
            infLine.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y + 20.0), 0, 0, 0);
            infLine.AddVertexAt(1, new Point2d(startPoint.X, startPoint.Y - 20.0), 0, 0, 0);
            infLine.AddVertexAt(2, new Point2d(startPoint.X + 10.0, startPoint.Y - 20.0), 0, 0, 0);
            DrawingUtilities.AddToDrawing(infLine);
            DrawingUtilities.DrawText(new Point3d(startPoint.X + 20.0, startPoint.Y - 20.0, 0), "A", 0.0);
        }
        private void DrawHorizontalExternalBarsInCut(Point3d startPoint, int quantity)
        {
            string layer = "4";
            double length = Wall.Heigth;
            if (TopExternal)
            {
                if (BottomExternal) { }
                else { length += bottomThickness - Cover - GaugeY - BottomOffset; }
            }
            else
            {
                length += topThickness - Cover - GaugeY - TopOffset;
                startPoint = new Point3d(startPoint.X, startPoint.Y + topThickness - Cover - GaugeY - TopOffset, 0);
                if (BottomExternal) {  }
                else { length +=  bottomThickness - Cover - GaugeY - BottomOffset; }
            }
            double effectiveSpacing = (length - GaugeX)/ (quantity-1);
            for (int index = 0; index < quantity; index++) { DrawingShapes.DrawCircle(new Point3d(startPoint.X -0.5*GaugeX, startPoint.Y -0.5*GaugeX- index * effectiveSpacing, 0), GaugeX, layer); }
            string dimStyleName = "DIST 1-50";
            string content = $"N{distribuctions[2].Id}";
            DrawingShapes.AddAlignedDimension(dimStyleName, new Point3d(startPoint.X + Cover, startPoint.Y, 0), new Point3d(startPoint.X + Cover, startPoint.Y - length, 0), content, 20.0, 0);
        }
        private void DrawHorizontalInternalBarsInCut(Point3d startPoint, int quantity)
        {
            string layer = "4";
            double length = Wall.Heigth;
            if (TopInternal)
            {
                if (BottomInternal) { }
                else { length += bottomThickness - Cover - GaugeY - BottomOffset; }
            }
            else
            {
                length += topThickness - Cover - GaugeY - TopOffset;
                startPoint = new Point3d(startPoint.X, startPoint.Y + topThickness - Cover - GaugeY - TopOffset, 0);
                if (BottomInternal) {  }
                else { length += bottomThickness - Cover - GaugeY - BottomOffset; }
            }

            double effectiveSpacing = (length - GaugeX) / (quantity - 1);

            for (int index = 0; index < quantity; index++)
            {
                DrawingShapes.DrawCircle(new Point3d(startPoint.X + 0.5*GaugeX, startPoint.Y - 0.5 * GaugeX - index * effectiveSpacing, 0), GaugeX, layer);
            }
            string dimStyleName = "DIST 1-50";
            string content = $"N{distribuctions[3].Id}";
            DrawingShapes.AddAlignedDimension(dimStyleName, new Point3d(startPoint.X - Cover - GaugeX, startPoint.Y, 0), new Point3d(startPoint.X - Cover - GaugeX, startPoint.Y - length, 0), content, -20.0, 0);
        }
        private void DrawBarNotationsInCut(Point3d startPoint)
        {
            Point3d initPoint = new Point3d(startPoint.X + Wall.Thickness - Cover, startPoint.Y - 0.75 * Wall.Heigth, 0);
            Leader leader = new Leader() { Layer = "1", HasArrowHead = true };
            leader.AppendVertex(initPoint);
            leader.AppendVertex(new Point3d(initPoint.X + 45.0, initPoint.Y - 20.0, 0));
            DrawingUtilities.DrawText(new Point3d(initPoint.X + 55.0, initPoint.Y - 30.0, 0), $"N{distribuctions[0].Id}", 0.0);
            DrawingUtilities.AddToDrawing(leader);

            initPoint = new Point3d(startPoint.X + Cover, startPoint.Y - 0.25 * Wall.Heigth, 0);
            leader = new Leader() { Layer = "1", HasArrowHead = true };
            leader.AppendVertex(initPoint);
            leader.AppendVertex(new Point3d(initPoint.X - 45.0, initPoint.Y + 20.0, 0));
            DrawingUtilities.DrawText(new Point3d(initPoint.X - 45.0, initPoint.Y + 30.0, 0), $"N{distribuctions[1].Id}", 0.0);
            DrawingUtilities.AddToDrawing(leader);
        }
        private void DrawTitleCut(Point3d startPoint)
        {
            DBText title = new DBText()
            {
                Layer = "3",
                Height = 10,
                TextString = "CORTE A-A",
                Justify = AttachmentPoint.BottomCenter,
                Rotation = 0,
                AlignmentPoint = startPoint
            };
            DrawingUtilities.AddToDrawing(title);
        }

    }
}
