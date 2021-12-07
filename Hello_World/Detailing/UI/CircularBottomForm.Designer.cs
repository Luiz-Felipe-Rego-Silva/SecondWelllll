
namespace Hello_World.Detailing.UI
{
    partial class CircularBottomForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CircularBottomForm));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_detail = new System.Windows.Forms.Button();
            this.rchtxt_title = new System.Windows.Forms.RichTextBox();
            this.msktxt_multiplier = new System.Windows.Forms.MaskedTextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_multiplier = new System.Windows.Forms.Label();
            this.lbl_unitAlert = new System.Windows.Forms.Label();
            this.grp_AdjacentElements = new System.Windows.Forms.GroupBox();
            this.msktxt_wallThickness = new System.Windows.Forms.MaskedTextBox();
            this.lbl_walSupThickness = new System.Windows.Forms.Label();
            this.grp_geometry = new System.Windows.Forms.GroupBox();
            this.msktxt_bottomThickness = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_edgeLength = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_externalDiameter = new System.Windows.Forms.MaskedTextBox();
            this.lbl_edgeLength = new System.Windows.Forms.Label();
            this.lbl_bottomThickness = new System.Windows.Forms.Label();
            this.lbl_externalDiameter = new System.Windows.Forms.Label();
            this.grp_steel = new System.Windows.Forms.GroupBox();
            this.cmbbx_horizontalBottomGauge = new System.Windows.Forms.ComboBox();
            this.cmbbx_verticalBottomGauge = new System.Windows.Forms.ComboBox();
            this.msktxt_horizontalSpacing = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_verticalSpacing = new System.Windows.Forms.MaskedTextBox();
            this.lbl_horizontalSpacing = new System.Windows.Forms.Label();
            this.lbl_horizontalGauge = new System.Windows.Forms.Label();
            this.lbl_verticalSpacing = new System.Windows.Forms.Label();
            this.lbl_verticalGauge = new System.Windows.Forms.Label();
            this.grp_Factors = new System.Windows.Forms.GroupBox();
            this.msktxt_anchorFactor = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_cover = new System.Windows.Forms.MaskedTextBox();
            this.lbl_anchorFactor = new System.Windows.Forms.Label();
            this.lbl_cover = new System.Windows.Forms.Label();
            this.grp_sideShapes = new System.Windows.Forms.GroupBox();
            this.chcbx_topHorizontalEngaged = new System.Windows.Forms.CheckBox();
            this.chcbx_topVerticalEngaged = new System.Windows.Forms.CheckBox();
            this.lbl_verticalConditions = new System.Windows.Forms.Label();
            this.lbl_horizontalConditions = new System.Windows.Forms.Label();
            this.lbl_horizontalEngaged = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chcbx_inf = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.grp_AdjacentElements.SuspendLayout();
            this.grp_geometry.SuspendLayout();
            this.grp_steel.SuspendLayout();
            this.grp_Factors.SuspendLayout();
            this.grp_sideShapes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(307, 355);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(76, 28);
            this.btn_cancel.TabIndex = 31;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_detail
            // 
            this.btn_detail.Location = new System.Drawing.Point(392, 355);
            this.btn_detail.Name = "btn_detail";
            this.btn_detail.Size = new System.Drawing.Size(71, 28);
            this.btn_detail.TabIndex = 29;
            this.btn_detail.Text = "Detalhar";
            this.btn_detail.UseVisualStyleBackColor = true;
            this.btn_detail.Click += new System.EventHandler(this.btn_detail_Click);
            // 
            // rchtxt_title
            // 
            this.rchtxt_title.Location = new System.Drawing.Point(53, 26);
            this.rchtxt_title.Multiline = false;
            this.rchtxt_title.Name = "rchtxt_title";
            this.rchtxt_title.ShowSelectionMargin = true;
            this.rchtxt_title.Size = new System.Drawing.Size(248, 16);
            this.rchtxt_title.TabIndex = 18;
            this.rchtxt_title.Text = "";
            // 
            // msktxt_multiplier
            // 
            this.msktxt_multiplier.Location = new System.Drawing.Point(379, 7);
            this.msktxt_multiplier.Name = "msktxt_multiplier";
            this.msktxt_multiplier.Size = new System.Drawing.Size(90, 20);
            this.msktxt_multiplier.TabIndex = 19;
            this.msktxt_multiplier.Text = "1";
            this.msktxt_multiplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(12, 29);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(35, 13);
            this.lbl_title.TabIndex = 26;
            this.lbl_title.Text = "Título";
            // 
            // lbl_multiplier
            // 
            this.lbl_multiplier.AutoSize = true;
            this.lbl_multiplier.Location = new System.Drawing.Point(307, 10);
            this.lbl_multiplier.Name = "lbl_multiplier";
            this.lbl_multiplier.Size = new System.Drawing.Size(66, 13);
            this.lbl_multiplier.TabIndex = 24;
            this.lbl_multiplier.Text = "Multiplicador";
            // 
            // lbl_unitAlert
            // 
            this.lbl_unitAlert.AutoSize = true;
            this.lbl_unitAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_unitAlert.ForeColor = System.Drawing.Color.Red;
            this.lbl_unitAlert.Location = new System.Drawing.Point(12, 7);
            this.lbl_unitAlert.Name = "lbl_unitAlert";
            this.lbl_unitAlert.Size = new System.Drawing.Size(284, 13);
            this.lbl_unitAlert.TabIndex = 22;
            this.lbl_unitAlert.Text = "Atenção: Todos os campos devem ser preenchidos em cm";
            // 
            // grp_AdjacentElements
            // 
            this.grp_AdjacentElements.Controls.Add(this.msktxt_wallThickness);
            this.grp_AdjacentElements.Controls.Add(this.lbl_walSupThickness);
            this.grp_AdjacentElements.Location = new System.Drawing.Point(307, 303);
            this.grp_AdjacentElements.Name = "grp_AdjacentElements";
            this.grp_AdjacentElements.Size = new System.Drawing.Size(157, 46);
            this.grp_AdjacentElements.TabIndex = 25;
            this.grp_AdjacentElements.TabStop = false;
            this.grp_AdjacentElements.Text = "Elementos Adjacentes";
            // 
            // msktxt_wallThickness
            // 
            this.msktxt_wallThickness.Location = new System.Drawing.Point(98, 16);
            this.msktxt_wallThickness.Name = "msktxt_wallThickness";
            this.msktxt_wallThickness.Size = new System.Drawing.Size(44, 20);
            this.msktxt_wallThickness.TabIndex = 3;
            this.msktxt_wallThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_walSupThickness
            // 
            this.lbl_walSupThickness.AutoSize = true;
            this.lbl_walSupThickness.Location = new System.Drawing.Point(9, 19);
            this.lbl_walSupThickness.Name = "lbl_walSupThickness";
            this.lbl_walSupThickness.Size = new System.Drawing.Size(44, 13);
            this.lbl_walSupThickness.TabIndex = 9;
            this.lbl_walSupThickness.Text = "Parede ";
            // 
            // grp_geometry
            // 
            this.grp_geometry.Controls.Add(this.msktxt_bottomThickness);
            this.grp_geometry.Controls.Add(this.msktxt_edgeLength);
            this.grp_geometry.Controls.Add(this.msktxt_externalDiameter);
            this.grp_geometry.Controls.Add(this.lbl_edgeLength);
            this.grp_geometry.Controls.Add(this.lbl_bottomThickness);
            this.grp_geometry.Controls.Add(this.lbl_externalDiameter);
            this.grp_geometry.Location = new System.Drawing.Point(307, 212);
            this.grp_geometry.Name = "grp_geometry";
            this.grp_geometry.Size = new System.Drawing.Size(162, 88);
            this.grp_geometry.TabIndex = 23;
            this.grp_geometry.TabStop = false;
            this.grp_geometry.Text = "Geometria";
            // 
            // msktxt_bottomThickness
            // 
            this.msktxt_bottomThickness.Location = new System.Drawing.Point(112, 35);
            this.msktxt_bottomThickness.Name = "msktxt_bottomThickness";
            this.msktxt_bottomThickness.Size = new System.Drawing.Size(44, 20);
            this.msktxt_bottomThickness.TabIndex = 10;
            this.msktxt_bottomThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_edgeLength
            // 
            this.msktxt_edgeLength.Location = new System.Drawing.Point(112, 59);
            this.msktxt_edgeLength.Name = "msktxt_edgeLength";
            this.msktxt_edgeLength.Size = new System.Drawing.Size(44, 20);
            this.msktxt_edgeLength.TabIndex = 2;
            this.msktxt_edgeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_externalDiameter
            // 
            this.msktxt_externalDiameter.Location = new System.Drawing.Point(112, 13);
            this.msktxt_externalDiameter.Name = "msktxt_externalDiameter";
            this.msktxt_externalDiameter.Size = new System.Drawing.Size(44, 20);
            this.msktxt_externalDiameter.TabIndex = 1;
            this.msktxt_externalDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_edgeLength
            // 
            this.lbl_edgeLength.AutoSize = true;
            this.lbl_edgeLength.Location = new System.Drawing.Point(6, 59);
            this.lbl_edgeLength.Name = "lbl_edgeLength";
            this.lbl_edgeLength.Size = new System.Drawing.Size(31, 13);
            this.lbl_edgeLength.TabIndex = 12;
            this.lbl_edgeLength.Text = "Abas";
            // 
            // lbl_bottomThickness
            // 
            this.lbl_bottomThickness.AutoSize = true;
            this.lbl_bottomThickness.Location = new System.Drawing.Point(6, 38);
            this.lbl_bottomThickness.Name = "lbl_bottomThickness";
            this.lbl_bottomThickness.Size = new System.Drawing.Size(56, 13);
            this.lbl_bottomThickness.TabIndex = 14;
            this.lbl_bottomThickness.Text = "Espessura";
            // 
            // lbl_externalDiameter
            // 
            this.lbl_externalDiameter.AutoSize = true;
            this.lbl_externalDiameter.Location = new System.Drawing.Point(6, 16);
            this.lbl_externalDiameter.Name = "lbl_externalDiameter";
            this.lbl_externalDiameter.Size = new System.Drawing.Size(88, 13);
            this.lbl_externalDiameter.TabIndex = 11;
            this.lbl_externalDiameter.Text = "Diâmetro Externo";
            // 
            // grp_steel
            // 
            this.grp_steel.Controls.Add(this.cmbbx_horizontalBottomGauge);
            this.grp_steel.Controls.Add(this.cmbbx_verticalBottomGauge);
            this.grp_steel.Controls.Add(this.msktxt_horizontalSpacing);
            this.grp_steel.Controls.Add(this.msktxt_verticalSpacing);
            this.grp_steel.Controls.Add(this.lbl_horizontalSpacing);
            this.grp_steel.Controls.Add(this.lbl_horizontalGauge);
            this.grp_steel.Controls.Add(this.lbl_verticalSpacing);
            this.grp_steel.Controls.Add(this.lbl_verticalGauge);
            this.grp_steel.Location = new System.Drawing.Point(307, 91);
            this.grp_steel.Name = "grp_steel";
            this.grp_steel.Size = new System.Drawing.Size(162, 121);
            this.grp_steel.TabIndex = 21;
            this.grp_steel.TabStop = false;
            this.grp_steel.Text = "Armadura";
            // 
            // cmbbx_horizontalBottomGauge
            // 
            this.cmbbx_horizontalBottomGauge.FormattingEnabled = true;
            this.cmbbx_horizontalBottomGauge.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0"});
            this.cmbbx_horizontalBottomGauge.Location = new System.Drawing.Point(96, 75);
            this.cmbbx_horizontalBottomGauge.Name = "cmbbx_horizontalBottomGauge";
            this.cmbbx_horizontalBottomGauge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbbx_horizontalBottomGauge.Size = new System.Drawing.Size(60, 21);
            this.cmbbx_horizontalBottomGauge.TabIndex = 3;
            this.cmbbx_horizontalBottomGauge.Text = "5.0";
            // 
            // cmbbx_verticalBottomGauge
            // 
            this.cmbbx_verticalBottomGauge.FormattingEnabled = true;
            this.cmbbx_verticalBottomGauge.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0"});
            this.cmbbx_verticalBottomGauge.Location = new System.Drawing.Point(96, 17);
            this.cmbbx_verticalBottomGauge.Name = "cmbbx_verticalBottomGauge";
            this.cmbbx_verticalBottomGauge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbbx_verticalBottomGauge.Size = new System.Drawing.Size(60, 21);
            this.cmbbx_verticalBottomGauge.TabIndex = 1;
            this.cmbbx_verticalBottomGauge.Text = "5.0";
            // 
            // msktxt_horizontalSpacing
            // 
            this.msktxt_horizontalSpacing.Location = new System.Drawing.Point(96, 97);
            this.msktxt_horizontalSpacing.Name = "msktxt_horizontalSpacing";
            this.msktxt_horizontalSpacing.Size = new System.Drawing.Size(60, 20);
            this.msktxt_horizontalSpacing.TabIndex = 4;
            this.msktxt_horizontalSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_verticalSpacing
            // 
            this.msktxt_verticalSpacing.Location = new System.Drawing.Point(96, 39);
            this.msktxt_verticalSpacing.Name = "msktxt_verticalSpacing";
            this.msktxt_verticalSpacing.Size = new System.Drawing.Size(60, 20);
            this.msktxt_verticalSpacing.TabIndex = 2;
            this.msktxt_verticalSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_horizontalSpacing
            // 
            this.lbl_horizontalSpacing.AutoSize = true;
            this.lbl_horizontalSpacing.Location = new System.Drawing.Point(6, 100);
            this.lbl_horizontalSpacing.Name = "lbl_horizontalSpacing";
            this.lbl_horizontalSpacing.Size = new System.Drawing.Size(72, 13);
            this.lbl_horizontalSpacing.TabIndex = 10;
            this.lbl_horizontalSpacing.Text = "Espaçamento";
            // 
            // lbl_horizontalGauge
            // 
            this.lbl_horizontalGauge.AutoSize = true;
            this.lbl_horizontalGauge.Location = new System.Drawing.Point(6, 78);
            this.lbl_horizontalGauge.Name = "lbl_horizontalGauge";
            this.lbl_horizontalGauge.Size = new System.Drawing.Size(54, 13);
            this.lbl_horizontalGauge.TabIndex = 9;
            this.lbl_horizontalGauge.Text = "Horizontal";
            // 
            // lbl_verticalSpacing
            // 
            this.lbl_verticalSpacing.AutoSize = true;
            this.lbl_verticalSpacing.Location = new System.Drawing.Point(6, 42);
            this.lbl_verticalSpacing.Name = "lbl_verticalSpacing";
            this.lbl_verticalSpacing.Size = new System.Drawing.Size(72, 13);
            this.lbl_verticalSpacing.TabIndex = 8;
            this.lbl_verticalSpacing.Text = "Espaçamento";
            // 
            // lbl_verticalGauge
            // 
            this.lbl_verticalGauge.AutoSize = true;
            this.lbl_verticalGauge.Location = new System.Drawing.Point(6, 20);
            this.lbl_verticalGauge.Name = "lbl_verticalGauge";
            this.lbl_verticalGauge.Size = new System.Drawing.Size(42, 13);
            this.lbl_verticalGauge.TabIndex = 7;
            this.lbl_verticalGauge.Text = "Vertical";
            // 
            // grp_Factors
            // 
            this.grp_Factors.Controls.Add(this.msktxt_anchorFactor);
            this.grp_Factors.Controls.Add(this.msktxt_cover);
            this.grp_Factors.Controls.Add(this.lbl_anchorFactor);
            this.grp_Factors.Controls.Add(this.lbl_cover);
            this.grp_Factors.Location = new System.Drawing.Point(307, 26);
            this.grp_Factors.Name = "grp_Factors";
            this.grp_Factors.Size = new System.Drawing.Size(162, 66);
            this.grp_Factors.TabIndex = 20;
            this.grp_Factors.TabStop = false;
            // 
            // msktxt_anchorFactor
            // 
            this.msktxt_anchorFactor.Location = new System.Drawing.Point(127, 37);
            this.msktxt_anchorFactor.Name = "msktxt_anchorFactor";
            this.msktxt_anchorFactor.Size = new System.Drawing.Size(29, 20);
            this.msktxt_anchorFactor.TabIndex = 2;
            this.msktxt_anchorFactor.Text = "34";
            this.msktxt_anchorFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_cover
            // 
            this.msktxt_cover.Location = new System.Drawing.Point(127, 13);
            this.msktxt_cover.Name = "msktxt_cover";
            this.msktxt_cover.Size = new System.Drawing.Size(29, 20);
            this.msktxt_cover.TabIndex = 1;
            this.msktxt_cover.Text = "5";
            this.msktxt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_anchorFactor
            // 
            this.lbl_anchorFactor.AutoSize = true;
            this.lbl_anchorFactor.Location = new System.Drawing.Point(6, 40);
            this.lbl_anchorFactor.Name = "lbl_anchorFactor";
            this.lbl_anchorFactor.Size = new System.Drawing.Size(103, 13);
            this.lbl_anchorFactor.TabIndex = 2;
            this.lbl_anchorFactor.Text = "Fator de Ancoragem";
            // 
            // lbl_cover
            // 
            this.lbl_cover.AutoSize = true;
            this.lbl_cover.Location = new System.Drawing.Point(6, 16);
            this.lbl_cover.Name = "lbl_cover";
            this.lbl_cover.Size = new System.Drawing.Size(60, 13);
            this.lbl_cover.TabIndex = 7;
            this.lbl_cover.Text = "Cobrimento";
            // 
            // grp_sideShapes
            // 
            this.grp_sideShapes.Controls.Add(this.chcbx_topHorizontalEngaged);
            this.grp_sideShapes.Controls.Add(this.chcbx_topVerticalEngaged);
            this.grp_sideShapes.Controls.Add(this.lbl_verticalConditions);
            this.grp_sideShapes.Controls.Add(this.lbl_horizontalConditions);
            this.grp_sideShapes.Controls.Add(this.lbl_horizontalEngaged);
            this.grp_sideShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_sideShapes.Location = new System.Drawing.Point(15, 273);
            this.grp_sideShapes.Name = "grp_sideShapes";
            this.grp_sideShapes.Size = new System.Drawing.Size(286, 104);
            this.grp_sideShapes.TabIndex = 32;
            this.grp_sideShapes.TabStop = false;
            this.grp_sideShapes.Text = "Engaste";
            // 
            // chcbx_topHorizontalEngaged
            // 
            this.chcbx_topHorizontalEngaged.AutoSize = true;
            this.chcbx_topHorizontalEngaged.Location = new System.Drawing.Point(148, 70);
            this.chcbx_topHorizontalEngaged.Name = "chcbx_topHorizontalEngaged";
            this.chcbx_topHorizontalEngaged.Size = new System.Drawing.Size(102, 17);
            this.chcbx_topHorizontalEngaged.TabIndex = 5;
            this.chcbx_topHorizontalEngaged.Text = "Parede Superior";
            this.chcbx_topHorizontalEngaged.UseVisualStyleBackColor = true;
            // 
            // chcbx_topVerticalEngaged
            // 
            this.chcbx_topVerticalEngaged.AutoSize = true;
            this.chcbx_topVerticalEngaged.Location = new System.Drawing.Point(148, 45);
            this.chcbx_topVerticalEngaged.Name = "chcbx_topVerticalEngaged";
            this.chcbx_topVerticalEngaged.Size = new System.Drawing.Size(102, 17);
            this.chcbx_topVerticalEngaged.TabIndex = 3;
            this.chcbx_topVerticalEngaged.Text = "Parede Superior";
            this.chcbx_topVerticalEngaged.UseVisualStyleBackColor = true;
            // 
            // lbl_verticalConditions
            // 
            this.lbl_verticalConditions.AutoSize = true;
            this.lbl_verticalConditions.Location = new System.Drawing.Point(6, 46);
            this.lbl_verticalConditions.Name = "lbl_verticalConditions";
            this.lbl_verticalConditions.Size = new System.Drawing.Size(45, 13);
            this.lbl_verticalConditions.TabIndex = 2;
            this.lbl_verticalConditions.Text = "Vertical:";
            // 
            // lbl_horizontalConditions
            // 
            this.lbl_horizontalConditions.AutoSize = true;
            this.lbl_horizontalConditions.Location = new System.Drawing.Point(6, 70);
            this.lbl_horizontalConditions.Name = "lbl_horizontalConditions";
            this.lbl_horizontalConditions.Size = new System.Drawing.Size(57, 13);
            this.lbl_horizontalConditions.TabIndex = 1;
            this.lbl_horizontalConditions.Text = "Horizontal:";
            // 
            // lbl_horizontalEngaged
            // 
            this.lbl_horizontalEngaged.AutoSize = true;
            this.lbl_horizontalEngaged.Location = new System.Drawing.Point(6, 16);
            this.lbl_horizontalEngaged.Name = "lbl_horizontalEngaged";
            this.lbl_horizontalEngaged.Size = new System.Drawing.Size(280, 13);
            this.lbl_horizontalEngaged.TabIndex = 0;
            this.lbl_horizontalEngaged.Text = "Marque, nas áreas abaixo, as regiões em que há engaste;";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(283, 199);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // chcbx_inf
            // 
            this.chcbx_inf.AutoSize = true;
            this.chcbx_inf.Location = new System.Drawing.Point(250, 108);
            this.chcbx_inf.Name = "chcbx_inf";
            this.chcbx_inf.Size = new System.Drawing.Size(15, 14);
            this.chcbx_inf.TabIndex = 34;
            this.chcbx_inf.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(278, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 35;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CircularBottomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 389);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chcbx_inf);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grp_sideShapes);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_detail);
            this.Controls.Add(this.rchtxt_title);
            this.Controls.Add(this.msktxt_multiplier);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_multiplier);
            this.Controls.Add(this.lbl_unitAlert);
            this.Controls.Add(this.grp_AdjacentElements);
            this.Controls.Add(this.grp_geometry);
            this.Controls.Add(this.grp_steel);
            this.Controls.Add(this.grp_Factors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CircularBottomForm";
            this.Text = "Detalhador de Fundos Circulares";
            this.grp_AdjacentElements.ResumeLayout(false);
            this.grp_AdjacentElements.PerformLayout();
            this.grp_geometry.ResumeLayout(false);
            this.grp_geometry.PerformLayout();
            this.grp_steel.ResumeLayout(false);
            this.grp_steel.PerformLayout();
            this.grp_Factors.ResumeLayout(false);
            this.grp_Factors.PerformLayout();
            this.grp_sideShapes.ResumeLayout(false);
            this.grp_sideShapes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_detail;
        private System.Windows.Forms.RichTextBox rchtxt_title;
        private System.Windows.Forms.MaskedTextBox msktxt_multiplier;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label lbl_multiplier;
        private System.Windows.Forms.Label lbl_unitAlert;
        private System.Windows.Forms.GroupBox grp_AdjacentElements;
        private System.Windows.Forms.MaskedTextBox msktxt_wallThickness;
        private System.Windows.Forms.Label lbl_walSupThickness;
        private System.Windows.Forms.GroupBox grp_geometry;
        private System.Windows.Forms.MaskedTextBox msktxt_bottomThickness;
        private System.Windows.Forms.MaskedTextBox msktxt_edgeLength;
        private System.Windows.Forms.MaskedTextBox msktxt_externalDiameter;
        private System.Windows.Forms.Label lbl_edgeLength;
        private System.Windows.Forms.Label lbl_bottomThickness;
        private System.Windows.Forms.Label lbl_externalDiameter;
        private System.Windows.Forms.GroupBox grp_steel;
        private System.Windows.Forms.ComboBox cmbbx_horizontalBottomGauge;
        private System.Windows.Forms.ComboBox cmbbx_verticalBottomGauge;
        private System.Windows.Forms.MaskedTextBox msktxt_horizontalSpacing;
        private System.Windows.Forms.MaskedTextBox msktxt_verticalSpacing;
        private System.Windows.Forms.Label lbl_horizontalSpacing;
        private System.Windows.Forms.Label lbl_horizontalGauge;
        private System.Windows.Forms.Label lbl_verticalSpacing;
        private System.Windows.Forms.Label lbl_verticalGauge;
        private System.Windows.Forms.GroupBox grp_Factors;
        private System.Windows.Forms.MaskedTextBox msktxt_anchorFactor;
        private System.Windows.Forms.MaskedTextBox msktxt_cover;
        private System.Windows.Forms.Label lbl_anchorFactor;
        private System.Windows.Forms.Label lbl_cover;
        private System.Windows.Forms.GroupBox grp_sideShapes;
        private System.Windows.Forms.Label lbl_horizontalEngaged;
        private System.Windows.Forms.CheckBox chcbx_topHorizontalEngaged;
        private System.Windows.Forms.CheckBox chcbx_topVerticalEngaged;
        private System.Windows.Forms.Label lbl_verticalConditions;
        private System.Windows.Forms.Label lbl_horizontalConditions;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chcbx_inf;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}