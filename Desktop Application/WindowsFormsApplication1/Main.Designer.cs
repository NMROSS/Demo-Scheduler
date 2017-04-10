namespace WindowsFormsApplication1
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.listBoxPapers = new System.Windows.Forms.ListBox();
            this.listBoxPotentialDemos = new System.Windows.Forms.ListBox();
            this.listBoxDemos = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.buttonNewPaper = new System.Windows.Forms.Button();
            this.buttonNewDemo = new System.Windows.Forms.Button();
            this.buttonDemoUnassign = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonDemoAssign = new System.Windows.Forms.Button();
            this.buttonEditDemo = new System.Windows.Forms.Button();
            this.listBoxPDHours = new System.Windows.Forms.ListBox();
            this.listBoxADHours = new System.Windows.Forms.ListBox();
            this.buttonPrintPaper = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxTimes = new System.Windows.Forms.PictureBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.timerLoading = new System.Windows.Forms.Timer(this.components);
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.buttonColour = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimes)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 49);
            this.label3.TabIndex = 8;
            this.label3.Text = "Computer Science Demo Selector";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(179, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 49);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // listBoxPapers
            // 
            this.listBoxPapers.FormattingEnabled = true;
            this.listBoxPapers.Items.AddRange(new object[] {
            "<< Add Paper >>"});
            this.listBoxPapers.Location = new System.Drawing.Point(12, 86);
            this.listBoxPapers.Name = "listBoxPapers";
            this.listBoxPapers.Size = new System.Drawing.Size(120, 472);
            this.listBoxPapers.TabIndex = 17;
            this.listBoxPapers.SelectedIndexChanged += new System.EventHandler(this.listBoxPapers_SelectedIndexChanged);
            // 
            // listBoxPotentialDemos
            // 
            this.listBoxPotentialDemos.FormattingEnabled = true;
            this.listBoxPotentialDemos.Items.AddRange(new object[] {
            "<< Add Demo >>"});
            this.listBoxPotentialDemos.Location = new System.Drawing.Point(489, 86);
            this.listBoxPotentialDemos.Name = "listBoxPotentialDemos";
            this.listBoxPotentialDemos.Size = new System.Drawing.Size(108, 433);
            this.listBoxPotentialDemos.TabIndex = 18;
            this.listBoxPotentialDemos.SelectedIndexChanged += new System.EventHandler(this.listBoxPotentialDemos_SelectedIndexChanged);
            // 
            // listBoxDemos
            // 
            this.listBoxDemos.FormattingEnabled = true;
            this.listBoxDemos.Location = new System.Drawing.Point(626, 86);
            this.listBoxDemos.Name = "listBoxDemos";
            this.listBoxDemos.Size = new System.Drawing.Size(98, 472);
            this.listBoxDemos.TabIndex = 19;
            this.listBoxDemos.SelectedIndexChanged += new System.EventHandler(this.listBoxDemos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(187, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Monday";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(244, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Tuesday";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(296, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Wednesday";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(361, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Thursday";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(426, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 65;
            this.label6.Text = "Friday";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(145, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 68;
            this.label9.Text = "8am";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(145, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 94;
            this.label10.Text = "9am";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(138, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 95;
            this.label7.Text = "10am";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(138, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 96;
            this.label8.Text = "11am";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(138, 257);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 97;
            this.label11.Text = "12pm";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(145, 295);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 98;
            this.label12.Text = "1pm";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(145, 332);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 99;
            this.label13.Text = "2pm";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(145, 375);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 100;
            this.label14.Text = "3pm";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(145, 411);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 101;
            this.label15.Text = "4pm";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(145, 452);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(27, 13);
            this.label16.TabIndex = 102;
            this.label16.Text = "5pm";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(145, 491);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 13);
            this.label17.TabIndex = 103;
            this.label17.Text = "6pm";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(145, 530);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 104;
            this.label18.Text = "7pm";
            // 
            // buttonNewPaper
            // 
            this.buttonNewPaper.Location = new System.Drawing.Point(12, 572);
            this.buttonNewPaper.Name = "buttonNewPaper";
            this.buttonNewPaper.Size = new System.Drawing.Size(120, 40);
            this.buttonNewPaper.TabIndex = 105;
            this.buttonNewPaper.Text = "Add Paper";
            this.buttonNewPaper.UseVisualStyleBackColor = true;
            this.buttonNewPaper.Click += new System.EventHandler(this.buttonNewPaper_Click);
            // 
            // buttonNewDemo
            // 
            this.buttonNewDemo.Location = new System.Drawing.Point(489, 527);
            this.buttonNewDemo.Name = "buttonNewDemo";
            this.buttonNewDemo.Size = new System.Drawing.Size(120, 40);
            this.buttonNewDemo.TabIndex = 106;
            this.buttonNewDemo.Text = "Add Demo";
            this.buttonNewDemo.UseVisualStyleBackColor = true;
            this.buttonNewDemo.Click += new System.EventHandler(this.buttonNewDemo_Click);
            // 
            // buttonDemoUnassign
            // 
            this.buttonDemoUnassign.Enabled = false;
            this.buttonDemoUnassign.Location = new System.Drawing.Point(626, 573);
            this.buttonDemoUnassign.Name = "buttonDemoUnassign";
            this.buttonDemoUnassign.Size = new System.Drawing.Size(120, 40);
            this.buttonDemoUnassign.TabIndex = 107;
            this.buttonDemoUnassign.Text = " Unassign Demo";
            this.buttonDemoUnassign.UseVisualStyleBackColor = true;
            this.buttonDemoUnassign.Click += new System.EventHandler(this.buttonDemoUnassign_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(44, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 108;
            this.label19.Text = "Papers";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(508, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 13);
            this.label20.TabIndex = 109;
            this.label20.Text = "Potential Demos";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(638, 70);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(86, 13);
            this.label21.TabIndex = 110;
            this.label21.Text = "Assigned Demos";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(12, 618);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 40);
            this.buttonRemove.TabIndex = 111;
            this.buttonRemove.Text = "Remove Paper";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonDemoAssign
            // 
            this.buttonDemoAssign.Enabled = false;
            this.buttonDemoAssign.Location = new System.Drawing.Point(489, 573);
            this.buttonDemoAssign.Name = "buttonDemoAssign";
            this.buttonDemoAssign.Size = new System.Drawing.Size(120, 40);
            this.buttonDemoAssign.TabIndex = 112;
            this.buttonDemoAssign.Text = " Assign Demo";
            this.buttonDemoAssign.UseVisualStyleBackColor = true;
            this.buttonDemoAssign.Click += new System.EventHandler(this.buttonDemoAssign_Click);
            // 
            // buttonEditDemo
            // 
            this.buttonEditDemo.Enabled = false;
            this.buttonEditDemo.Location = new System.Drawing.Point(489, 619);
            this.buttonEditDemo.Name = "buttonEditDemo";
            this.buttonEditDemo.Size = new System.Drawing.Size(120, 40);
            this.buttonEditDemo.TabIndex = 113;
            this.buttonEditDemo.Text = "Demo Infomation";
            this.buttonEditDemo.UseVisualStyleBackColor = true;
            this.buttonEditDemo.Click += new System.EventHandler(this.buttonEditDemo_Click);
            // 
            // listBoxPDHours
            // 
            this.listBoxPDHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPDHours.ForeColor = System.Drawing.Color.LimeGreen;
            this.listBoxPDHours.FormattingEnabled = true;
            this.listBoxPDHours.Location = new System.Drawing.Point(596, 86);
            this.listBoxPDHours.Name = "listBoxPDHours";
            this.listBoxPDHours.Size = new System.Drawing.Size(24, 433);
            this.listBoxPDHours.TabIndex = 114;
            // 
            // listBoxADHours
            // 
            this.listBoxADHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxADHours.ForeColor = System.Drawing.Color.LimeGreen;
            this.listBoxADHours.FormattingEnabled = true;
            this.listBoxADHours.Location = new System.Drawing.Point(723, 86);
            this.listBoxADHours.Name = "listBoxADHours";
            this.listBoxADHours.Size = new System.Drawing.Size(24, 472);
            this.listBoxADHours.TabIndex = 115;
            // 
            // buttonPrintPaper
            // 
            this.buttonPrintPaper.Location = new System.Drawing.Point(627, 619);
            this.buttonPrintPaper.Name = "buttonPrintPaper";
            this.buttonPrintPaper.Size = new System.Drawing.Size(120, 40);
            this.buttonPrintPaper.TabIndex = 116;
            this.buttonPrintPaper.Text = "Print Timetable";
            this.buttonPrintPaper.UseVisualStyleBackColor = true;
            this.buttonPrintPaper.Click += new System.EventHandler(this.buttonPrintPaper_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::WindowsFormsApplication1.Properties.Resources.Key;
            this.pictureBox2.Location = new System.Drawing.Point(17, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(262, 41);
            this.pictureBox2.TabIndex = 117;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBoxTimes
            // 
            this.pictureBoxTimes.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBoxTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxTimes.Location = new System.Drawing.Point(179, 86);
            this.pictureBoxTimes.Name = "pictureBoxTimes";
            this.pictureBoxTimes.Size = new System.Drawing.Size(300, 480);
            this.pictureBoxTimes.TabIndex = 20;
            this.pictureBoxTimes.TabStop = false;
            this.pictureBoxTimes.Click += new System.EventHandler(this.PictureBoxTimes_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(11, 62);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 13);
            this.label22.TabIndex = 118;
            this.label22.Text = "Assigned Lab";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(108, 62);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 13);
            this.label23.TabIndex = 119;
            this.label23.Text = "Unassigned Lab";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(215, 62);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 13);
            this.label24.TabIndex = 120;
            this.label24.Text = "Selected Lab";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(182, 572);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 86);
            this.groupBox1.TabIndex = 121;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key";
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLoading.Image")));
            this.pictureBoxLoading.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLoading.InitialImage")));
            this.pictureBoxLoading.Location = new System.Drawing.Point(492, 419);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(101, 97);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLoading.TabIndex = 122;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // timerLoading
            // 
            this.timerLoading.Interval = 1000;
            this.timerLoading.Tick += new System.EventHandler(this.timerLoading_Tick);
            // 
            // buttonColour
            // 
            this.buttonColour.Location = new System.Drawing.Point(626, 18);
            this.buttonColour.Name = "buttonColour";
            this.buttonColour.Size = new System.Drawing.Size(120, 40);
            this.buttonColour.TabIndex = 123;
            this.buttonColour.Text = "Change Back Colour";
            this.buttonColour.UseVisualStyleBackColor = true;
            this.buttonColour.Click += new System.EventHandler(this.buttonColour_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(759, 671);
            this.Controls.Add(this.buttonColour);
            this.Controls.Add(this.pictureBoxLoading);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPrintPaper);
            this.Controls.Add(this.buttonEditDemo);
            this.Controls.Add(this.listBoxADHours);
            this.Controls.Add(this.listBoxPDHours);
            this.Controls.Add(this.buttonDemoAssign);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.buttonDemoUnassign);
            this.Controls.Add(this.buttonNewDemo);
            this.Controls.Add(this.buttonNewPaper);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxTimes);
            this.Controls.Add(this.listBoxDemos);
            this.Controls.Add(this.listBoxPotentialDemos);
            this.Controls.Add(this.listBoxPapers);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label3);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox listBoxPapers;
        private System.Windows.Forms.ListBox listBoxPotentialDemos;
        private System.Windows.Forms.ListBox listBoxDemos;
        private System.Windows.Forms.PictureBox pictureBoxTimes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button buttonNewPaper;
        private System.Windows.Forms.Button buttonNewDemo;
        private System.Windows.Forms.Button buttonDemoUnassign;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonDemoAssign;
        private System.Windows.Forms.Button buttonEditDemo;
        private System.Windows.Forms.ListBox listBoxPDHours;
        private System.Windows.Forms.ListBox listBoxADHours;
        private System.Windows.Forms.Button buttonPrintPaper;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.Timer timerLoading;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.Button buttonColour;
    }
}