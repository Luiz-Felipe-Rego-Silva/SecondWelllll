using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Utilities
{
    class DrawingShapes
    {
        public static void DrawCircle(Point3d center, double diameter, string layer)
        {
            Circle Circle = new Circle();
            Circle.Center = center;
            Circle.Radius = diameter / 2.0;
            Circle.Layer = layer;
            DrawingUtilities.AddToDrawing(Circle);
        }

        public static void DrawArc(Point3d center, double diameter, double startAngle, double endAngle, string layer)
        {
            Arc Arc = new Arc(center, diameter / 2.0, DegrresToRadians(startAngle), DegrresToRadians(endAngle));
            Arc.Layer = layer;
            DrawingUtilities.AddToDrawing(Arc);
        }

        public static double DegrresToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static void DrawLine(Point3d startPoint, Point3d endPoint, string layer) 
        {
            Polyline line = new Polyline();
            line.Layer = layer;
            line.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            line.AddVertexAt(0, new Point2d(endPoint.X, endPoint.Y), 0, 0, 0);

            DrawingUtilities.AddToDrawing(line);
        }
        public static void DrawRectangle(Point3d Center, double X_length, double Y_length, string layer)
        {
            Point3d LeftUp = new Point3d(Center.X - X_length/2.0, Center.Y + Y_length/2.0, 0);
            Point3d LeftDown = new Point3d(Center.X - X_length/2.0, Center.Y - Y_length/2.0, 0);
            Point3d RightDown = new Point3d(Center.X + X_length/2.0, Center.Y - Y_length/2.0, 0);
            Point3d RightUp = new Point3d(Center.X + X_length/2.0, Center.Y + Y_length/2.0, 0);
            
            DrawLine(LeftUp, RightUp, layer);
            DrawLine(RightUp, RightDown, layer);
            DrawLine(RightDown, LeftDown, layer);
            DrawLine(LeftDown, LeftUp, layer);
        }
    }
}
