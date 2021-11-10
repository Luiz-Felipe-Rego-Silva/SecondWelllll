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
        public string Title { get; set;}
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
            double baseLength = Math.PI * (Wall.InternalDiameter + 2 * Cover + 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
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

            double baseLength = Math.PI * (Wall.ExternalDiameter - 2 * Cover - 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
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
            return (2.0 * gauge * AnchorFactor);
        }
        public void DrawDistribuctions(Point3d basePoint)
        {
            BasePoint = basePoint;
            string layer = "5";
            CreateVerticalExternalLineBar(layer);
            CreateVerticalInternalLineBar(layer);
            CreateHorizontalExternalLineBar(layer);
            CreateHorizontalInternalLineBar(layer);

            Point3d wallBasePoint = new Point3d(BasePoint.X + 20 + 2 * (Wall.Thickness - Cover + getAnchorLength(GaugeY)) + 150.0, BasePoint.Y - 150.0, 0);
            Wall.DrawPlanifiedWall(wallBasePoint, topThickness, bottomThickness);

            foreach (StandardDistribuction distribuction in distribuctions) { distribuction.Draw();}
        }
        private int getVerticalQuantity(double length, double spacing)
        {
            return (int) Math.Ceiling(length / spacing);
        }
        private int getHorizontalExternalQuantity()
        {
            if (passarela > 1)
            {
                if (edgeLength > 1) { return (int) Math.Ceiling((Wall.Heigth) / SpacingX);}
                else { return (int) Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY / 10.0) / SpacingX);}
            }
            else
            {
                if (edgeLength > 1) { return (int) Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY / 10.0) / SpacingX);}
                else { return (int) Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX);}
            }
        }
        private int getHorizontalInternalQuantity()
        {
            if (topThickness > 1)
            {
                if (bottomThickness > 1) { return (int) Math.Ceiling((Wall.Heigth) / SpacingX);}
                else { return (int) Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY / 10.0) / SpacingX);}
            }
            else
            {
                if (bottomThickness > 1) { return (int)Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY / 10.0) / SpacingX);}
                else { return (int)Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX);}
            }
        }


    }
}
