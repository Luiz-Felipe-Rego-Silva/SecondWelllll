
namespace Hello_World.UI
{
    partial class MainWellForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWellForm));
            this.picBox_supWell = new System.Windows.Forms.PictureBox();
            this.lbl_instruction = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.msktxt_topThickness = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_bottomThickness = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_wallHeigth = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_sideLength = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_insideDiameter = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_wallThickness = new System.Windows.Forms.MaskedTextBox();
            this.lbl_insideDiameter = new System.Windows.Forms.Label();
            this.lbl_wallThickness = new System.Windows.Forms.Label();
            this.lbl_steel = new System.Windows.Forms.Label();
            this.btn_misla = new System.Windows.Forms.Button();
            this.btn_edge = new System.Windows.Forms.Button();
            this.btn_top = new System.Windows.Forms.Button();
            this.btn_doDetail = new System.Windows.Forms.Button();
            this.btn_steel = new System.Windows.Forms.Button();
            this.lbl_passarela = new System.Windows.Forms.Label();
            this.msktxt_passarela = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_supWell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox_supWell
            // 
            this.picBox_supWell.Image = ((System.Drawing.Image)(resources.GetObject("picBox_supWell.Image")));
            this.picBox_supWell.Location = new System.Drawing.Point(305, 61);
            this.picBox_supWell.Margin = new System.Windows.Forms.Padding(4);
            this.picBox_supWell.Name = "picBox_supWell";
            this.picBox_supWell.Size = new System.Drawing.Size(198, 163);
            this.picBox_supWell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_supWell.TabIndex = 1;
            this.picBox_supWell.TabStop = false;
            // 
            // lbl_instruction
            // 
            this.lbl_instruction.AutoSize = true;
            this.lbl_instruction.Location = new System.Drawing.Point(13, 13);
            this.lbl_instruction.Name = "lbl_instruction";
            this.lbl_instruction.Size = new System.Drawing.Size(323, 19);
            this.lbl_instruction.TabIndex = 2;
            this.lbl_instruction.Text = "Insira abaixo as medidas e detalhes do poço em cm:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 280);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // msktxt_topThickness
            // 
            this.msktxt_topThickness.Location = new System.Drawing.Point(236, 103);
            this.msktxt_topThickness.Name = "msktxt_topThickness";
            this.msktxt_topThickness.Size = new System.Drawing.Size(43, 26);
            this.msktxt_topThickness.TabIndex = 6;
            this.msktxt_topThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_bottomThickness
            // 
            this.msktxt_bottomThickness.Location = new System.Drawing.Point(236, 297);
            this.msktxt_bottomThickness.Name = "msktxt_bottomThickness";
            this.msktxt_bottomThickness.Size = new System.Drawing.Size(43, 26);
            this.msktxt_bottomThickness.TabIndex = 7;
            this.msktxt_bottomThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_wallHeigth
            // 
            this.msktxt_wallHeigth.Location = new System.Drawing.Point(236, 198);
            this.msktxt_wallHeigth.Name = "msktxt_wallHeigth";
            this.msktxt_wallHeigth.Size = new System.Drawing.Size(43, 26);
            this.msktxt_wallHeigth.TabIndex = 8;
            this.msktxt_wallHeigth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_sideLength
            // 
            this.msktxt_sideLength.Location = new System.Drawing.Point(51, 47);
            this.msktxt_sideLength.Name = "msktxt_sideLength";
            this.msktxt_sideLength.Size = new System.Drawing.Size(43, 26);
            this.msktxt_sideLength.TabIndex = 9;
            this.msktxt_sideLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_insideDiameter
            // 
            this.msktxt_insideDiameter.Location = new System.Drawing.Point(460, 231);
            this.msktxt_insideDiameter.Name = "msktxt_insideDiameter";
            this.msktxt_insideDiameter.Size = new System.Drawing.Size(43, 26);
            this.msktxt_insideDiameter.TabIndex = 10;
            this.msktxt_insideDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_wallThickness
            // 
            this.msktxt_wallThickness.Location = new System.Drawing.Point(460, 263);
            this.msktxt_wallThickness.Name = "msktxt_wallThickness";
            this.msktxt_wallThickness.Size = new System.Drawing.Size(43, 26);
            this.msktxt_wallThickness.TabIndex = 11;
            this.msktxt_wallThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_insideDiameter
            // 
            this.lbl_insideDiameter.AutoSize = true;
            this.lbl_insideDiameter.Location = new System.Drawing.Point(332, 234);
            this.lbl_insideDiameter.Name = "lbl_insideDiameter";
            this.lbl_insideDiameter.Size = new System.Drawing.Size(110, 19);
            this.lbl_insideDiameter.TabIndex = 12;
            this.lbl_insideDiameter.Text = "Diâmetro interno";
            // 
            // lbl_wallThickness
            // 
            this.lbl_wallThickness.AutoSize = true;
            this.lbl_wallThickness.Location = new System.Drawing.Point(372, 263);
            this.lbl_wallThickness.Name = "lbl_wallThickness";
            this.lbl_wallThickness.Size = new System.Drawing.Size(70, 19);
            this.lbl_wallThickness.TabIndex = 13;
            this.lbl_wallThickness.Text = "Espessura";
            // 
            // lbl_steel
            // 
            this.lbl_steel.AutoSize = true;
            this.lbl_steel.ForeColor = System.Drawing.Color.Red;
            this.lbl_steel.Location = new System.Drawing.Point(315, 313);
            this.lbl_steel.Name = "lbl_steel";
            this.lbl_steel.Size = new System.Drawing.Size(194, 19);
            this.lbl_steel.TabIndex = 14;
            this.lbl_steel.Text = "Atenção: Detalhar armaduras !";
            // 
            // btn_misla
            // 
            this.btn_misla.Location = new System.Drawing.Point(17, 396);
            this.btn_misla.Name = "btn_misla";
            this.btn_misla.Size = new System.Drawing.Size(75, 23);
            this.btn_misla.TabIndex = 15;
            this.btn_misla.Text = "Misula";
            this.btn_misla.UseVisualStyleBackColor = true;
            this.btn_misla.Click += new System.EventHandler(this.btn_misla_Click);
            // 
            // btn_edge
            // 
            this.btn_edge.Location = new System.Drawing.Point(99, 396);
            this.btn_edge.Name = "btn_edge";
            this.btn_edge.Size = new System.Drawing.Size(75, 23);
            this.btn_edge.TabIndex = 16;
            this.btn_edge.Text = "Abas";
            this.btn_edge.UseVisualStyleBackColor = true;
            this.btn_edge.Click += new System.EventHandler(this.btn_edge_Click);
            // 
            // btn_top
            // 
            this.btn_top.Location = new System.Drawing.Point(181, 395);
            this.btn_top.Name = "btn_top";
            this.btn_top.Size = new System.Drawing.Size(75, 23);
            this.btn_top.TabIndex = 17;
            this.btn_top.Text = "Tampa";
            this.btn_top.UseVisualStyleBackColor = true;
            this.btn_top.Click += new System.EventHandler(this.btn_top_Click);
            // 
            // btn_doDetail
            // 
            this.btn_doDetail.Location = new System.Drawing.Point(427, 395);
            this.btn_doDetail.Name = "btn_doDetail";
            this.btn_doDetail.Size = new System.Drawing.Size(75, 23);
            this.btn_doDetail.TabIndex = 18;
            this.btn_doDetail.Text = "Inserir";
            this.btn_doDetail.UseVisualStyleBackColor = true;
            this.btn_doDetail.Click += new System.EventHandler(this.btn_doDetail_Click);
            // 
            // btn_steel
            // 
            this.btn_steel.Location = new System.Drawing.Point(336, 395);
            this.btn_steel.Name = "btn_steel";
            this.btn_steel.Size = new System.Drawing.Size(85, 23);
            this.btn_steel.TabIndex = 19;
            this.btn_steel.Text = "Armadura";
            this.btn_steel.UseVisualStyleBackColor = true;
            this.btn_steel.Click += new System.EventHandler(this.btn_steel_Click);
            // 
            // lbl_passarela
            // 
            this.lbl_passarela.AutoSize = true;
            this.lbl_passarela.Location = new System.Drawing.Point(13, 347);
            this.lbl_passarela.Name = "lbl_passarela";
            this.lbl_passarela.Size = new System.Drawing.Size(133, 19);
            this.lbl_passarela.TabIndex = 21;
            this.lbl_passarela.Text = "Passarela/Pingadeira";
            // 
            // msktxt_passarela
            // 
            this.msktxt_passarela.Location = new System.Drawing.Point(164, 344);
            this.msktxt_passarela.Name = "msktxt_passarela";
            this.msktxt_passarela.Size = new System.Drawing.Size(43, 26);
            this.msktxt_passarela.TabIndex = 20;
            this.msktxt_passarela.Text = "0";
            this.msktxt_passarela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainWellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 430);
            this.Controls.Add(this.lbl_passarela);
            this.Controls.Add(this.msktxt_passarela);
            this.Controls.Add(this.btn_steel);
            this.Controls.Add(this.btn_doDetail);
            this.Controls.Add(this.btn_top);
            this.Controls.Add(this.btn_edge);
            this.Controls.Add(this.btn_misla);
            this.Controls.Add(this.lbl_steel);
            this.Controls.Add(this.lbl_wallThickness);
            this.Controls.Add(this.lbl_insideDiameter);
            this.Controls.Add(this.msktxt_wallThickness);
            this.Controls.Add(this.msktxt_insideDiameter);
            this.Controls.Add(this.msktxt_sideLength);
            this.Controls.Add(this.msktxt_wallHeigth);
            this.Controls.Add(this.msktxt_bottomThickness);
            this.Controls.Add(this.msktxt_topThickness);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_instruction);
            this.Controls.Add(this.picBox_supWell);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWellForm";
            this.Text = "MainWellForm";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_supWell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picBox_supWell;
        private System.Windows.Forms.Label lbl_instruction;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MaskedTextBox msktxt_topThickness;
        private System.Windows.Forms.MaskedTextBox msktxt_bottomThickness;
        private System.Windows.Forms.MaskedTextBox msktxt_wallHeigth;
        private System.Windows.Forms.MaskedTextBox msktxt_sideLength;
        private System.Windows.Forms.MaskedTextBox msktxt_insideDiameter;
        private System.Windows.Forms.MaskedTextBox msktxt_wallThickness;
        private System.Windows.Forms.Label lbl_insideDiameter;
        private System.Windows.Forms.Label lbl_wallThickness;
        private System.Windows.Forms.Label lbl_steel;
        private System.Windows.Forms.Button btn_misla;
        private System.Windows.Forms.Button btn_edge;
        private System.Windows.Forms.Button btn_top;
        private System.Windows.Forms.Button btn_doDetail;
        private System.Windows.Forms.Button btn_steel;
        private System.Windows.Forms.Label lbl_passarela;
        private System.Windows.Forms.MaskedTextBox msktxt_passarela;
    }
}