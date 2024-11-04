namespace DVLD.Applications.Local_Driving_License
{
    partial class frmListLocalDrivingLicesnseApplications
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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.editApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.sechduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechduleVisionTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechduleWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechduleStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.issueDrivingLicenseFirstTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLDLA = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.editApplicationToolStripMenuItem,
            this.deleteApplicationToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cancelApplicationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.sechduleToolStripMenuItem,
            this.toolStripSeparator1,
            this.issueDrivingLicenseFirstTimeToolStripMenuItem,
            this.toolStripMenuItem4,
            this.showLicenseToolStripMenuItem,
            this.toolStripMenuItem5,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(311, 356);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // showApplicationDetailsToolStripMenuItem
            // 
            this.showApplicationDetailsToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.showApplicationDetailsToolStripMenuItem.Name = "showApplicationDetailsToolStripMenuItem";
            this.showApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.showApplicationDetailsToolStripMenuItem.Text = "&Show Application Details";
            this.showApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(307, 6);
            // 
            // editApplicationToolStripMenuItem
            // 
            this.editApplicationToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editApplicationToolStripMenuItem.Name = "editApplicationToolStripMenuItem";
            this.editApplicationToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.editApplicationToolStripMenuItem.Text = "&Edit Application";
            this.editApplicationToolStripMenuItem.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // deleteApplicationToolStripMenuItem
            // 
            this.deleteApplicationToolStripMenuItem.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.deleteApplicationToolStripMenuItem.Name = "deleteApplicationToolStripMenuItem";
            this.deleteApplicationToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.deleteApplicationToolStripMenuItem.Text = "&Delete Application";
            this.deleteApplicationToolStripMenuItem.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(307, 6);
            // 
            // cancelApplicationToolStripMenuItem
            // 
            this.cancelApplicationToolStripMenuItem.Image = global::DVLD.Properties.Resources.Delete_32;
            this.cancelApplicationToolStripMenuItem.Name = "cancelApplicationToolStripMenuItem";
            this.cancelApplicationToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.cancelApplicationToolStripMenuItem.Text = "&Cancel Application";
            this.cancelApplicationToolStripMenuItem.Click += new System.EventHandler(this.cancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(307, 6);
            // 
            // sechduleToolStripMenuItem
            // 
            this.sechduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sechduleVisionTestToolStripMenuItem,
            this.sechduleWrittenTestToolStripMenuItem,
            this.sechduleStreetTestToolStripMenuItem});
            this.sechduleToolStripMenuItem.Image = global::DVLD.Properties.Resources.Schedule_Test_32;
            this.sechduleToolStripMenuItem.Name = "sechduleToolStripMenuItem";
            this.sechduleToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.sechduleToolStripMenuItem.Text = "Sechdule &Tests";
            // 
            // sechduleVisionTestToolStripMenuItem
            // 
            this.sechduleVisionTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.sechduleVisionTestToolStripMenuItem.Name = "sechduleVisionTestToolStripMenuItem";
            this.sechduleVisionTestToolStripMenuItem.Size = new System.Drawing.Size(245, 36);
            this.sechduleVisionTestToolStripMenuItem.Text = "Schedule Vision Test";
            this.sechduleVisionTestToolStripMenuItem.Click += new System.EventHandler(this.sechduleVisionTestToolStripMenuItem_Click);
            // 
            // sechduleWrittenTestToolStripMenuItem
            // 
            this.sechduleWrittenTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Written_Test_32;
            this.sechduleWrittenTestToolStripMenuItem.Name = "sechduleWrittenTestToolStripMenuItem";
            this.sechduleWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(245, 36);
            this.sechduleWrittenTestToolStripMenuItem.Text = "Schedule Written Test";
            this.sechduleWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.sechduleWrittenTestToolStripMenuItem_Click);
            // 
            // sechduleStreetTestToolStripMenuItem
            // 
            this.sechduleStreetTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.sechduleStreetTestToolStripMenuItem.Name = "sechduleStreetTestToolStripMenuItem";
            this.sechduleStreetTestToolStripMenuItem.Size = new System.Drawing.Size(245, 36);
            this.sechduleStreetTestToolStripMenuItem.Text = "Schedule Street Test";
            this.sechduleStreetTestToolStripMenuItem.Click += new System.EventHandler(this.sechduleStreetTestToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(307, 6);
            // 
            // issueDrivingLicenseFirstTimeToolStripMenuItem
            // 
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Name = "issueDrivingLicenseFirstTimeToolStripMenuItem";
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Text = "&Issue Driving License (First Time) ";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(307, 6);
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_View_32;
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.showLicenseToolStripMenuItem.Text = "Show &License";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(307, 6);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(310, 36);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License &History";
            // 
            // dgvLDLA
            // 
            this.dgvLDLA.AllowUserToAddRows = false;
            this.dgvLDLA.AllowUserToDeleteRows = false;
            this.dgvLDLA.AllowUserToResizeRows = false;
            this.dgvLDLA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLDLA.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLDLA.BackgroundColor = System.Drawing.Color.White;
            this.dgvLDLA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLDLA.ContextMenuStrip = this.contextMenuStrip;
            this.dgvLDLA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLDLA.Location = new System.Drawing.Point(1, 418);
            this.dgvLDLA.MultiSelect = false;
            this.dgvLDLA.Name = "dgvLDLA";
            this.dgvLDLA.ReadOnly = true;
            this.dgvLDLA.RowHeadersWidth = 51;
            this.dgvLDLA.RowTemplate.Height = 24;
            this.dgvLDLA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLDLA.Size = new System.Drawing.Size(1285, 489);
            this.dgvLDLA.TabIndex = 1;
            this.dgvLDLA.DoubleClick += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(1, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1285, 63);
            this.label1.TabIndex = 2;
            this.label1.Text = "Manage Local Driving License Applications";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 372);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 36);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter By : ";
            // 
            // cbFilter
            // 
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No.",
            "Full Name",
            "Status"});
            this.cbFilter.Location = new System.Drawing.Point(189, 374);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(225, 33);
            this.cbFilter.TabIndex = 6;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(430, 375);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(181, 30);
            this.txtFilter.TabIndex = 7;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(170, 948);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(62, 32);
            this.lblNumberOfRecords.TabIndex = 9;
            this.lblNumberOfRecords.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 948);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "# Records : ";
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.Image = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewApplication.Location = new System.Drawing.Point(1184, 331);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(91, 81);
            this.btnAddNewApplication.TabIndex = 11;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1118, 948);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(157, 48);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Local_321;
            this.pictureBox2.Location = new System.Drawing.Point(723, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 61);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(535, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(217, 232);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // frmListLocalDrivingLicesnseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1287, 1002);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvLDLA);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmListLocalDrivingLicesnseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Local Driving Licesnse Applications";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicesnseApplications_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.DataGridView dgvLDLA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cancelApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem sechduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechduleVisionTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechduleWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechduleStreetTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicenseFirstTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbFilter;
    }
}