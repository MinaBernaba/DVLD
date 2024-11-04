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

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private int _LDLA_ID;
        private clsLocalDrivingLicenseApplication _LDLA;
        private DataView dvTestAppointment;
        private clsTestType.enTestType TestType = enTestType.VisionTest;
        public frmListTestAppointments(int LDLA_ID)
        {
            InitializeComponent();
            _LDLA_ID = LDLA_ID;
        }
        private void DefineTestType()
        {
            switch (_LDLA.NumberOfPassedTests()) { 
                case 0:
                    {
                        TestType = enTestType.VisionTest;
                        pcTitle.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                        this.Text = "Vision Test";
                        return;
                    }
                case 1:
                    {
                        TestType = enTestType.WrittenTest; 
                        pcTitle.Image = Resources.Written_Test_512;
                        lblTitle.Text = "Written Test";
                        this.Text = "Written Test";
                        return;
                    }
                case 2:
                    {
                        TestType = enTestType.StreetTest;
                        pcTitle.Image = Resources.Street_test_512;
                        lblTitle.Text = "Street Test";
                        this.Text = "Street Test";
                        return;
                    }
            }
        
        }
        private void _RefreshDGV() {
            if (_LDLA.DoesApplicationHaveAnAppointmentBeforeOnThisTest(Convert.ToByte(TestType)))
            {
                DataTable dt = _LDLA.GetTestAppointmentsInfoAboutCertainTestType(Convert.ToByte(TestType));
                dvTestAppointment = dt.DefaultView;
                dgvTestAppointments.DataSource = dvTestAppointment;
               
                dgvTestAppointments.Columns[0].HeaderText = "Test Appointment ID";
                dgvTestAppointments.Columns[0].Width = 180;
                dgvTestAppointments.Columns[1].HeaderText = "Test Type Title";
                dgvTestAppointments.Columns[1].Width = 250;
                dgvTestAppointments.Columns[2].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[2].Width = 200;
                dgvTestAppointments.Columns[3].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[3].Width = 100;
                dgvTestAppointments.Columns[4].HeaderText = "Is Locked";
                lblNumberOfRecords.Text = dvTestAppointment.Count.ToString();
            }
            else lblNumberOfRecords.Text = 0.ToString();
        }
        private void _Reset()
        {
            _LDLA = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LDLA_ID);
            if (_LDLA == null) {
                MessageBox.Show($"There is no Local Driving License Application ID {_LDLA_ID}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            DefineTestType();
            _RefreshDGV();
            ctrlLocalDrivingLicenseApplicationInfo.EnablellShowLicenseInfo = false;
            ctrlLocalDrivingLicenseApplicationInfo.LoadLDLA_Info(_LDLA.LocalDrivingLicenseApplicationID);

        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _Reset();
        }
        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            if (_LDLA.DoesApplicantHaveAnActiveTestAppointment())
            {
                MessageBox.Show("The applicant already has an active appointment of this test!"
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (_LDLA.DoesApplicatPassThisTest(Convert.ToByte(TestType)))
            {
                MessageBox.Show("The applicant already has Passed this test before!, You can only retake failed test!"
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmScheduleTest frm = new frmScheduleTest(_LDLA_ID);
            frm.ShowDialog();
            _RefreshDGV();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnScheduleTest_MouseEnter(object sender, EventArgs e)
        {
            btnScheduleTest.BackColor = Color.GreenYellow;
        }
        private void btnScheduleTest_MouseLeave(object sender, EventArgs e)
        {
            btnScheduleTest.BackColor = Color.White;
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)dgvTestAppointments.CurrentRow.Cells[4].Value)
            {
                MessageBox.Show("The applicant has already taken the test, you can't edit the test appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduleTest frm = new frmScheduleTest(_LDLA_ID, (int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDGV();
        }
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)dgvTestAppointments.CurrentRow.Cells[4].Value)
            {
                MessageBox.Show("The applicant has already taken the test, you can't edit the result.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest frm = new frmTakeTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDGV();
            ctrlLocalDrivingLicenseApplicationInfo.EnablellShowLicenseInfo = false;
            ctrlLocalDrivingLicenseApplicationInfo.LoadLDLA_Info(_LDLA.LocalDrivingLicenseApplicationID);
        }
    }
}
