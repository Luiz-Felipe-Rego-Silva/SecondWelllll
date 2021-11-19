using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace Structures.Utilities
{
    public static class DrawingUtilities
    {
        public static long DrawObjectMark(Entity entity) 
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            ObjectId objectId = ObjectId.Null;
            try
            {
                using (DocumentLock documentLock = document.LockDocument())
                {
                    using (Transaction transaction = database.TransactionManager.StartTransaction())
                    {
                        BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        blockTableRecord.AppendEntity(entity);
                        transaction.AddNewlyCreatedDBObject(entity, true);
                        objectId = entity.ObjectId;
                        transaction.Commit();
                        transaction.Dispose();
                    }
                    documentLock.Dispose();
                }
            }
            catch (Exception e)
            {
                editor.WriteMessage(e.Message);
            }
            return objectId.Handle.Value;
        }
        public static void AddToDrawing(Entity entity)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            try
            {
                using (DocumentLock documentLock = document.LockDocument())
                {
                    using (Transaction transaction = database.TransactionManager.StartTransaction())
                    {
                        BlockTable blockTable = (BlockTable) transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord blockTableRecord = (BlockTableRecord) transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        blockTableRecord.AppendEntity(entity);
                        transaction.AddNewlyCreatedDBObject(entity, true);

                        transaction.Commit();
                        transaction.Dispose();
                    }
                    documentLock.Dispose();
                }
            }
            catch (Exception e)
            {
                editor.WriteMessage(e.Message);
            }

        }
        public static ObjectId DrawObject(Entity entity)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            ObjectId objectId = ObjectId.Null;
            try
            {
                using (DocumentLock documentLock = document.LockDocument())
                {
                    using (Transaction transaction = database.TransactionManager.StartTransaction())
                    {
                        BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        blockTableRecord.AppendEntity(entity);
                        transaction.AddNewlyCreatedDBObject(entity, true);
                        objectId = entity.ObjectId;
                        transaction.Commit();
                        transaction.Dispose();
                    }
                    documentLock.Dispose();
                }
            }
            catch (Exception e)
            {
                editor.WriteMessage(e.Message);
            }
            return objectId;
        }

        public static Point3d GetPointFromUser(string question)
        {
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();

            PromptPointOptions pointOptions = new PromptPointOptions("\n" + question);
            PromptPointResult pointResult = Application.DocumentManager.MdiActiveDocument.Editor.GetPoint(pointOptions);

            return pointResult.Value;
        }
        public static void DrawText(Point3d center, string content, double orientation) 
        {
            DBText descriptionText = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = content,
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = orientation,
                AlignmentPoint = center
            };
            AddToDrawing(descriptionText);
        }

    }
}
