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

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestType _TestTypeID;
        private clsTestType _TestType;
        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);
            if (_TestType == null)
            {
                MessageBox.Show("No Test Type with ID = " + Convert.ToByte(_TestTypeID), "_Application TypeID Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblShowingTestTypeID.Text = Convert.ToByte(_TestType.TestTypeID).ToString();
            txtTestTilte.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtTestFees.Text = _TestType.TestTypeFees.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid, put the mouse over the red icon(s) to see the error(s).",
                 "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestType.TestTypeTitle = txtTestTilte.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = Convert.ToDecimal(txtTestFees.Text);
            if (_TestType.Save())
                MessageBox.Show("Test type updated successfully!", "Updated!",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Test type update failed!", "Failed!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtTest_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(textBox.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider.SetError(textBox, "This field can't be blank!");
            }
            else errorProvider.SetError(textBox, null);
        }

        private void txtTestFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) &&
              !(e.KeyChar == '.' && !txtTestFees.Text.Contains('.')));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
