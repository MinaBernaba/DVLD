namespace DVLD
{
    partial class frmListPeople
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvShowPeopleInSystem = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowPeopleInSystem)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShowPeopleInSystem
            // 
            this.dgvShowPeopleInSystem.AllowUserToAddRows = false;
            this.dgvShowPeopleInSystem.AllowUserToDeleteRows = false;
            this.dgvShowPeopleInSystem.AllowUserToResizeRows = false;
            this.dgvShowPeopleInSystem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShowPeopleInSystem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvShowPeopleInSystem.BackgroundColor = System.Drawing.Color.White;
            this.dgvShowPeopleInSystem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowPeopleInSystem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvShowPeopleInSystem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvShowPeopleInSystem.Location = new System.Drawing.Point(0, 177);
            this.dgvShowPeopleInSystem.MultiSelect = false;
            this.dgvShowPeopleInSystem.Name = "dgvShowPeopleInSystem";
            this.dgvShowPeopleInSystem.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowPeopleInSystem.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShowPeopleInSystem.RowHeadersWidth = 51;
            this.dgvShowPeopleInSystem.RowTemplate.Height = 24;
            this.dgvShowPeopleInSystem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShowPeopleInSystem.Size = new System.Drawing.Size(1196, 366);
            this.dgvShowPeopleInSystem.TabIndex = 0;
            this.dgvShowPeopleInSystem.TabStop = false;
            // 
            // frmListPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1196, 543);
            this.Controls.Add(this.dgvShowPeopleInSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListPeople";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.ManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowPeopleInSystem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShowPeopleInSystem;
    }
}