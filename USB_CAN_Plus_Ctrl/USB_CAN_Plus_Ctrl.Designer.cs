namespace USB_CAN_Plus_Ctrl
{
    partial class USB_CAN_Plus_Ctrl
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
            this.grpModule1 = new System.Windows.Forms.GroupBox();
            this.btnConnect1 = new System.Windows.Forms.Button();
            this.grpReadParamsMdl1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPhaseCVolt1 = new System.Windows.Forms.TextBox();
            this.txtPhaseBVolt1 = new System.Windows.Forms.TextBox();
            this.txtPhaseAVolt1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTemperature1 = new System.Windows.Forms.TextBox();
            this.txtCurVolt1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpSetParamsMdl1 = new System.Windows.Forms.GroupBox();
            this.txtOutCurntFP1 = new System.Windows.Forms.TextBox();
            this.txtOutVoltFP1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudOutCurntSI1 = new System.Windows.Forms.NumericUpDown();
            this.nudOutVoltSI1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpModule2 = new System.Windows.Forms.GroupBox();
            this.btnConnect2 = new System.Windows.Forms.Button();
            this.grpReadParamsMdl2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPhaseCVolt2 = new System.Windows.Forms.TextBox();
            this.txtPhaseBVolt2 = new System.Windows.Forms.TextBox();
            this.txtPhaseAVolt2 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTemperature2 = new System.Windows.Forms.TextBox();
            this.txtCurVolt2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.grpSetParamsMdl2 = new System.Windows.Forms.GroupBox();
            this.txtOutCurntFP2 = new System.Windows.Forms.TextBox();
            this.txtOutVoltFP2 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.nudOutCurntSI2 = new System.Windows.Forms.NumericUpDown();
            this.nudOutVoltSI2 = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblSerialNo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.grpModule1.SuspendLayout();
            this.grpReadParamsMdl1.SuspendLayout();
            this.grpSetParamsMdl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutCurntSI1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutVoltSI1)).BeginInit();
            this.grpModule2.SuspendLayout();
            this.grpReadParamsMdl2.SuspendLayout();
            this.grpSetParamsMdl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutCurntSI2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutVoltSI2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpModule1
            // 
            this.grpModule1.Controls.Add(this.btnConnect1);
            this.grpModule1.Controls.Add(this.grpReadParamsMdl1);
            this.grpModule1.Controls.Add(this.grpSetParamsMdl1);
            this.grpModule1.Location = new System.Drawing.Point(14, 21);
            this.grpModule1.Name = "grpModule1";
            this.grpModule1.Size = new System.Drawing.Size(561, 239);
            this.grpModule1.TabIndex = 1;
            this.grpModule1.TabStop = false;
            this.grpModule1.Text = "Модуль 1";
            // 
            // btnConnect1
            // 
            this.btnConnect1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnConnect1.Location = new System.Drawing.Point(3, 213);
            this.btnConnect1.Name = "btnConnect1";
            this.btnConnect1.Size = new System.Drawing.Size(555, 23);
            this.btnConnect1.TabIndex = 2;
            this.btnConnect1.Text = "Підключити";
            this.btnConnect1.UseVisualStyleBackColor = true;
            this.btnConnect1.Click += new System.EventHandler(this.BtnConnect1_Click);
            // 
            // grpReadParamsMdl1
            // 
            this.grpReadParamsMdl1.Controls.Add(this.lblSerialNo);
            this.grpReadParamsMdl1.Controls.Add(this.label12);
            this.grpReadParamsMdl1.Controls.Add(this.label11);
            this.grpReadParamsMdl1.Controls.Add(this.label10);
            this.grpReadParamsMdl1.Controls.Add(this.txtPhaseCVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.txtPhaseBVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.txtPhaseAVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.label9);
            this.grpReadParamsMdl1.Controls.Add(this.txtTemperature1);
            this.grpReadParamsMdl1.Controls.Add(this.txtCurVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.label8);
            this.grpReadParamsMdl1.Controls.Add(this.label7);
            this.grpReadParamsMdl1.Location = new System.Drawing.Point(7, 101);
            this.grpReadParamsMdl1.Name = "grpReadParamsMdl1";
            this.grpReadParamsMdl1.Size = new System.Drawing.Size(548, 100);
            this.grpReadParamsMdl1.TabIndex = 1;
            this.grpReadParamsMdl1.TabStop = false;
            this.grpReadParamsMdl1.Text = "Характеристики модуля";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(256, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "C";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(256, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "B";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(256, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "A";
            // 
            // txtPhaseCVolt1
            // 
            this.txtPhaseCVolt1.Location = new System.Drawing.Point(276, 69);
            this.txtPhaseCVolt1.Name = "txtPhaseCVolt1";
            this.txtPhaseCVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseCVolt1.TabIndex = 16;
            // 
            // txtPhaseBVolt1
            // 
            this.txtPhaseBVolt1.Location = new System.Drawing.Point(276, 43);
            this.txtPhaseBVolt1.Name = "txtPhaseBVolt1";
            this.txtPhaseBVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseBVolt1.TabIndex = 15;
            // 
            // txtPhaseAVolt1
            // 
            this.txtPhaseAVolt1.Location = new System.Drawing.Point(276, 17);
            this.txtPhaseAVolt1.Name = "txtPhaseAVolt1";
            this.txtPhaseAVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseAVolt1.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(182, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Напруга фаз";
            // 
            // txtTemperature1
            // 
            this.txtTemperature1.Location = new System.Drawing.Point(126, 54);
            this.txtTemperature1.Name = "txtTemperature1";
            this.txtTemperature1.Size = new System.Drawing.Size(46, 20);
            this.txtTemperature1.TabIndex = 12;
            // 
            // txtCurVolt1
            // 
            this.txtCurVolt1.Location = new System.Drawing.Point(127, 25);
            this.txtCurVolt1.Name = "txtCurVolt1";
            this.txtCurVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtCurVolt1.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Температура модуля";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Напруга";
            // 
            // grpSetParamsMdl1
            // 
            this.grpSetParamsMdl1.Controls.Add(this.button1);
            this.grpSetParamsMdl1.Controls.Add(this.txtOutCurntFP1);
            this.grpSetParamsMdl1.Controls.Add(this.txtOutVoltFP1);
            this.grpSetParamsMdl1.Controls.Add(this.label6);
            this.grpSetParamsMdl1.Controls.Add(this.label5);
            this.grpSetParamsMdl1.Controls.Add(this.nudOutCurntSI1);
            this.grpSetParamsMdl1.Controls.Add(this.nudOutVoltSI1);
            this.grpSetParamsMdl1.Controls.Add(this.label4);
            this.grpSetParamsMdl1.Controls.Add(this.label3);
            this.grpSetParamsMdl1.Controls.Add(this.label2);
            this.grpSetParamsMdl1.Controls.Add(this.label1);
            this.grpSetParamsMdl1.Location = new System.Drawing.Point(7, 20);
            this.grpSetParamsMdl1.Name = "grpSetParamsMdl1";
            this.grpSetParamsMdl1.Size = new System.Drawing.Size(548, 74);
            this.grpSetParamsMdl1.TabIndex = 0;
            this.grpSetParamsMdl1.TabStop = false;
            this.grpSetParamsMdl1.Text = "Параметри, що задаються";
            // 
            // txtOutCurntFP1
            // 
            this.txtOutCurntFP1.Location = new System.Drawing.Point(269, 40);
            this.txtOutCurntFP1.Name = "txtOutCurntFP1";
            this.txtOutCurntFP1.Size = new System.Drawing.Size(100, 20);
            this.txtOutCurntFP1.TabIndex = 9;
            // 
            // txtOutVoltFP1
            // 
            this.txtOutVoltFP1.Location = new System.Drawing.Point(269, 16);
            this.txtOutVoltFP1.Name = "txtOutVoltFP1";
            this.txtOutVoltFP1.Size = new System.Drawing.Size(100, 20);
            this.txtOutVoltFP1.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(179, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "мА";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(179, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "мВ";
            // 
            // nudOutCurntSI1
            // 
            this.nudOutCurntSI1.DecimalPlaces = 3;
            this.nudOutCurntSI1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudOutCurntSI1.Location = new System.Drawing.Point(113, 40);
            this.nudOutCurntSI1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudOutCurntSI1.Name = "nudOutCurntSI1";
            this.nudOutCurntSI1.Size = new System.Drawing.Size(60, 20);
            this.nudOutCurntSI1.TabIndex = 5;
            this.nudOutCurntSI1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudOutCurntSI1.ValueChanged += new System.EventHandler(this.NudOutCurntSI1_ValueChanged);
            // 
            // nudOutVoltSI1
            // 
            this.nudOutVoltSI1.Location = new System.Drawing.Point(113, 16);
            this.nudOutVoltSI1.Maximum = new decimal(new int[] {
            550,
            0,
            0,
            0});
            this.nudOutVoltSI1.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudOutVoltSI1.Name = "nudOutVoltSI1";
            this.nudOutVoltSI1.Size = new System.Drawing.Size(60, 20);
            this.nudOutVoltSI1.TabIndex = 4;
            this.nudOutVoltSI1.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudOutVoltSI1.ValueChanged += new System.EventHandler(this.NudOutVoltSI1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(71, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Iₒᵤₜ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uₒᵤₜ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Струм";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Напруга";
            // 
            // grpModule2
            // 
            this.grpModule2.Controls.Add(this.btnConnect2);
            this.grpModule2.Controls.Add(this.grpReadParamsMdl2);
            this.grpModule2.Controls.Add(this.grpSetParamsMdl2);
            this.grpModule2.Location = new System.Drawing.Point(14, 266);
            this.grpModule2.Name = "grpModule2";
            this.grpModule2.Size = new System.Drawing.Size(561, 239);
            this.grpModule2.TabIndex = 4;
            this.grpModule2.TabStop = false;
            this.grpModule2.Text = "Модуль 2";
            // 
            // btnConnect2
            // 
            this.btnConnect2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnConnect2.Location = new System.Drawing.Point(3, 213);
            this.btnConnect2.Name = "btnConnect2";
            this.btnConnect2.Size = new System.Drawing.Size(555, 23);
            this.btnConnect2.TabIndex = 2;
            this.btnConnect2.Text = "Підключити";
            this.btnConnect2.UseVisualStyleBackColor = true;
            // 
            // grpReadParamsMdl2
            // 
            this.grpReadParamsMdl2.Controls.Add(this.label13);
            this.grpReadParamsMdl2.Controls.Add(this.label14);
            this.grpReadParamsMdl2.Controls.Add(this.label15);
            this.grpReadParamsMdl2.Controls.Add(this.txtPhaseCVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.txtPhaseBVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.txtPhaseAVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.label16);
            this.grpReadParamsMdl2.Controls.Add(this.txtTemperature2);
            this.grpReadParamsMdl2.Controls.Add(this.txtCurVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.label17);
            this.grpReadParamsMdl2.Controls.Add(this.label18);
            this.grpReadParamsMdl2.Location = new System.Drawing.Point(7, 101);
            this.grpReadParamsMdl2.Name = "grpReadParamsMdl2";
            this.grpReadParamsMdl2.Size = new System.Drawing.Size(548, 100);
            this.grpReadParamsMdl2.TabIndex = 1;
            this.grpReadParamsMdl2.TabStop = false;
            this.grpReadParamsMdl2.Text = "Характеристики модуля";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(273, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "C";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(273, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "B";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(273, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "A";
            // 
            // txtPhaseCVolt2
            // 
            this.txtPhaseCVolt2.Location = new System.Drawing.Point(293, 69);
            this.txtPhaseCVolt2.Name = "txtPhaseCVolt2";
            this.txtPhaseCVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseCVolt2.TabIndex = 16;
            // 
            // txtPhaseBVolt2
            // 
            this.txtPhaseBVolt2.Location = new System.Drawing.Point(293, 43);
            this.txtPhaseBVolt2.Name = "txtPhaseBVolt2";
            this.txtPhaseBVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseBVolt2.TabIndex = 15;
            // 
            // txtPhaseAVolt2
            // 
            this.txtPhaseAVolt2.Location = new System.Drawing.Point(293, 17);
            this.txtPhaseAVolt2.Name = "txtPhaseAVolt2";
            this.txtPhaseAVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseAVolt2.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(195, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Напруга фаз";
            // 
            // txtTemperature2
            // 
            this.txtTemperature2.Location = new System.Drawing.Point(126, 54);
            this.txtTemperature2.Name = "txtTemperature2";
            this.txtTemperature2.Size = new System.Drawing.Size(46, 20);
            this.txtTemperature2.TabIndex = 12;
            // 
            // txtCurVolt2
            // 
            this.txtCurVolt2.Location = new System.Drawing.Point(127, 25);
            this.txtCurVolt2.Name = "txtCurVolt2";
            this.txtCurVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtCurVolt2.TabIndex = 10;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(114, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Температура модуля";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(7, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Напруга";
            // 
            // grpSetParamsMdl2
            // 
            this.grpSetParamsMdl2.Controls.Add(this.txtOutCurntFP2);
            this.grpSetParamsMdl2.Controls.Add(this.txtOutVoltFP2);
            this.grpSetParamsMdl2.Controls.Add(this.label19);
            this.grpSetParamsMdl2.Controls.Add(this.label20);
            this.grpSetParamsMdl2.Controls.Add(this.nudOutCurntSI2);
            this.grpSetParamsMdl2.Controls.Add(this.nudOutVoltSI2);
            this.grpSetParamsMdl2.Controls.Add(this.label21);
            this.grpSetParamsMdl2.Controls.Add(this.label22);
            this.grpSetParamsMdl2.Controls.Add(this.label23);
            this.grpSetParamsMdl2.Controls.Add(this.label24);
            this.grpSetParamsMdl2.Location = new System.Drawing.Point(7, 20);
            this.grpSetParamsMdl2.Name = "grpSetParamsMdl2";
            this.grpSetParamsMdl2.Size = new System.Drawing.Size(548, 74);
            this.grpSetParamsMdl2.TabIndex = 0;
            this.grpSetParamsMdl2.TabStop = false;
            this.grpSetParamsMdl2.Text = "Параметри, що задаються";
            // 
            // txtOutCurntFP2
            // 
            this.txtOutCurntFP2.Location = new System.Drawing.Point(269, 40);
            this.txtOutCurntFP2.Name = "txtOutCurntFP2";
            this.txtOutCurntFP2.Size = new System.Drawing.Size(100, 20);
            this.txtOutCurntFP2.TabIndex = 9;
            // 
            // txtOutVoltFP2
            // 
            this.txtOutVoltFP2.Location = new System.Drawing.Point(269, 16);
            this.txtOutVoltFP2.Name = "txtOutVoltFP2";
            this.txtOutVoltFP2.Size = new System.Drawing.Size(100, 20);
            this.txtOutVoltFP2.TabIndex = 8;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(179, 45);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(22, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "мА";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(179, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(22, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "мВ";
            // 
            // nudOutCurntSI2
            // 
            this.nudOutCurntSI2.DecimalPlaces = 3;
            this.nudOutCurntSI2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudOutCurntSI2.Location = new System.Drawing.Point(113, 40);
            this.nudOutCurntSI2.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudOutCurntSI2.Name = "nudOutCurntSI2";
            this.nudOutCurntSI2.Size = new System.Drawing.Size(60, 20);
            this.nudOutCurntSI2.TabIndex = 5;
            this.nudOutCurntSI2.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // nudOutVoltSI2
            // 
            this.nudOutVoltSI2.Location = new System.Drawing.Point(113, 16);
            this.nudOutVoltSI2.Maximum = new decimal(new int[] {
            550,
            0,
            0,
            0});
            this.nudOutVoltSI2.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudOutVoltSI2.Name = "nudOutVoltSI2";
            this.nudOutVoltSI2.Size = new System.Drawing.Size(60, 20);
            this.nudOutVoltSI2.TabIndex = 4;
            this.nudOutVoltSI2.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(71, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 20);
            this.label21.TabIndex = 3;
            this.label21.Text = "Iₒᵤₜ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(71, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(36, 20);
            this.label22.TabIndex = 2;
            this.label22.Text = "Uₒᵤₜ";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(7, 42);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(38, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "Струм";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(7, 20);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "Напруга";
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNo.Location = new System.Drawing.Point(331, 43);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(132, 20);
            this.lblSerialNo.TabIndex = 21;
            this.lblSerialNo.Text = "Серійний номер:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(459, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Test button";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // USB_CAN_Plus_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 514);
            this.Controls.Add(this.grpModule2);
            this.Controls.Add(this.grpModule1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "USB_CAN_Plus_Ctrl";
            this.Text = "USB-CAN Plus Controller";
            this.grpModule1.ResumeLayout(false);
            this.grpReadParamsMdl1.ResumeLayout(false);
            this.grpReadParamsMdl1.PerformLayout();
            this.grpSetParamsMdl1.ResumeLayout(false);
            this.grpSetParamsMdl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutCurntSI1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutVoltSI1)).EndInit();
            this.grpModule2.ResumeLayout(false);
            this.grpReadParamsMdl2.ResumeLayout(false);
            this.grpReadParamsMdl2.PerformLayout();
            this.grpSetParamsMdl2.ResumeLayout(false);
            this.grpSetParamsMdl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutCurntSI2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutVoltSI2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpModule1;
        private System.Windows.Forms.GroupBox grpSetParamsMdl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudOutCurntSI1;
        private System.Windows.Forms.NumericUpDown nudOutVoltSI1;
        private System.Windows.Forms.Button btnConnect1;
        private System.Windows.Forms.GroupBox grpReadParamsMdl1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPhaseCVolt1;
        private System.Windows.Forms.TextBox txtPhaseBVolt1;
        private System.Windows.Forms.TextBox txtPhaseAVolt1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTemperature1;
        private System.Windows.Forms.TextBox txtCurVolt1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOutCurntFP1;
        private System.Windows.Forms.TextBox txtOutVoltFP1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpModule2;
        private System.Windows.Forms.Button btnConnect2;
        private System.Windows.Forms.GroupBox grpReadParamsMdl2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPhaseCVolt2;
        private System.Windows.Forms.TextBox txtPhaseBVolt2;
        private System.Windows.Forms.TextBox txtPhaseAVolt2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTemperature2;
        private System.Windows.Forms.TextBox txtCurVolt2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox grpSetParamsMdl2;
        private System.Windows.Forms.TextBox txtOutCurntFP2;
        private System.Windows.Forms.TextBox txtOutVoltFP2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown nudOutCurntSI2;
        private System.Windows.Forms.NumericUpDown nudOutVoltSI2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblSerialNo;
        private System.Windows.Forms.Button button1;
    }
}

