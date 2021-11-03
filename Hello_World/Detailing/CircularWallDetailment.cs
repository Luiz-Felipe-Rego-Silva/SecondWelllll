using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.WellDetailer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_World.Detailing
{
    class CircularWallDetailment
    {
        //Dados relativos a parede
        public CircularWall Wall { get; private set; }
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double AnchorFactor { get; private set; }
        public double Cover { get; private set; }
        //Dados relativos aos contornos da parede
        public double EdgeLength { get; private set; }
        public double Passarela { get; private set; }
        public bool isTopInternalEngaged { get; private set;}
        public bool isTopExternalEngaged { get; private set; }
        public bool isBottomInternalEngaged { get; private set; }
        public bool isBottomExternalEngaged { get; private set; }
        //Bitolas para compatibilização
        public double TopOffset { get; private set; }
        public double BottomOffset { get; private set; }
        //Pontos  
        public Point3d BasePoint { get; private set; }


        public void setSteelInfo(double spacingX, double spacingY, double gaugeX, double gaugeY, double anchorFactor, double cover) 
        {
            SpacingX = spacingX;
            SpacingY = spacingY;
            GaugeX = gaugeX;
            GaugeY = gaugeY;
            AnchorFactor = anchorFactor;
            Cover = cover;
        }
        private double getAnchorLength(double gauge) 
        {
            return Math.Ceiling(gauge * AnchorFactor / 10.0);
        }
        private Polyline CreateVerticalInternalLineBar() 
        {
            Polyline internalBar = new Polyline();
            Point2d internalBasePoint = new Point2d(BasePoint.X, BasePoint.Y - TopOffset);
            if (isBottomInternalEngaged) 
            {
                if (isTopInternalEngaged) 
                {
                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - ( Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
                else 
                {
                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
            }
            else 
            {
                if (isTopInternalEngaged) 
                {
                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + getAnchorLength(GaugeY) - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
                else 
                {
                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(5, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + getAnchorLength(GaugeY) - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
            }
            return internalBar;
        }
        private Polyline CreateVerticalExternalLineBar() 
        {
            Polyline externalBar = new Polyline();
            Point2d ExternalBasePoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 100.0, BasePoint.Y - TopOffset);
            if (isBottomExternalEngaged) 
            {
                if (isTopExternalEngaged) 
                {
                    externalBar.AddVertexAt(0, new Point2d(ExternalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y - ( Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(ExternalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), ExternalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
                else 
                {
                    externalBar.AddVertexAt(0, new Point2d(ExternalBasePoint.X + (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    externalBar.AddVertexAt(4, new Point2d(ExternalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), BasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
            }
            else 
            {
                if (isTopExternalEngaged) 
                {
                    externalBar.AddVertexAt(0, new Point2d(ExternalBasePoint.X - (Wall.Thickness - Cover) + getAnchorLength(GaugeY), ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    externalBar.AddVertexAt(4, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y + getAnchorLength(GaugeY) - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
                else 
                {
                    externalBar.AddVertexAt(0, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(ExternalBasePoint.X, ExternalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    externalBar.AddVertexAt(4, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    externalBar.AddVertexAt(5, new Point2d(ExternalBasePoint.X - (Wall.Thickness - 2 * Cover), ExternalBasePoint.Y + getAnchorLength(GaugeY) - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
            }
            return externalBar;
        }
        private Polyline CreateHorizontalInternalLineBar() 
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 160.0, BasePoint.Y - Wall.Heigth - 70.0); 
            Polyline internalLine = new Polyline();
            double baseLength = Math.PI * (Wall.InternalDiameter + 2 * Cover + 2 * GaugeY/10.0) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            internalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            internalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);
            return internalLine;
        }
        private Polyline CreateHorizontalExternalLineBar() 
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 160.0, BasePoint.Y - Wall.Heigth - 120.0);
            Polyline externaline = new Polyline();

            double baseLength = Math.PI * (Wall.ExternalDiameter - 2 * Cover - 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            externaline.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            externaline.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            return externaline;
        }
        private bool DoesNeedAmendment(double length) 
        {
            if (length/1200.0 > 1) 
            {
                return true;
            }
            else
            {
               return false;
            }
        }
        private int getNumberOfAmendment(double baseLength, double gauge) 
        {
            int numberOfAmendment = (int) (baseLength / 1200.0);
            if (Math.Floor((baseLength + numberOfAmendment * getAmendmentLength(gauge)) / 1200.0) > numberOfAmendment)
                return numberOfAmendment + 1;
            else
                return numberOfAmendment; 
        }
        private double getAmendmentLength(double gauge) 
        {
            return 2.0 * gauge * AnchorFactor / 10.0;
        }
    }
}
