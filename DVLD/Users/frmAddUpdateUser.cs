using BusinessLogicOfDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {

        private clsUser _User = new clsUser();
        private int _UserID;
        enum enMode {AddNew , Update}
        enMode Mode = enMode.AddNew;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _UserID = UserID;
            _User = clsUser.FindUser(_UserID);
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        this.Text = "Add new user";
                        lblTitle.Text = "Add new user";
                        tcLoginInfo.Enabled = false;
                        ctrlPersonCardWithFilter.FilterFocus();
                        break;
                    }
                case enMode.Update:
                    {
                        this.Text = "Update User";
                        lblTitle.Text = "Update User";

                        btnSave.Enabled = true;

                        ctrlPersonCardWithFilter.EnableFilter = false;
                        if (_User == null)
                        {
                            MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.Close();

                            return;
                        }
                        ctrlPersonCardWithFilter.LoadPersonInfo(_User.PersonID);
                        lblUserID.Text = _UserID.ToString();
                        txtUserName.Text = _User.UserName;
                        txtPassword.Text = _User.Password;
                        txtConfirmPassword.Text = _User.Password;
                        chkIsActive.Checked = _User.IsActive;
                        break;
                    }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid, put the mouse over the red icon(s) to see the error(s).",
                 "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;
            if (Mode == enMode.AddNew)
            {
                if (_User.Save())
                {
                    Mode = enMode.Update;
                    this.Text = "Update User";
                    lblTitle.Text = "Update User";
                    lblUserID.Text = _User.UserID.ToString();
                    MessageBox.Show($"User added successfully with ID : {_User.UserID} !", "User Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Adding failed!", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Mode == enMode.Update)
            {
                if (_User.Save())
                {
                    MessageBox.Show($"User with ID : {_User.UserID} updated successfully!", "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Update failed!", "Failed",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update || ctrlPersonCardWithFilter.SelectedPerson != null)
            {
                if (clsUser.IsUserExistByPersonID(_User.PersonID) && Mode == enMode.AddNew)
                {
                    MessageBox.Show("The selected person is already a user, Please select another one!",
                        "Select another person",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tcLoginInfo.Enabled = true;
                btnSave.Enabled = true;
                tabControl.SelectedIndex = 1;
            }
            else MessageBox.Show("Please select a person!", "No person selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlPersonCardWithFilter_OnPersonSelected(int obj)
        {
            _User.PersonID = obj;
            btnNext.Focus();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtUserName, "Username can't be blank!");
                return;
            }
            else errorProvider.SetError(txtUserName, null);
            if (_User.UserName != txtUserName.Text && clsUser.IsUserNameExist(txtUserName.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtUserName,"This username already exists for another user!");
                return;
            }
            else errorProvider.SetError(txtUserName, null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Password can't be blank!");
                return;
            }
            else errorProvider.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Password confirmation Doesn't match!");
                return;
            }
            else errorProvider.SetError(txtConfirmPassword, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
