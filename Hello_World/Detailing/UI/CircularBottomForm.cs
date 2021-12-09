using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hello_World.Detailing.UI
{
    public partial class CircularBottomForm : Form
    {
        private CircularBottomDetailing _bottomDetailing;
        public CircularBottomForm()
        {
            InitializeComponent();

        }
        private void btn_detail_Click(object sender, EventArgs e)
        {
            try
            {
                 _bottomDetailing = new CircularBottomDetailing();
                 _bottomDetailing.Title = rchtxt_title.Text;
                 SetArmorBottom();
                 SetBorderCondition();
                 SetBorderShape();
                 Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para desenho:");
                 _bottomDetailing.DrawDetailment(BasePoint);
            }
            catch(Exception) { }
        }
        private void SetArmorBottom() 
        {
            double gaugeX, gaugeY, spacingX, spacingY, cover, anchorFactor;
            gaugeX = Convert.ToDouble(cmbbx_horizontalBottomGauge.Text);
            gaugeY = Convert.ToDouble(cmbbx_verticalBottomGauge.Text);
            spacingX = Convert.ToDouble(msktxt_horizontalSpacing.Text);
            spacingY = Convert.ToDouble(msktxt_verticalSpacing.Text);
            cover = Convert.ToDouble(msktxt_cover.Text);
            anchorFactor = Convert.ToDouble(msktxt_anchorFactor.Text);
            _bottomDetailing.SetArmorInfo(gaugeX, gaugeY, spacingX, spacingY, anchorFactor, cover);

        }
        private void SetBorderCondition() 
        {

        }
        private void SetBorderShape() 
        {
            double supWallThickness;
            supWallThickness = Convert.ToDouble(msktxt_elem1Thickness.Text);
            _bottomDetailing.SetBorderThickness(supWallThickness);
            double diameter, thickness, edgeLength;
            diameter = Convert.ToDouble(msktxt_externalDiameter.Text);
            thickness = Convert.ToDouble(msktxt_bottomThickness.Text);
            edgeLength = Convert.ToDouble(msktxt_edgeLength.Text);
            _bottomDetailing.SetGeometryInfo(diameter, thickness, edgeLength, supWallThickness);
        }
        private void SetCutInfo() 
        {
            double elem1 = Convert.ToDouble(msktxt_elem1Thickness.Text);
            double elem2 = Convert.ToDouble(msktxt_elem2Thickness.Text);
            double elem3 = Convert.ToDouble(msktxt_elem3Thickness.Text);
            double elem4 = Convert.ToDouble(msktxt_elem4Thickness.Text);
        }

    }
}
