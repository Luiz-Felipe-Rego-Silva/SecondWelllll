
namespace AutoDetailers.UI
{
    partial class TopForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopForm));
            this.picBox_hood = new System.Windows.Forms.PictureBox();
            this.picBox_gaps = new System.Windows.Forms.PictureBox();
            this.lbl_instruction = new System.Windows.Forms.Label();
            this.msktxt_posX = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_posY = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_insideDiameter = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_horizontalGap = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_verticalGap = new System.Windows.Forms.MaskedTextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_hood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_gaps)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox_hood
            // 
            this.picBox_hood.Image = ((System.Drawing.Image)(resources.GetObject("picBox_hood.Image")));
            this.picBox_hood.Location = new System.Drawing.Point(6, 48);
            this.picBox_hood.Margin = new System.Windows.Forms.Padding(4);
            this.picBox_hood.Name = "picBox_hood";
            this.picBox_hood.Size = new System.Drawing.Size(280, 220);
            this.picBox_hood.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_hood.TabIndex = 0;
            this.picBox_hood.TabStop = false;
            // 
            // picBox_gaps
            // 
            this.picBox_gaps.Image = ((System.Drawing.Image)(resources.GetObject("picBox_gaps.Image")));
            this.picBox_gaps.Location = new System.Drawing.Point(303, 49);
            this.picBox_gaps.Margin = new System.Windows.Forms.Padding(4);
            this.picBox_gaps.Name = "picBox_gaps";
            this.picBox_gaps.Size = new System.Drawing.Size(256, 219);
            this.picBox_gaps.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_gaps.TabIndex = 1;
            this.picBox_gaps.TabStop = false;
            // 
            // lbl_instruction
            // 
            this.lbl_instruction.AutoSize = true;
            this.lbl_instruction.Location = new System.Drawing.Point(20, 19);
            this.lbl_instruction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_instruction.Name = "lbl_instruction";
            this.lbl_instruction.Size = new System.Drawing.Size(254, 19);
            this.lbl_instruction.TabIndex = 2;
            this.lbl_instruction.Text = "Insira os dados relativos à tampa em cm:";
            // 
            // msktxt_posX
            // 
            this.msktxt_posX.Location = new System.Drawing.Point(39, 233);
            this.msktxt_posX.Margin = new System.Windows.Forms.Padding(4);
            this.msktxt_posX.Name = "msktxt_posX";
            this.msktxt_posX.Size = new System.Drawing.Size(53, 26);
            this.msktxt_posX.TabIndex = 3;
            this.msktxt_posX.Text = "0";
            // 
            // msktxt_posY
            // 
            this.msktxt_posY.Location = new System.Drawing.Point(223, 185);
            this.msktxt_posY.Margin = new System.Windows.Forms.Padding(4);
            this.msktxt_posY.Name = "msktxt_posY";
            this.msktxt_posY.Size = new System.Drawing.Size(53, 26);
            this.msktxt_posY.TabIndex = 4;
            this.msktxt_posY.Text = "-15";
            // 
            // msktxt_insideDiameter
            // 
            this.msktxt_insideDiameter.Location = new System.Drawing.Point(258, 94);
            this.msktxt_insideDiameter.Margin = new System.Windows.Forms.Padding(4);
            this.msktxt_insideDiameter.Name = "msktxt_insideDiameter";
            this.msktxt_insideDiameter.Size = new System.Drawing.Size(37, 26);
            this.msktxt_insideDiameter.TabIndex = 5;
            this.msktxt_insideDiameter.Text = "100";
            // 
            // msktxt_horizontalGap
            // 
            this.msktxt_horizontalGap.Location = new System.Drawing.Point(384, 58);
            this.msktxt_horizontalGap.Margin = new System.Windows.Forms.Padding(4);
            this.msktxt_horizontalGap.Name = "msktxt_horizontalGap";
            this.msktxt_horizontalGap.Size = new System.Drawing.Size(48, 26);
            this.msktxt_horizontalGap.TabIndex = 6;
            this.msktxt_horizontalGap.Text = "10";
            // 
            // msktxt_verticalGap
            // 
            this.msktxt_verticalGap.Location = new System.Drawing.Point(510, 158);
            this.msktxt_verticalGap.Margin = new System.Windows.Forms.Padding(4);
            this.msktxt_verticalGap.Name = "msktxt_verticalGap";
            this.msktxt_verticalGap.Size = new System.Drawing.Size(49, 26);
            this.msktxt_verticalGap.TabIndex = 7;
            this.msktxt_verticalGap.Text = "10";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(447, 276);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(112, 34);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Salvar";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(327, 276);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(112, 34);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // TopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 321);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.msktxt_verticalGap);
            this.Controls.Add(this.msktxt_horizontalGap);
            this.Controls.Add(this.msktxt_insideDiameter);
            this.Controls.Add(this.msktxt_posY);
            this.Controls.Add(this.msktxt_posX);
            this.Controls.Add(this.lbl_instruction);
            this.Controls.Add(this.picBox_gaps);
            this.Controls.Add(this.picBox_hood);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TopForm";
            this.Text = "TopForm";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_hood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_gaps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_hood;
        private System.Windows.Forms.PictureBox picBox_gaps;
        private System.Windows.Forms.Label lbl_instruction;
        private System.Windows.Forms.MaskedTextBox msktxt_posX;
        private System.Windows.Forms.MaskedTextBox msktxt_posY;
        private System.Windows.Forms.MaskedTextBox msktxt_insideDiameter;
        private System.Windows.Forms.MaskedTextBox msktxt_horizontalGap;
        private System.Windows.Forms.MaskedTextBox msktxt_verticalGap;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
    }
}