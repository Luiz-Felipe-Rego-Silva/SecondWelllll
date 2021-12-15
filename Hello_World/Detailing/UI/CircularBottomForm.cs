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
                SetCutInfo();
                Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para o desenho:");
                _bottomDetailing.DrawDetailment(BasePoint);
            }
            catch (Exception) { }
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
            bool[] verticalConditions = new bool[4] { chcBx_NegativeUpEngaged.Checked, chcBx_NegativeDownEngaged.Checked, chcBx_PositiveUpEngaged.Checked, chcBx_PositiveDownEngaged.Checked };
            _bottomDetailing.SetEngagedVerticalConditions(verticalConditions);
            bool[] horizontalConditions = new bool[4] { chcBx_NegativeLeftEngaged.Checked, chcBx_NegativaRightEngaged.Checked, chcBx_PositiveLeftEngaged.Checked, chcBx_PositiveRigthEngaged.Checked };
            _bottomDetailing.SetEngagedHorizontalConditions(horizontalConditions);
        }
        private void SetBorderShape()
        {
            double supWallThickness, infWallThickness;
            supWallThickness = Convert.ToDouble(msktxt_TopThickness.Text);
            infWallThickness = Convert.ToDouble(msktxt_InfThickness.Text);
            _bottomDetailing.SetBorderThickness(supWallThickness, infWallThickness);
            double diameter, thickness, edgeLength;
            diameter = Convert.ToDouble(msktxt_externalDiameter.Text);
            thickness = Convert.ToDouble(msktxt_bottomThickness.Text);
            edgeLength = Convert.ToDouble(msktxt_edgeLength.Text);
            _bottomDetailing.SetGeometryInfo(diameter, thickness, edgeLength);
        }
        private void SetCutInfo()
        {
            bool[] conditions = new bool[4];
            conditions[0] = chcBx_VerticalUpLeft.Checked;
            conditions[1] = chcBx_VerticalUpRigth.Checked;
            conditions[2] = chcBx_VerticalDownLeft.Checked;
            conditions[3] = chcBx_VerticalDownRigth.Checked;
            _bottomDetailing.SetBorderShape(conditions);
        }

    }
}
