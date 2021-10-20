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
        public static void AddToDrawing(Entity entity)
        {
            Document Document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database CurrentDatabase = Document.Database;
            Transaction Transaction = CurrentDatabase.TransactionManager.StartTransaction();
            {
                BlockTable AutocadBlockTable = Transaction.GetObject(CurrentDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord AutocadBlockTableRecord = Transaction.GetObject(AutocadBlockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                AutocadBlockTableRecord.AppendEntity(entity);
                Transaction.AddNewlyCreatedDBObject(entity, true);
                Transaction.Commit();
            }
        }
        public static Point3d GetPointFromUser(string question)
        {
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();

            PromptPointOptions pointOptions = new PromptPointOptions("\n" + question);
            PromptPointResult pointResult = Application.DocumentManager.MdiActiveDocument.Editor.GetPoint(pointOptions);

            return pointResult.Value;
        }
    }
}
