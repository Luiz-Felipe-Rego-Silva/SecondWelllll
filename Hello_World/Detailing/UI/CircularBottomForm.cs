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
            _bottomDetailing = new CircularBottomDetailing();
        }
        private void btn_detail_Click(object sender, EventArgs e)
        {
            SetArmorBottom();
            SetBorderCondition();
            Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para desenho:");
            _bottomDetailing.DrawDetailment(BasePoint);
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
            _bottomDetailing.SetBorderConditions(chcbx_topHorizontalEngaged.Checked, chcbx_topVerticalEngaged.Checked);
        }
        private void SetBorderShape() 
        {
            double supWallThickness;
            supWallThickness = Convert.ToDouble(msktxt_wallThickness.Text);
            _bottomDetailing.SetBorderThickness(supWallThickness);
            
            double diameter, thickness, edgeLength;
            diameter = Convert.ToDouble(msktxt_externalDiameter.Text);
            thickness = Convert.ToDouble(msktxt_bottomThickness.Text);
            edgeLength = Convert.ToDouble(msktxt_edgeLength.Text);
        }

    }
}
