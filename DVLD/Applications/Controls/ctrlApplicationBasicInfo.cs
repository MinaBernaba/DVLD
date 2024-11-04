using BusinessLogicOfDVLD;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID;
        private clsApplication _ApplicationInfo;
        public int ApplicationID {  get { return _ApplicationID;  }  }
        public clsApplication ApplicationInfo {get { return _ApplicationInfo; } }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            _ApplicationInfo = clsApplication.FindApplication(ApplicationID);
            if (_ApplicationInfo == null) {
                MessageBox.Show($"No Application ID with {ApplicationID}", "Error"
                    , MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            lblAppID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblAppStatus.Text = _ApplicationInfo.StatusText;
            lblAppType.Text = _ApplicationInfo.ApplicationTypeInfo.ApplicationTypeTitle;
            lblAppFees.Text = _ApplicationInfo.PaidFees.ToString();
            lblApplicantID.Text = _ApplicationInfo.ApplicantPersonID.ToString();
            lblAppDate.Text = _ApplicationInfo.ApplicationDate.ToString("dd/MM/yyyy");
            lblStatusDate.Text = _ApplicationInfo.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = _ApplicationInfo.UserInfo.UserName;
        }

        private void lnlApplicant_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
