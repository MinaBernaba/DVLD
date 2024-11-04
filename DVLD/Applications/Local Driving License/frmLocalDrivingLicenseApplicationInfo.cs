using BusinessLogicOfDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _LDLA_ID;
       // private clsLocalDrivingLicenseApplication LDLA;
        public frmLocalDrivingLicenseApplicationInfo(int LDLA_ID)
        {
            InitializeComponent();
            _LDLA_ID = LDLA_ID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            if (!clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(_LDLA_ID))
            {
                MessageBox.Show($"No Local Driving License Application ID with {_LDLA_ID}", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlLocalDrivingLicenseApplicationInfo.LoadLDLA_Info(_LDLA_ID);
        }
    }
}
