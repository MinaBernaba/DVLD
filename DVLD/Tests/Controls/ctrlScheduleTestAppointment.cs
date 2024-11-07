using BusinessLogicOfDVLD;
using DVLD.Classes;
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
using static BusinessLogicOfDVLD.clsApplication;
using static BusinessLogicOfDVLD.clsTestType;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTestAppointment : UserControl
    {
        private enum enMode { AddNew,Update}
        private enMode Mode = enMode.AddNew;


        private int _LDLA_ID;
        private clsLocalDrivingLicenseApplication _LDLA;


        private clsApplication RetakeApplication;

        private clsTestAppointment TestAppointment;

        public clsTestType.enTestType TestType = enTestType.VisionTest;


        public ctrlScheduleTestAppointment()
        {
            InitializeComponent();
        }
        private void DefineTestType()
        {
            switch (_LDLA.NumberOfPassedTests())
            {
                case 0:
                    {
                        TestType = enTestType.VisionTest;
                        pcTitle.Image = Resources.Vision_512;
                        gbTestType.Text = "Schedule Vision Test";
                        lblTitle.Text = "Schedule Vision Test";
                        return;
                    }
                case 1:
                    {
                        TestType = enTestType.WrittenTest;
                        pcTitle.Image = Resources.Written_Test_512;
                        gbTestType.Text = "Schedule Written Test";
                        lblTitle.Text = "Schedule Written Test";
                        return;
                    }
                case 2:
                    {
                        TestType = enTestType.StreetTest;
                        pcTitle.Image = Resources.Street_test_512;
                        gbTestType.Text = "Schedule Street Test";
                        lblTitle.Text = "Schedule Street Test";
                        return;
                    }
            }
        }
        private void _RetakeTestInfo() {
         
         RetakeApplication = new clsApplication();
         RetakeApplication.ApplicantPersonID = _LDLA.ApplicantPersonID;
         RetakeApplication.ApplicationDate = DateTime.Now;
         RetakeApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
         RetakeApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
         RetakeApplication.LastStatusDate = DateTime.Now;
         RetakeApplication.PaidFees = clsApplicationType.Find(Convert.ToByte(clsApplication.enApplicationType.RetakeTest)).ApplicationFees;
         RetakeApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
         lblRetakeAppFees.Text = RetakeApplication.PaidFees.ToString();
         lblTotalFees.Text = (RetakeApplication.PaidFees + Convert.ToDecimal(lblTestFees.Text)).ToString();
            
        }
        public void LoadDataOfScheduleTest(int LDLA_ID, int TestAppointmentID = -1)
        {
            if (TestAppointmentID == -1)
            {
                TestAppointment = new clsTestAppointment();
                Mode = enMode.AddNew;
            }
            else
            {
                TestAppointment = clsTestAppointment.Find(TestAppointmentID);
                Mode = enMode.Update;
            }


            _LDLA_ID = LDLA_ID;
            _LDLA = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LDLA_ID);

            if (_LDLA == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LDLA_ID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            DefineTestType();


            dtTestDate.MinDate = DateTime.Now;
            lblShowLDLAID.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LDLA.LicenseClassInfo.ClassName;
            lblFullName.Text = _LDLA.ApplicantFullName;
            lblTrials.Text = _LDLA.TrailsCount(Convert.ToByte(TestType)).ToString();
            lblTestFees.Text = clsTestType.Find(TestType).TestTypeFees.ToString();
            if (Convert.ToInt16(lblTrials.Text) > 0) _RetakeTestInfo();
            else
            {
                gbRetakeTest.Enabled = false;
                lblTotalFees.Text = lblTestFees.Text;
                lblRetakeAppFees.Text = 0.ToString();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(lblTrials.Text) > 0)
            {
                if (RetakeApplication.Save())
                {
                    lblRetakeTestAppID.Text = RetakeApplication.ApplicationID.ToString();
                    TestAppointment.RetakeTestApplicationID = RetakeApplication.ApplicationID;
                    MessageBox.Show($"Adding retake application with ID {RetakeApplication.ApplicationID}!", "Added",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Adding retake application failed!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        TestAppointment = new clsTestAppointment();
                        TestAppointment.TestTypeID = (int)TestType;
                        TestAppointment.LocalDrivingLicenseApplicationID = _LDLA_ID;
                        TestAppointment.AppointmentDate = dtTestDate.Value;
                        TestAppointment.PaidFees = Convert.ToDecimal(lblTestFees.Text);
                        TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                        TestAppointment.IsLocked = false;
                        if (TestAppointment.Save())
                        {
                            MessageBox.Show($"Adding new test appointment with ID {TestAppointment.TestAppointmentID}!", "Added",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Adding new test appointment failed!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                       return;
                    }
                case enMode.Update:
                    {
                        TestAppointment.AppointmentDate = dtTestDate.Value;
                        if (TestAppointment.Save()) 
                            MessageBox.Show($"Test appointment with ID {TestAppointment.TestAppointmentID} Updated successfully !", "Updated",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else MessageBox.Show("Test appointment update failed!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
            }
            
            
            


        }
    }
}
