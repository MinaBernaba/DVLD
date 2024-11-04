using BusinessLogicOfDVLD;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLogicOfDVLD.clsTestType;

namespace DVLD.Tests.Controls
{
    
    public partial class ctrlTakeTest : UserControl
    {
        public int LDLA_ID;
        public int TestAppointmentID;
        public clsLocalDrivingLicenseApplication LDLA;
        public clsTestAppointment TestAppointment;
        public clsTestType.enTestType TestType = enTestType.VisionTest;
        public ctrlTakeTest()
        {
            InitializeComponent();
        }
        private void _DefineTestType()
        {
            switch (LDLA.NumberOfPassedTests())
            {
                case 0:
                    {
                        TestType = enTestType.VisionTest;
                        pcTitle.Image = Resources.Vision_512;
                        gbTakeTest.Text = "Vision Test";
                        return;
                    }
                case 1:
                    {
                        TestType = enTestType.WrittenTest;
                        pcTitle.Image = Resources.Written_Test_512;
                        gbTakeTest.Text = "Written Test";
                        return;
                    }
                case 2:
                    {
                        TestType = enTestType.StreetTest;
                        pcTitle.Image = Resources.Street_test_512;
                        gbTakeTest.Text = "Street Test";
                        return;
                    }
            }
        }
        public void LoadTakeTestInfo(int TestAppointmentID)
        {
            this.TestAppointmentID = TestAppointmentID;
            TestAppointment = clsTestAppointment.Find(this.TestAppointmentID);
            if (TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + this.TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LDLA_ID = TestAppointment.LocalDrivingLicenseApplicationID;
            LDLA = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LDLA_ID);
            _DefineTestType();
            lblLocalDrivingLicenseAppID.Text = LDLA_ID.ToString();
            lblLicenseClass.Text = LDLA.LicenseClassInfo.ClassName;
            lblFullName.Text = LDLA.ApplicantFullName;
            lblTrial.Text = LDLA.TrailsCount(Convert.ToByte(TestAppointment.TestTypeID)).ToString();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblFees.Text = TestAppointment.PaidFees.ToString();
        }

    }
}
