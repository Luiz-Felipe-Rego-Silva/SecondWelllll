// (C) Copyright 2021 by //
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Views.DetailersViews;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(AutoDetailers.MyCommands))]

namespace AutoDetailers
{
    public class MyCommands
    {
        //[CommandMethod("Hello")]
        //public void Answer() 
        //{
        //    MainWellForm MainForm = new MainWellForm();
        //    MainForm.Show();
        //    // Put your command code here
        //    Document document = Application.DocumentManager.MdiActiveDocument;
        //    Editor editor;
        //    if (document != null)
        //    {
        //        editor = document.Editor;
        //        editor.WriteMessage("FUNCIONOU MEU PROGAMAZINHO");
        //    }
        //}
        [CommandMethod("Parede_Circular")]
        public void CallCircularWallForm()
        {
            CircularWallForm wallForm = new CircularWallForm();
            wallForm.Show();
            // Put your command code here
            Document document = Application.DocumentManager.MdiActiveDocument;
            Editor editor;
            if (document != null)
            {
                editor = document.Editor;
                editor.WriteMessage("FUNCIONOU BB");
            }
        }
        [CommandMethod("Fundo_Circular")]
        public void CallCircularBottomForm()
        {
            CircularBottomForm bottomForm = new CircularBottomForm();
            bottomForm.Show();
            // Put your command code here
            Document document = Application.DocumentManager.MdiActiveDocument;
            Editor editor;
            if (document != null)
            {
                editor = document.Editor;
                editor.WriteMessage("FUNCIONOU BB");
            }
        }
        [CommandMethod("BlocoA")]
        public void CallAnchorDetailer()
        {
            Controllers.AnchorBlockDetailing detailing = new Controllers.AnchorBlockDetailing();
            detailing.DrawDetailment();
        }

        /*// Modal Command with pickfirst selection
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            PromptSelectionResult result = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection();
            if (result.Status == PromptStatus.OK)
            {
                // There are selected entities
                // Put your command using pickfirst set code here
            }
            else
            {
                // There are no selected entities
                // Put your command code here
            }
        }

        // Application Session Command with localized name
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        // LispFunction is similar to CommandMethod but it creates a lisp 
        // callable function. Many return types are supported not just string
        // or integer.*/

    }

}
