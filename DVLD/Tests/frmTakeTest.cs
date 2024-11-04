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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private clsTest _Test;
        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            ctrlTakeTest.TestAppointmentID = TestAppointmentID;
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTakeTest.LoadTakeTestInfo(ctrlTakeTest.TestAppointmentID);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you can't change the Pass/Fail result.", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                this.Close();
                return;
            }
            _Test = new clsTest();
            _Test.TestAppointmentID = ctrlTakeTest.TestAppointmentID;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text;
            if (_Test.Save())
            {
                if (ctrlTakeTest.TestAppointment.LockTestAppointment())
                {
                    ctrlTakeTest.lblTestID.Text = _Test.TestID.ToString();
                    MessageBox.Show($"The result saved in test ID {_Test.TestID} !", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
                else MessageBox.Show($"Error in locking test appointment!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show($"Error in saving test result!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
