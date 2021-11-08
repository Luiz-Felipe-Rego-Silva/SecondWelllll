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
        public bool isTopInternalEngaged { get; private set; }
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
        public void setBorderConditions() { }
        public void setOffsets() { }
        private double getAnchorLength(double gauge)
        {
            return Math.Ceiling(gauge * AnchorFactor / 10.0);
        }
        private Polyline CreateVerticalExternalLineBar(string layer)
        {
            Polyline externalBar = new Polyline();
            externalBar.Layer = layer;
            Point2d externalBasePoint = new Point2d(BasePoint.X - 60.0, BasePoint.Y - TopOffset);
            double heigth = (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset));
            double totalLenth = 0.0;
            if (isBottomExternalEngaged)
            {
                if (isTopExternalEngaged)
                {
                    totalLenth += 2 * ((Wall.Thickness - Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    externalBar.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    externalBar.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    externalBar.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (isTopExternalEngaged)
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);
                    externalBar.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - Cover) - getAnchorLength(GaugeY), externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    externalBar.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * ((Wall.Thickness - 2 * Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    externalBar.AddVertexAt(0, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    externalBar.AddVertexAt(1, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(2, new Point2d(externalBasePoint.X, externalBasePoint.Y), 0, 0, 0);
                    externalBar.AddVertexAt(3, new Point2d(externalBasePoint.X, externalBasePoint.Y - heigth), 0, 0, 0);
                    externalBar.AddVertexAt(4, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y - heigth), 0, 0, 0);
                    externalBar.AddVertexAt(5, new Point2d(externalBasePoint.X - (Wall.Thickness - 2 * Cover), externalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            ObjectId objectId = DrawingUtilities.DrawObject(externalBar);
            int quantity = getVerticalQuantity(Math.PI * (Wall.ExternalDiameter - 2 * Cover), SpacingY);
            LineToBar(objectId, 1, GaugeY, SpacingY, quantity);
            return externalBar;
        }
        private Polyline CreateVerticalInternalLineBar(string layer)
        {
            Polyline internalBar = new Polyline();
            internalBar.Layer = layer;
            Point2d internalBasePoint = new Point2d(BasePoint.X - 240.0, BasePoint.Y - TopOffset);
            double heigth = (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset));
            double totalLenth = 0.0;

            if (isBottomInternalEngaged)
            {
                if (isTopInternalEngaged)
                {
                    totalLenth += 2 * ((Wall.Thickness - Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    internalBar.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y - heigth), 0, 0, 0);
                }
            }
            else
            {
                if (isTopInternalEngaged)
                {
                    totalLenth += 2 * getAnchorLength(GaugeY) + (2 * Wall.Thickness - 3 * Cover) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - Cover) + getAnchorLength(GaugeY), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y - (Wall.Heigth - (2 * Cover + TopOffset + BottomOffset))), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    internalBar.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
                else
                {
                    totalLenth += 2 * ((Wall.Thickness - 2 * Cover) + getAnchorLength(GaugeY)) + heigth;
                    heigth += getNumberOfAmendment(totalLenth, GaugeY) * getAmendmentLength(GaugeY);

                    internalBar.AddVertexAt(0, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - getAnchorLength(GaugeY)), 0, 0, 0);
                    internalBar.AddVertexAt(1, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(2, new Point2d(internalBasePoint.X, internalBasePoint.Y), 0, 0, 0);
                    internalBar.AddVertexAt(3, new Point2d(internalBasePoint.X, internalBasePoint.Y - heigth), 0, 0, 0);
                    internalBar.AddVertexAt(4, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y - heigth), 0, 0, 0);
                    internalBar.AddVertexAt(5, new Point2d(internalBasePoint.X + (Wall.Thickness - 2 * Cover), internalBasePoint.Y + getAnchorLength(GaugeY) - heigth), 0, 0, 0);
                }
            }
            ObjectId objectId = DrawingUtilities.DrawObject(internalBar);
            int quantity = getVerticalQuantity(Math.PI * (Wall.ExternalDiameter - 2 * Cover), SpacingY);
            LineToBar(objectId, 2, GaugeY, SpacingY, quantity);
            return internalBar;
        }
        private Polyline CreateHorizontalInternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 160.0, BasePoint.Y - Wall.Heigth - 70.0);
            Polyline internalLine = new Polyline();
            internalLine.Layer = layer;
            double baseLength = Math.PI * (Wall.InternalDiameter + 2 * Cover + 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            internalLine.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            internalLine.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            ObjectId objectId = DrawingUtilities.DrawObject(internalLine);
            int quantity = getVerticalQuantity(Wall.ExternalDiameter - 2 * Cover, SpacingY);
            LineToBar(objectId, 3, GaugeY, SpacingY, quantity);

            return internalLine;
        }
        private Polyline CreateHorizontalExternalLineBar(string layer)
        {
            Point2d baseHorizontalBarPoint = new Point2d(BasePoint.X + 2 * (Wall.Thickness - 2 * Cover + getAnchorLength(GaugeY)) + 160.0, BasePoint.Y - Wall.Heigth - 120.0);
            Polyline externaline = new Polyline();
            externaline.Layer = layer;

            double baseLength = Math.PI * (Wall.ExternalDiameter - 2 * Cover - 2 * GaugeY / 10.0) + getAmendmentLength(GaugeX);
            double totalLength = baseLength + (getNumberOfAmendment(baseLength, GaugeX)) * getAmendmentLength(GaugeX);

            externaline.AddVertexAt(0, new Point2d(baseHorizontalBarPoint.X, baseHorizontalBarPoint.Y), 0, 0, 0);
            externaline.AddVertexAt(1, new Point2d(baseHorizontalBarPoint.X + totalLength, baseHorizontalBarPoint.Y), 0, 0, 0);

            return externaline;
        }
        private int getNumberOfAmendment(double baseLength, double gauge)
        {
            int numberOfAmendment = (int)(baseLength / 1200.0);
            if (Math.Floor((baseLength + numberOfAmendment * getAmendmentLength(gauge)) / 1200.0) > numberOfAmendment)
                return numberOfAmendment + 1;
            else
                return numberOfAmendment;
        }
        private double getAmendmentLength(double gauge)
        {
            return 2.0 * gauge * AnchorFactor / 10.0;
        }
        private void DrawDistribuctions()
        {
            string layer = "5";

            DrawingUtilities.AddToDrawing(CreateVerticalExternalLineBar(layer));
            DrawingUtilities.AddToDrawing(CreateVerticalInternalLineBar(layer));
            DrawingUtilities.AddToDrawing(CreateHorizontalExternalLineBar(layer));
            DrawingUtilities.AddToDrawing(CreateHorizontalInternalLineBar(layer));
        }
        private void LineToBar(ObjectId LineId, int Id, double gauge, double spacing, double quantity)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DocumentLock documentLock = document.LockDocument();
            Transaction transaction = document.TransactionManager.StartTransaction();
            Entity obj = (Entity)transaction.GetObject(LineId, OpenMode.ForWrite);

            if (obj.GetType() == typeof(Polyline))
            {
                Polyline line = (Polyline)obj;
                AddRegAppTableRecord("STRUCTCS");
                ResultBuffer ResultBuffer =
                    new ResultBuffer(
                        new TypedValue(1001, "STRUCTCS"),
                        new TypedValue(1000, $"{Id}"),
                        new TypedValue(1000, $"{Math.Round(line.Length)}"),
                        new TypedValue(1000, $"{gauge}"),
                        new TypedValue(1000, $"{spacing}"),
                        new TypedValue(1000, $"{quantity}"),
                        new TypedValue(1000, $"{0}"),
                        new TypedValue(1000, $"{0}")
                    );
                obj.XData = ResultBuffer;
                ResultBuffer.Dispose();
                transaction.Commit();
                documentLock.Dispose();
            }
        }
        private static void AddRegAppTableRecord(string RegAppName)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            using (Transaction transaction = document.TransactionManager.StartTransaction())
            {
                RegAppTable appTable = (RegAppTable)transaction.GetObject(database.RegAppTableId, OpenMode.ForRead, false);
                if (!appTable.Has(RegAppName))
                {
                    appTable.UpgradeOpen();
                    var appTableRecord = new RegAppTableRecord { Name = RegAppName };
                    appTable.Add(appTableRecord);
                    transaction.AddNewlyCreatedDBObject(appTableRecord, true);
                }
                transaction.Commit();
            }
        }
        private int getVerticalQuantity(double length, double spacing)
        {
            return (int)(length / spacing);
        }
        private int getHorizontalQuantity(double spacing, Top top, Bottom bottom)
        {
            double length = 0.0;
            if (top.Passarela > 1) 
            {
                if(bottom.EdgeLength > 1) 
                { 

                }
                else 
                { 
                }
            }
            else 
            {
                if (bottom.EdgeLength > 1) 
                {

                }
                else 
                {

                }
            }
            
            
            return (int)(length / spacing);
        }
    }
}
