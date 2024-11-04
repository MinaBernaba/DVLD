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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _LDLA_ID;
        private int _TestAppointmentID;
        public frmScheduleTest(int LDLA_ID , int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LDLA_ID = LDLA_ID;
            _TestAppointmentID = TestAppointmentID;
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            if(_TestAppointmentID == -1) 
                ctrlScheduleTestAppointment.LoadDataOfScheduleTest(_LDLA_ID);
            else ctrlScheduleTestAppointment.LoadDataOfScheduleTest(_LDLA_ID, _TestAppointmentID);

            switch (ctrlScheduleTestAppointment.TestType)
            {
                case (clsTestType.enTestType.VisionTest):
                {
                    this.Text = "Vision Test";
                    break;
                }
                case (clsTestType.enTestType.WrittenTest):
                {
                        this.Text = "Written Test";
                        break;
                }
                case (clsTestType.enTestType.StreetTest):
                {
                        this.Text = "Street Test";
                        break;
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
