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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        private int _LDLAID;
        private clsLocalDrivingLicenseApplication _LDLApp;
        public int LDLAID 
        { 
            get { return _LDLAID; }
        }


        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void LoadLDLA_Info(int LDLA_ID)
        {
            _LDLAID = LDLA_ID;
            _LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LDLAID);
            if (_LDLApp == null)
            {
                MessageBox.Show($"No Local Driving License Application ID with {_LDLAID}", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            llShowLicenceInfo.Enabled = _LDLApp.DoesLicenseIssuedForThisApplication();

            ctrlApplicationBasicInfo.LoadApplicationInfo(_LDLApp.ApplicationID);

            lblLDLA_ID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LDLApp.LicenseClassInfo.ClassName;
            lblPassedTests.Text = _LDLApp.NumberOfPassedTests().ToString();

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Implement Here
        }
    }
}
