using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace WellDetailing.WellDetailer.Entities
{
    public class Terrain
    {
        public double Nivel { get; private set; }
        private Point3d[] TerrainInCut;
        private const double TerrainLength = 50.0;
        private const double TerrainHeigth = 20.0;
        
        public Terrain(double nivel) 
        {
            Nivel = nivel;
            TerrainInCut = new Point3d[8];
        }

        public void DrawTerrainInCut(Point3d startPoint, double externalLength)
        {
            DoPointsAtTerrainInCut(startPoint, externalLength);
            DrawHatchTerrain();
        }

        private void DoPointsAtTerrainInCut(Point3d startPoint, double externalLength)
        {
            TerrainInCut[0] = startPoint;
            TerrainInCut[1] = new Point3d(TerrainInCut[0].X - TerrainLength, TerrainInCut[0].Y, 0);
            TerrainInCut[2] = new Point3d(TerrainInCut[1].X, TerrainInCut[1].Y - TerrainHeigth, 0);
            TerrainInCut[3] = new Point3d(TerrainInCut[2].X + TerrainLength, TerrainInCut[2].Y, 0);
            TerrainInCut[4] = new Point3d(TerrainInCut[0].X + externalLength, TerrainInCut[0].Y, 0);
            TerrainInCut[5] = new Point3d(TerrainInCut[4].X + TerrainLength, TerrainInCut[4].Y, 0);
            TerrainInCut[6] = new Point3d(TerrainInCut[5].X, TerrainInCut[5].Y - TerrainHeigth, 0);
            TerrainInCut[7] = new Point3d(TerrainInCut[6].X - TerrainLength, TerrainInCut[6].Y, 0);
        }
        private void DrawHatchTerrain()
        {

        }

    }
}