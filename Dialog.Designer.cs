namespace TidsBok
{
    partial class Dialog
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
            this.btnExecute = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSB2 = new System.Windows.Forms.RadioButton();
            this.rbSB1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNoOfSelectedPat = new System.Windows.Forms.Label();
            this.cbMarkAll = new System.Windows.Forms.CheckBox();
            this.clbPatientList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnExecute.Location = new System.Drawing.Point(65, 200);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(58, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Kör";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(19, 45);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(153, 20);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.CloseUp += new System.EventHandler(this.dtpStartDate_CloseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Välj maskin";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(19, 95);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(153, 20);
            this.dtpEndDate.TabIndex = 5;
            this.dtpEndDate.CloseUp += new System.EventHandler(this.dtpEndDate_CloseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Startdatum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Slutdatum";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSB2);
            this.groupBox1.Controls.Add(this.rbSB1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnExecute);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 249);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Välj startdatum till och med slutdatum";
            // 
            // rbSB2
            // 
            this.rbSB2.AutoSize = true;
            this.rbSB2.Location = new System.Drawing.Point(106, 162);
            this.rbSB2.Name = "rbSB2";
            this.rbSB2.Size = new System.Drawing.Size(45, 17);
            this.rbSB2.TabIndex = 9;
            this.rbSB2.TabStop = true;
            this.rbSB2.Text = "SB2";
            this.rbSB2.UseVisualStyleBackColor = true;
            this.rbSB2.CheckedChanged += new System.EventHandler(this.rbSB2_CheckedChanged);
            // 
            // rbSB1
            // 
            this.rbSB1.AutoSize = true;
            this.rbSB1.Location = new System.Drawing.Point(35, 162);
            this.rbSB1.Name = "rbSB1";
            this.rbSB1.Size = new System.Drawing.Size(45, 17);
            this.rbSB1.TabIndex = 8;
            this.rbSB1.TabStop = true;
            this.rbSB1.Text = "SB1";
            this.rbSB1.UseVisualStyleBackColor = true;
            this.rbSB1.CheckedChanged += new System.EventHandler(this.rbSB1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblNoOfSelectedPat);
            this.groupBox2.Controls.Add(this.cbMarkAll);
            this.groupBox2.Controls.Add(this.clbPatientList);
            this.groupBox2.Location = new System.Drawing.Point(213, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 327);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patientlista";
            // 
            // lblNoOfSelectedPat
            // 
            this.lblNoOfSelectedPat.AutoSize = true;
            this.lblNoOfSelectedPat.Location = new System.Drawing.Point(97, 301);
            this.lblNoOfSelectedPat.Name = "lblNoOfSelectedPat";
            this.lblNoOfSelectedPat.Size = new System.Drawing.Size(49, 13);
            this.lblNoOfSelectedPat.TabIndex = 10;
            this.lblNoOfSelectedPat.Text = "Antal pat";
            // 
            // cbMarkAll
            // 
            this.cbMarkAll.AutoSize = true;
            this.cbMarkAll.Checked = true;
            this.cbMarkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMarkAll.Location = new System.Drawing.Point(7, 300);
            this.cbMarkAll.Name = "cbMarkAll";
            this.cbMarkAll.Size = new System.Drawing.Size(84, 17);
            this.cbMarkAll.TabIndex = 1;
            this.cbMarkAll.Text = "Markera alla";
            this.cbMarkAll.UseVisualStyleBackColor = true;
            this.cbMarkAll.CheckedChanged += new System.EventHandler(this.cbMarkAll_CheckedChanged);
            // 
            // clbPatientList
            // 
            this.clbPatientList.CheckOnClick = true;
            this.clbPatientList.FormattingEnabled = true;
            this.clbPatientList.Location = new System.Drawing.Point(6, 19);
            this.clbPatientList.Name = "clbPatientList";
            this.clbPatientList.Size = new System.Drawing.Size(166, 274);
            this.clbPatientList.TabIndex = 0;
            this.clbPatientList.SelectedIndexChanged += new System.EventHandler(this.clbPatientList_SelectedIndexChanged);
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 362);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tidsbokning (av Erik Fura v0.4)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbPatientList;
        private System.Windows.Forms.RadioButton rbSB2;
        private System.Windows.Forms.RadioButton rbSB1;
        private System.Windows.Forms.CheckBox cbMarkAll;
        private System.Windows.Forms.Label lblNoOfSelectedPat;
    }
}