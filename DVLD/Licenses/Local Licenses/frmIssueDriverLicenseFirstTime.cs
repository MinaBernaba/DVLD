using BusinessLogicOfDVLD;
using DVLD.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LDLA_ID;
        private clsLocalDrivingLicenseApplication _LDLA;
        public frmIssueDriverLicenseFirstTime(int LDLA_ID)
        {
            InitializeComponent();
            _LDLA_ID = LDLA_ID;
        }
        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LDLA = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LDLA_ID);
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLDLA_Info(_LDLA.LocalDrivingLicenseApplicationID);
        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int LicenseID = _LDLA.IssueLicense(txtNotes.Text, clsGlobal.CurrentUser.UserID);
            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
