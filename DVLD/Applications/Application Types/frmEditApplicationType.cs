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

namespace DVLD.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private clsApplicationType _ApplicationType;
        private byte _ApplicationTypeID;
        public frmEditApplicationType(byte ApplicationTypesID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypesID;
        }
        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            if (_ApplicationType == null)
            {
                MessageBox.Show("No Application Type with ID = " + _ApplicationTypeID, "_Application TypeID Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            txtApplicationTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();
            lblShowingApplicationID.Text = _ApplicationType.ApplicationTypeID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid, put the mouse over the red icon(s) to see the error(s).",
                 "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTypeTitle = txtApplicationTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToDecimal(txtApplicationFees.Text);
            if (_ApplicationType.Save())
                MessageBox.Show("Application type updated successfully!","Updated!",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
            else MessageBox.Show("Application type update failed!", "Failed!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void txtApplication_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider.SetError(textBox, "This field can't be blank!");
            }
            else errorProvider.SetError(textBox, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
