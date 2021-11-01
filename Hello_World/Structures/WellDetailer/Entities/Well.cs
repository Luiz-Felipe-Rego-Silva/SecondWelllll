using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using WellDetailing.WellDetailer.Entities;

namespace Structures.WellDetailer.Entities
{
    public class Well
    {
        public CircularWall Wall { get;  set; }
        public Misla Misla { get;  set; }
        public Bottom Bottom { get;  set; }
        public Top Top { get;  set; }

        public Well()
        {

        }
        public Well(CircularWall wall, Bottom bottom, Top top)
        {
            Wall = wall;
            Bottom = bottom;
            Top = top;
        }
        public Well(CircularWall wall, Misla misla, Bottom bottom, Top top)
        {
            Wall = wall;
            Misla = misla;
            Bottom = bottom;
            Top = top;
        }

        public void DrawWellBluePrint(Point3d StartPointBluePrint) 
        {
            Point3d CenterWallBluePrint = new Point3d(StartPointBluePrint.X, StartPointBluePrint.Y,0);
            Point3d CenterBottomBluePrint = CenterWallBluePrint;
            Point3d CenterMislaBluePrint = CenterWallBluePrint;

            Wall.DrawWallBluePrint(CenterWallBluePrint);
            Bottom.DrawBottomBluePrint(CenterBottomBluePrint);
            if (Misla.Length > 1 && Misla.Heigth > 1) { Misla.DrawBluePrint(CenterMislaBluePrint, Wall.InternalDiameter - 2.0*Misla.Length); }

            Wall.DrawWallBluePrintAnnotations(CenterWallBluePrint);
            Bottom.DrawBottomBluePrintAnnotations(CenterBottomBluePrint);
            DrawGeneralAnnotations(StartPointBluePrint);
        }

        public void DrawWellUpperView(Point3d centerUpperView) 
        {
            Wall.DrawWallUpperView(centerUpperView);
            Wall.DrawWallUpperViewAnnotations(centerUpperView);
            Top.DrawTopUpperView(centerUpperView);
            Top.DrawTopUpperViewAnnotations(centerUpperView);
        }
        public void DrawGeneralAnnotations(Point3d bluePrintCenter) 
        {
            Point3d hoodCenter = new Point3d(bluePrintCenter.X + Bottom.Diameter * 0.5 + Top.Hood.Position.X + 300.0, bluePrintCenter.Y + Top.Hood.Position.Y, 0);
            CutLine cutLine = new CutLine('A');
            cutLine.DrawCutLine((Wall.ExternalDiameter / 2.0) + Bottom.EdgeLength, bluePrintCenter, hoodCenter);
        }

        public void DrawWellAACut(Point3d StartPointAACut) 
        {
            Point3d topAndWallStartPoint = new Point3d(StartPointAACut.X + Top.Passarela, StartPointAACut.Y - Top.Thickness, 0);
            Point3d bottomStartPoint = new Point3d(topAndWallStartPoint.X, topAndWallStartPoint.Y - Wall.Heigth, 0);
            Point3d mislaStartPoint = new Point3d(bottomStartPoint.X + Wall.Thickness, bottomStartPoint.Y, 0);

            Top.DrawTopAACut(topAndWallStartPoint);
            Top.DrawTopAACutAnnotation(Bottom.EdgeLength);

            Wall.DrawAACut(topAndWallStartPoint);
            Wall.DrawAACutAnnotations(Bottom.EdgeLength);

            Bottom.DrawAACut(bottomStartPoint);
            Bottom.DrawAACutAnnotations();

            if (Misla.Length > 1 && Misla.Heigth > 1) 
            { 
                Misla.DrawMislaAACut(mislaStartPoint, Wall.InternalDiameter - 2 * Misla.Length);
                Misla.DrawAACutAnnotations();
            }
        }
    }
}
