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
        private double effectiveHeigth;
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double AnchorFactor { get; private set; }
        public double Cover { get; private set; }
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
        public void SetSteelInfo(double spacingX, double spacingY, double gaugeX, double gaugeY, double anchorFactor, double cover)
        {
            SpacingX = spacingX;
            SpacingY = spacingY;
            GaugeX = gaugeX;
            GaugeY = gaugeY;
            AnchorFactor = anchorFactor;
            Cover = cover;
        }
        //Barras
        private Polyline VerticalExternalLine, VerticalInternalLine, HorizontalInternalLine, HorizontalExternalLine;
        public void SetBorderConditions(bool isTopInternalEngaged, bool isTopExternalEngaged, bool isBottomInternalEngaged, bool isBottomExternalEngaged)
        {
            IsTopExternalEngaged = isTopExternalEngaged;
            IsTopInternalEngaged = isTopInternalEngaged;
            IsBottomExternalEngaged = isBottomExternalEngaged;
            IsBottomInternalEngaged = isBottomInternalEngaged;
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
        private double getAnchorLength(double gauge)
        {
            return Math.Ceiling(gauge * AnchorFactor / 10.0);
        }
        private Polyline CreateVerticalExternalLineBar(string layer)
        {
            VerticalExternalLine = new Polyline();
            VerticalExternalLine.Layer = layer;
            Point2d externalBasePoint = new Point2d(BasePoint.X - 60.0, BasePoint.Y - TopOffset);
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
            return VerticalExternalLine;
        }
        private Polyline CreateVerticalInternalLineBar(string layer)
        {
            VerticalInternalLine = new Polyline();
            VerticalInternalLine.Layer = layer;
            Point2d internalBasePoint = new Point2d(BasePoint.X - 240.0, BasePoint.Y - TopOffset);
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
                    VerticalInternalLine.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
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
                    VerticalInternalLine.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
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
            return VerticalInternalLine;
        }
        private Polyline CreateHorizontalInternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 160.0, BasePoint.Y - Wall.Heigth - 70.0);
            HorizontalInternalLine = new Polyline();
            HorizontalInternalLine.Layer = layer;
            double baseLength = Math.PI * (Wall.InternalDiameter + 2 * Cover + 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            HorizontalInternalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            HorizontalInternalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);
            return HorizontalInternalLine;
        }
        private Polyline CreateHorizontalExternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 160.0, BasePoint.Y - Wall.Heigth - 120.0);
            HorizontalExternalLine = new Polyline();
            HorizontalExternalLine.Layer = layer;

            double baseLength = Math.PI * (Wall.ExternalDiameter - 2 * Cover - 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            HorizontalExternalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            HorizontalExternalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            return HorizontalExternalLine;
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
            return (2.0 * gauge * AnchorFactor) / 10.0;
        }
        private void DrawDistribuctions()
        {
            string layer = "5";
            CreateVerticalExternalLineBar(layer);
            ;
            CreateHorizontalExternalLineBar(layer);
            CreateHorizontalInternalLineBar(layer);

            int verticalQuantity = getVerticalQuantity((Wall.ExternalDiameter - 2 * Cover), SpacingY);
            int horizontalExternalQuantity = getHorizontalExternalQuantity();
            int horizontalInternalQuantity = getHorizontalInternalQuantity();

            List<StandardDistribuction> distribuctions = new List<StandardDistribuction>();
            //distribuctions.Add(new StandardDistribuction(1, CreateVerticalInternalLineBar(layer), GaugeY, SpacingY ,verticalQuantity ));
        }
        private int getVerticalQuantity(double length, double spacing)
        {
            return (int)(length / spacing);
        }
        private int getHorizontalExternalQuantity()
        {
            if (passarela > 1)
            {
                if (edgeLength > 1)
                {
                    return (int) Math.Ceiling((Wall.Heigth) / SpacingX);
                }
                else
                {
                    return (int) Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY / 10.0) / SpacingX);
                }
            }
            else
            {
                if (edgeLength > 1)
                {
                    return (int) Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY / 10.0) / SpacingX);
                }
                else
                {
                    return (int) Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX);
                }
            }
        }
        private int getHorizontalInternalQuantity()
        {
            if (topThickness > 1)
            {
                if (bottomThickness > 1)
                {
                    return (int) Math.Ceiling((Wall.Heigth) / SpacingX);
                }
                else
                {
                    return (int) Math.Ceiling((Wall.Heigth + bottomThickness - Cover - GaugeY / 10.0) / SpacingX);
                }
            }
            else
            {
                if (bottomThickness > 1)
                {
                    return (int)Math.Ceiling((Wall.Heigth + topThickness - Cover - GaugeY / 10.0) / SpacingX);
                }
                else
                {
                    return (int)Math.Ceiling((Wall.Heigth + topThickness + bottomThickness - 2 * Cover - 2 * GaugeY - TopOffset - BottomOffset) / SpacingX);
                }
            }
        }


    }
}
