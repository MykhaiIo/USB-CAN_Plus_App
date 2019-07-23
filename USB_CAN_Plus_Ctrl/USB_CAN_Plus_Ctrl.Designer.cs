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
            this.components = new System.ComponentModel.Container();
            this.grpModule1 = new System.Windows.Forms.GroupBox();
            this.chkPowerDevice1 = new System.Windows.Forms.CheckBox();
            this.grpReadParamsMdl1 = new System.Windows.Forms.GroupBox();
            this.txtCurCurnt1 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPhaseCAVolt1 = new System.Windows.Forms.TextBox();
            this.txtPhaseBCVolt1 = new System.Windows.Forms.TextBox();
            this.txtPhaseABVolt1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTemperature1 = new System.Windows.Forms.TextBox();
            this.txtCurVolt1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpSetParamsMdl1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudOutCurntSI1 = new System.Windows.Forms.NumericUpDown();
            this.nudOutVoltSI1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSerialNo1 = new System.Windows.Forms.Label();
            this.btnConnect1 = new System.Windows.Forms.Button();
            this.grpModule2 = new System.Windows.Forms.GroupBox();
            this.chkPowerDevice2 = new System.Windows.Forms.CheckBox();
            this.grpReadParamsMdl2 = new System.Windows.Forms.GroupBox();
            this.txtCurCurnt2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTemperature2 = new System.Windows.Forms.TextBox();
            this.txtCurVolt2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPhaseCAVolt2 = new System.Windows.Forms.TextBox();
            this.txtPhaseBCVolt2 = new System.Windows.Forms.TextBox();
            this.txtPhaseABVolt2 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.grpSetParamsMdl2 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.nudOutCurntSI2 = new System.Windows.Forms.NumericUpDown();
            this.nudOutVoltSI2 = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tmrDisplayDeviceParams = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.tmrRefreshDeviceParams = new System.Windows.Forms.Timer(this.components);
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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpModule1
            // 
            this.grpModule1.Controls.Add(this.chkPowerDevice1);
            this.grpModule1.Controls.Add(this.grpReadParamsMdl1);
            this.grpModule1.Controls.Add(this.grpSetParamsMdl1);
            this.grpModule1.Location = new System.Drawing.Point(6, 10);
            this.grpModule1.Name = "grpModule1";
            this.grpModule1.Size = new System.Drawing.Size(397, 250);
            this.grpModule1.TabIndex = 1;
            this.grpModule1.TabStop = false;
            this.grpModule1.Text = "Модуль 1";
            // 
            // chkPowerDevice1
            // 
            this.chkPowerDevice1.AutoSize = true;
            this.chkPowerDevice1.Location = new System.Drawing.Point(7, 225);
            this.chkPowerDevice1.Name = "chkPowerDevice1";
            this.chkPowerDevice1.Size = new System.Drawing.Size(124, 17);
            this.chkPowerDevice1.TabIndex = 2;
            this.chkPowerDevice1.Text = "Підключити модуль";
            this.chkPowerDevice1.UseVisualStyleBackColor = true;
            this.chkPowerDevice1.CheckedChanged += new System.EventHandler(this.ChkPowerDevice1_CheckedChanged);
            // 
            // grpReadParamsMdl1
            // 
            this.grpReadParamsMdl1.Controls.Add(this.txtCurCurnt1);
            this.grpReadParamsMdl1.Controls.Add(this.label26);
            this.grpReadParamsMdl1.Controls.Add(this.label12);
            this.grpReadParamsMdl1.Controls.Add(this.label11);
            this.grpReadParamsMdl1.Controls.Add(this.label10);
            this.grpReadParamsMdl1.Controls.Add(this.txtPhaseCAVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.txtPhaseBCVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.txtPhaseABVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.label9);
            this.grpReadParamsMdl1.Controls.Add(this.txtTemperature1);
            this.grpReadParamsMdl1.Controls.Add(this.txtCurVolt1);
            this.grpReadParamsMdl1.Controls.Add(this.label8);
            this.grpReadParamsMdl1.Controls.Add(this.label7);
            this.grpReadParamsMdl1.Location = new System.Drawing.Point(7, 101);
            this.grpReadParamsMdl1.Name = "grpReadParamsMdl1";
            this.grpReadParamsMdl1.Size = new System.Drawing.Size(384, 111);
            this.grpReadParamsMdl1.TabIndex = 1;
            this.grpReadParamsMdl1.TabStop = false;
            this.grpReadParamsMdl1.Text = "Характеристики модуля";
            // 
            // txtCurCurnt1
            // 
            this.txtCurCurnt1.Location = new System.Drawing.Point(127, 45);
            this.txtCurCurnt1.Name = "txtCurCurnt1";
            this.txtCurCurnt1.Size = new System.Drawing.Size(66, 20);
            this.txtCurCurnt1.TabIndex = 19;
            this.txtCurCurnt1.TextChanged += new System.EventHandler(this.TxtCurCurnt1_TextChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(7, 48);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(38, 13);
            this.label26.TabIndex = 20;
            this.label26.Text = "Струм";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(301, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "CA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(301, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "BC";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(301, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "AB";
            // 
            // txtPhaseCAVolt1
            // 
            this.txtPhaseCAVolt1.Location = new System.Drawing.Point(323, 71);
            this.txtPhaseCAVolt1.Name = "txtPhaseCAVolt1";
            this.txtPhaseCAVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseCAVolt1.TabIndex = 16;
            this.txtPhaseCAVolt1.TextChanged += new System.EventHandler(this.TxtPhaseCAVolt1_TextChanged);
            // 
            // txtPhaseBCVolt1
            // 
            this.txtPhaseBCVolt1.Location = new System.Drawing.Point(323, 45);
            this.txtPhaseBCVolt1.Name = "txtPhaseBCVolt1";
            this.txtPhaseBCVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseBCVolt1.TabIndex = 15;
            this.txtPhaseBCVolt1.TextChanged += new System.EventHandler(this.TxtPhaseBCVolt1_TextChanged);
            // 
            // txtPhaseABVolt1
            // 
            this.txtPhaseABVolt1.Location = new System.Drawing.Point(323, 19);
            this.txtPhaseABVolt1.Name = "txtPhaseABVolt1";
            this.txtPhaseABVolt1.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseABVolt1.TabIndex = 14;
            this.txtPhaseABVolt1.TextChanged += new System.EventHandler(this.TxtPhaseABVolt1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(227, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Напруга фаз";
            // 
            // txtTemperature1
            // 
            this.txtTemperature1.Location = new System.Drawing.Point(127, 71);
            this.txtTemperature1.Name = "txtTemperature1";
            this.txtTemperature1.Size = new System.Drawing.Size(66, 20);
            this.txtTemperature1.TabIndex = 12;
            this.txtTemperature1.TextChanged += new System.EventHandler(this.TxtTemperature1_TextChanged);
            // 
            // txtCurVolt1
            // 
            this.txtCurVolt1.Location = new System.Drawing.Point(127, 19);
            this.txtCurVolt1.Name = "txtCurVolt1";
            this.txtCurVolt1.Size = new System.Drawing.Size(66, 20);
            this.txtCurVolt1.TabIndex = 10;
            this.txtCurVolt1.TextChanged += new System.EventHandler(this.TxtCurVolt1_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Температура модуля";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Напруга";
            // 
            // grpSetParamsMdl1
            // 
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
            this.grpSetParamsMdl1.Size = new System.Drawing.Size(384, 74);
            this.grpSetParamsMdl1.TabIndex = 0;
            this.grpSetParamsMdl1.TabStop = false;
            this.grpSetParamsMdl1.Text = "Параметри, що задаються";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(179, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "А";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(179, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "В";
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
            // lblSerialNo1
            // 
            this.lblSerialNo1.AutoSize = true;
            this.lblSerialNo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNo1.Location = new System.Drawing.Point(265, 265);
            this.lblSerialNo1.Name = "lblSerialNo1";
            this.lblSerialNo1.Size = new System.Drawing.Size(132, 20);
            this.lblSerialNo1.TabIndex = 21;
            this.lblSerialNo1.Text = "Серійний номер:";
            // 
            // btnConnect1
            // 
            this.btnConnect1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConnect1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnConnect1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnConnect1.ForeColor = System.Drawing.Color.Black;
            this.btnConnect1.Location = new System.Drawing.Point(3, 297);
            this.btnConnect1.Name = "btnConnect1";
            this.btnConnect1.Size = new System.Drawing.Size(823, 33);
            this.btnConnect1.TabIndex = 2;
            this.btnConnect1.Text = "Підключити";
            this.btnConnect1.UseVisualStyleBackColor = false;
            this.btnConnect1.Click += new System.EventHandler(this.BtnConnect1_Click);
            // 
            // grpModule2
            // 
            this.grpModule2.Controls.Add(this.chkPowerDevice2);
            this.grpModule2.Controls.Add(this.grpReadParamsMdl2);
            this.grpModule2.Controls.Add(this.grpSetParamsMdl2);
            this.grpModule2.Location = new System.Drawing.Point(425, 10);
            this.grpModule2.Name = "grpModule2";
            this.grpModule2.Size = new System.Drawing.Size(397, 250);
            this.grpModule2.TabIndex = 4;
            this.grpModule2.TabStop = false;
            this.grpModule2.Text = "Модуль 2";
            // 
            // chkPowerDevice2
            // 
            this.chkPowerDevice2.AutoSize = true;
            this.chkPowerDevice2.Location = new System.Drawing.Point(7, 225);
            this.chkPowerDevice2.Name = "chkPowerDevice2";
            this.chkPowerDevice2.Size = new System.Drawing.Size(124, 17);
            this.chkPowerDevice2.TabIndex = 3;
            this.chkPowerDevice2.Text = "Підключити модуль";
            this.chkPowerDevice2.UseVisualStyleBackColor = true;
            this.chkPowerDevice2.CheckedChanged += new System.EventHandler(this.ChkPowerDevice2_CheckedChanged);
            // 
            // grpReadParamsMdl2
            // 
            this.grpReadParamsMdl2.Controls.Add(this.txtCurCurnt2);
            this.grpReadParamsMdl2.Controls.Add(this.label17);
            this.grpReadParamsMdl2.Controls.Add(this.txtTemperature2);
            this.grpReadParamsMdl2.Controls.Add(this.txtCurVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.label18);
            this.grpReadParamsMdl2.Controls.Add(this.label27);
            this.grpReadParamsMdl2.Controls.Add(this.label13);
            this.grpReadParamsMdl2.Controls.Add(this.label14);
            this.grpReadParamsMdl2.Controls.Add(this.label15);
            this.grpReadParamsMdl2.Controls.Add(this.txtPhaseCAVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.txtPhaseBCVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.txtPhaseABVolt2);
            this.grpReadParamsMdl2.Controls.Add(this.label16);
            this.grpReadParamsMdl2.Location = new System.Drawing.Point(7, 101);
            this.grpReadParamsMdl2.Name = "grpReadParamsMdl2";
            this.grpReadParamsMdl2.Size = new System.Drawing.Size(384, 111);
            this.grpReadParamsMdl2.TabIndex = 1;
            this.grpReadParamsMdl2.TabStop = false;
            this.grpReadParamsMdl2.Text = "Характеристики модуля";
            // 
            // txtCurCurnt2
            // 
            this.txtCurCurnt2.Location = new System.Drawing.Point(127, 45);
            this.txtCurCurnt2.Name = "txtCurCurnt2";
            this.txtCurCurnt2.Size = new System.Drawing.Size(66, 20);
            this.txtCurCurnt2.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(7, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 31;
            this.label17.Text = "Струм";
            // 
            // txtTemperature2
            // 
            this.txtTemperature2.Location = new System.Drawing.Point(127, 71);
            this.txtTemperature2.Name = "txtTemperature2";
            this.txtTemperature2.Size = new System.Drawing.Size(66, 20);
            this.txtTemperature2.TabIndex = 29;
            // 
            // txtCurVolt2
            // 
            this.txtCurVolt2.Location = new System.Drawing.Point(127, 19);
            this.txtCurVolt2.Name = "txtCurVolt2";
            this.txtCurVolt2.Size = new System.Drawing.Size(66, 20);
            this.txtCurVolt2.TabIndex = 26;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(7, 74);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(114, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "Температура модуля";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(7, 22);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 13);
            this.label27.TabIndex = 27;
            this.label27.Text = "Напруга";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(301, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(21, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "CA";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(301, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "BC";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(301, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "AB";
            // 
            // txtPhaseCAVolt2
            // 
            this.txtPhaseCAVolt2.Location = new System.Drawing.Point(323, 71);
            this.txtPhaseCAVolt2.Name = "txtPhaseCAVolt2";
            this.txtPhaseCAVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseCAVolt2.TabIndex = 23;
            // 
            // txtPhaseBCVolt2
            // 
            this.txtPhaseBCVolt2.Location = new System.Drawing.Point(323, 46);
            this.txtPhaseBCVolt2.Name = "txtPhaseBCVolt2";
            this.txtPhaseBCVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseBCVolt2.TabIndex = 22;
            // 
            // txtPhaseABVolt2
            // 
            this.txtPhaseABVolt2.Location = new System.Drawing.Point(323, 21);
            this.txtPhaseABVolt2.Name = "txtPhaseABVolt2";
            this.txtPhaseABVolt2.Size = new System.Drawing.Size(46, 20);
            this.txtPhaseABVolt2.TabIndex = 21;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(227, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 20;
            this.label16.Text = "Напруга фаз";
            // 
            // grpSetParamsMdl2
            // 
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
            this.grpSetParamsMdl2.Size = new System.Drawing.Size(384, 74);
            this.grpSetParamsMdl2.TabIndex = 0;
            this.grpSetParamsMdl2.TabStop = false;
            this.grpSetParamsMdl2.Text = "Параметри, що задаються";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(179, 45);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "А";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(179, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "В";
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
            // tmrDisplayDeviceParams
            // 
            this.tmrDisplayDeviceParams.Enabled = true;
            this.tmrDisplayDeviceParams.Interval = 1000;
            this.tmrDisplayDeviceParams.Tick += new System.EventHandler(this.TmrDisplayDeviceParams_Tick);
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Items.AddRange(new object[] {
            "Активний перший модуль",
            "Активний другий модуль",
            "Активні обидва модулі"});
            this.cmbDevices.Location = new System.Drawing.Point(300, 12);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(263, 24);
            this.cmbDevices.TabIndex = 5;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.CmbDevices_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(11, 15);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(268, 16);
            this.label25.TabIndex = 6;
            this.label25.Text = "Оберіть зарядний модуль для взаємодії";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroProgressBar1);
            this.groupBox1.Controls.Add(this.btnConnect1);
            this.groupBox1.Controls.Add(this.lblSerialNo1);
            this.groupBox1.Controls.Add(this.grpModule1);
            this.groupBox1.Controls.Add(this.grpModule2);
            this.groupBox1.Location = new System.Drawing.Point(14, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 333);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // metroProgressBar1
            // 
            this.metroProgressBar1.Location = new System.Drawing.Point(364, 302);
            this.metroProgressBar1.Name = "metroProgressBar1";
            this.metroProgressBar1.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.metroProgressBar1.Size = new System.Drawing.Size(100, 23);
            this.metroProgressBar1.TabIndex = 23;
            // 
            // tmrRefreshDeviceParams
            // 
            this.tmrRefreshDeviceParams.Enabled = true;
            this.tmrRefreshDeviceParams.Interval = 30;
            this.tmrRefreshDeviceParams.Tick += new System.EventHandler(this.TmrRefreshDeviceParams_Tick);
            // 
            // USB_CAN_Plus_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 379);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.cmbDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "USB_CAN_Plus_Ctrl";
            this.Text = "USB-CAN Plus Controller";
            this.grpModule1.ResumeLayout(false);
            this.grpModule1.PerformLayout();
            this.grpReadParamsMdl1.ResumeLayout(false);
            this.grpReadParamsMdl1.PerformLayout();
            this.grpSetParamsMdl1.ResumeLayout(false);
            this.grpSetParamsMdl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutCurntSI1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutVoltSI1)).EndInit();
            this.grpModule2.ResumeLayout(false);
            this.grpModule2.PerformLayout();
            this.grpReadParamsMdl2.ResumeLayout(false);
            this.grpReadParamsMdl2.PerformLayout();
            this.grpSetParamsMdl2.ResumeLayout(false);
            this.grpSetParamsMdl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutCurntSI2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutVoltSI2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox txtPhaseCAVolt1;
        private System.Windows.Forms.TextBox txtPhaseBCVolt1;
        private System.Windows.Forms.TextBox txtPhaseABVolt1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTemperature1;
        private System.Windows.Forms.TextBox txtCurVolt1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpModule2;
        private System.Windows.Forms.GroupBox grpReadParamsMdl2;
        private System.Windows.Forms.GroupBox grpSetParamsMdl2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown nudOutCurntSI2;
        private System.Windows.Forms.NumericUpDown nudOutVoltSI2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblSerialNo1;
        private System.Windows.Forms.Timer tmrDisplayDeviceParams;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPhaseCAVolt2;
        private System.Windows.Forms.TextBox txtPhaseBCVolt2;
        private System.Windows.Forms.TextBox txtPhaseABVolt2;
        private System.Windows.Forms.Label label16;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar1;
        private System.Windows.Forms.CheckBox chkPowerDevice1;
        private System.Windows.Forms.CheckBox chkPowerDevice2;
        private System.Windows.Forms.Timer tmrRefreshDeviceParams;
        private System.Windows.Forms.TextBox txtCurCurnt1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtCurCurnt2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTemperature2;
        private System.Windows.Forms.TextBox txtCurVolt2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label27;
    }
}

