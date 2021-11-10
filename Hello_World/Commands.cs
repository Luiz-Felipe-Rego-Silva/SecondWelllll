// (C) Copyright 2021 by  
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Hello_World.Detailing.UI;
using Hello_World.UI;
using System;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(Hello_World.MyCommands))]

namespace Hello_World
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        [CommandMethod("Hello")]
        public void Answer() 
        {
            MainWellForm MainForm = new MainWellForm();
            MainForm.Show();

            // Put your command code here
            Document document = Application.DocumentManager.MdiActiveDocument;
            Editor editor;
            if (document != null)
            {
                editor = document.Editor;
                editor.WriteMessage("FUNCIONOU MEU PROGAMAZINHO");

            }
        }
        [CommandMethod("Parede_Circular")]
        public void callCircularWallForm()
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
