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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            ctrlUserCard.LoadUserInfo(UserID);
        }
        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindUser(_UserID);
            if (_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserCard.LoadUserInfo(_UserID);
        }
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtCurrentPassword, "Current Password can't be blank!");
                return;
            }
            else errorProvider.SetError(txtCurrentPassword, null);
            if (txtCurrentPassword.Text.Trim() != _User.Password)
            {
                e.Cancel = true;
                errorProvider.SetError(txtCurrentPassword, "Current password is wrong!");
                return;
            }
            else errorProvider.SetError(txtCurrentPassword, null);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNewPassword, "New Password can't be blank!");
                return;
            }
            else errorProvider.SetError(txtNewPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            
            if(txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Password confirmation doesn't match new password!");
                return;
            }
            else errorProvider.SetError(txtConfirmPassword, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid, put the mouse over the red icon(s) to see the error(s).",
                 "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Password changed successfully", "Password changed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                string UserName = string.Empty , Password = string.Empty ;
                if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
                {
                    if (UserName.ToLower() == _User.UserName.ToLower()) clsGlobal.RememberMe(_User.UserName, _User.Password);
                }
            }
            else MessageBox.Show("Password change failed", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
