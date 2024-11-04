using BusinessLogicOfDVLD;
using DVLD.Tests;
using DVLD.Tests.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        DataView dvLDLAs;
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }
        private void _Refresh()
        {
            cbFilter.SelectedIndex = 0;
            DataTable dataTable = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplication();
            dvLDLAs = dataTable.DefaultView;
            dgvLDLA.DataSource = dvLDLAs;
            lblNumberOfRecords.Text = dvLDLAs.Count.ToString();

        }
        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _Refresh();
            if(dgvLDLA.RowCount > 0)
            {
                dgvLDLA.Columns[0].HeaderText = "LDL AppID";
                dgvLDLA.Columns[0].Width = 70;
                dgvLDLA.Columns[1].Width = 230;
                dgvLDLA.Columns[2].Width = 150;
                dgvLDLA.Columns[3].Width = 80;
                dgvLDLA.Columns[4].Width = 150;
                dgvLDLA.Columns[5].Width = 70;
                dgvLDLA.Columns[6].Width = 165;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = !(cbFilter.SelectedIndex == 0);
            txtFilter.Focus();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = "";
            switch (cbFilter.Text)
            {
                case "None":
                    {
                        RowFilter = "None";
                        break;
                    }
                case "L.D.L.AppID":
                    {
                        RowFilter = "L.D.L.AppID";
                        break;
                    }
                case "National No.":
                    {
                        RowFilter = "National No.";
                        break;
                    }
                case "Full Name":
                    {
                        RowFilter = "Full Name";
                        break;
                    }
                case "Status":
                    {
                        RowFilter = "Application Status";
                        break;
                    }
                default: RowFilter = "";
                    break;
                    
            }
            if (txtFilter.Text == string.Empty|| RowFilter == "None")
            {
                dvLDLAs.RowFilter = string.Empty;
                lblNumberOfRecords.Text = dvLDLAs.Count.ToString();
                return;
            }
            if (RowFilter == "L.D.L.AppID")
                dvLDLAs.RowFilter = string.Format($"{RowFilter} = {txtFilter.Text}");
            else dvLDLAs.RowFilter = string.Format($"[{RowFilter}] LIKE '{txtFilter.Text}%'");
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "L.D.L.AppID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            _Refresh();
        }
        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication(
                (int)dgvLDLA.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }
        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you wanted to delete this Local Driving License Application?", "Confirm",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication((int)dgvLDLA.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Application Successfully!", "Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Application can't be deleted because it has data linked to it!", "Deletion Failed!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _Refresh();
        }
        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID((int)dgvLDLA.CurrentRow.Cells[0].Value).CancelApplication();
            _Refresh();
        }
        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLDLA.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            
            int LDLA_ID = (int)dgvLDLA.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LDLA = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LDLA_ID);
            bool IsLicenseExist = LDLA.DoesLicenseIssuedForThisApplication();
            int TotalPassedTests = (int)dgvLDLA.CurrentRow.Cells[5].Value;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && LDLA.ApplicationStatus == clsApplication.enApplicationStatus.New;
            showLicenseToolStripMenuItem.Enabled = IsLicenseExist;

            deleteApplicationToolStripMenuItem.Enabled = LDLA.ApplicationStatus == clsApplication.enApplicationStatus.New;
            cancelApplicationToolStripMenuItem.Enabled = LDLA.ApplicationStatus == clsApplication.enApplicationStatus.New;
            editApplicationToolStripMenuItem.Enabled = LDLA.ApplicationStatus == clsApplication.enApplicationStatus.New;


            sechduleToolStripMenuItem.Enabled = (TotalPassedTests != 3) && LDLA.ApplicationStatus != clsApplication.enApplicationStatus.Cancelled; ;
            sechduleVisionTestToolStripMenuItem.Enabled = (TotalPassedTests == 0);
            sechduleWrittenTestToolStripMenuItem.Enabled = (TotalPassedTests == 1);
            sechduleStreetTestToolStripMenuItem.Enabled = (TotalPassedTests == 2);
            
            
        }
        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLDLA.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }
        private void sechduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLDLA.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }
        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLDLA.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }
    }
}
