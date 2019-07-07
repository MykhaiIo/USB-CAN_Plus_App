namespace HEXtoFLOAT
{
    partial class HEXtoFLOAT
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.FLOATCurTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SICurVal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HEXCurTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SIVoltVal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.HEXVoltTextBox = new System.Windows.Forms.TextBox();
            this.FLOATVoltTextBox = new System.Windows.Forms.TextBox();
            this.bConvert = new System.Windows.Forms.Button();
            this.GetDataFromCAN = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FLOATCurTextBox
            // 
            this.FLOATCurTextBox.Location = new System.Drawing.Point(109, 58);
            this.FLOATCurTextBox.Name = "FLOATCurTextBox";
            this.FLOATCurTextBox.Size = new System.Drawing.Size(100, 20);
            this.FLOATCurTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SICurVal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.HEXCurTextBox);
            this.groupBox1.Controls.Add(this.FLOATCurTextBox);
            this.groupBox1.Location = new System.Drawing.Point(41, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "SI Unit";
            // 
            // SICurVal
            // 
            this.SICurVal.Location = new System.Drawing.Point(109, 98);
            this.SICurVal.Name = "SICurVal";
            this.SICurVal.Size = new System.Drawing.Size(100, 20);
            this.SICurVal.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Float value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hexadecimal value";
            // 
            // HEXCurTextBox
            // 
            this.HEXCurTextBox.Location = new System.Drawing.Point(109, 19);
            this.HEXCurTextBox.Name = "HEXCurTextBox";
            this.HEXCurTextBox.Size = new System.Drawing.Size(100, 20);
            this.HEXCurTextBox.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.SIVoltVal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.HEXVoltTextBox);
            this.groupBox2.Controls.Add(this.FLOATVoltTextBox);
            this.groupBox2.Location = new System.Drawing.Point(299, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 133);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tension";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "SI Unit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Float value";
            // 
            // SIVoltVal
            // 
            this.SIVoltVal.Location = new System.Drawing.Point(121, 98);
            this.SIVoltVal.Name = "SIVoltVal";
            this.SIVoltVal.Size = new System.Drawing.Size(100, 20);
            this.SIVoltVal.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hexadecimal value";
            // 
            // HEXVoltTextBox
            // 
            this.HEXVoltTextBox.Location = new System.Drawing.Point(121, 19);
            this.HEXVoltTextBox.Name = "HEXVoltTextBox";
            this.HEXVoltTextBox.Size = new System.Drawing.Size(100, 20);
            this.HEXVoltTextBox.TabIndex = 1;
            // 
            // FLOATVoltTextBox
            // 
            this.FLOATVoltTextBox.Location = new System.Drawing.Point(121, 58);
            this.FLOATVoltTextBox.Name = "FLOATVoltTextBox";
            this.FLOATVoltTextBox.Size = new System.Drawing.Size(100, 20);
            this.FLOATVoltTextBox.TabIndex = 0;
            // 
            // bConvert
            // 
            this.bConvert.Location = new System.Drawing.Point(244, 160);
            this.bConvert.Name = "bConvert";
            this.bConvert.Size = new System.Drawing.Size(75, 23);
            this.bConvert.TabIndex = 3;
            this.bConvert.Text = "Convert";
            this.bConvert.UseVisualStyleBackColor = true;
            this.bConvert.Click += new System.EventHandler(this.HEXtoFLOAT_Click);
            // 
            // GetDataFromCAN
            // 
            this.GetDataFromCAN.Location = new System.Drawing.Point(445, 160);
            this.GetDataFromCAN.Name = "GetDataFromCAN";
            this.GetDataFromCAN.Size = new System.Drawing.Size(75, 23);
            this.GetDataFromCAN.TabIndex = 4;
            this.GetDataFromCAN.Text = "Load";
            this.GetDataFromCAN.UseVisualStyleBackColor = true;
            this.GetDataFromCAN.Click += new System.EventHandler(this.GetDataFromCAN_Click);
            // 
            // HEXtoFLOAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 195);
            this.Controls.Add(this.GetDataFromCAN);
            this.Controls.Add(this.bConvert);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HEXtoFLOAT";
            this.Text = "HEXtoFLOAT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox FLOATCurTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox HEXCurTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox HEXVoltTextBox;
        private System.Windows.Forms.TextBox FLOATVoltTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bConvert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SICurVal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SIVoltVal;
        private System.Windows.Forms.Button GetDataFromCAN;
    }
}

