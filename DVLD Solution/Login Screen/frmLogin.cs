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
using System.IO;

namespace DVLD.Login_Screen
{
    public partial class frmLogin : Form
    {
        public clsUser User = new clsUser();
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User = clsUser.FindUser(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            if (User != null)
            {
                if (User.IsActive)
                {
                    if (chkRememberMe.Checked) clsGlobal.RememberMe(User.UserName, User.Password);
                    else clsGlobal.RememberMe("", "");
                    clsGlobal.CurrentUser = User;
                    this.Tag = User.UserID;
                    this.Close();
                }
                else
                {
                    txtUsername.Focus(); 
                    MessageBox.Show("Your account is not active, Please contact your admin!", "Inactivated Account",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUsername.Focus();
                MessageBox.Show("Invalid Username/Password!", "Wrong Credentials",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
                string Username = string.Empty;
                string Password = string.Empty;
                if (clsGlobal.GetStoredCredential(ref Username, ref Password))
                {
                    txtUsername.Text = Username;
                    txtPassword.Text = Password;
                    chkRememberMe.Checked = true;
                }
                else chkRememberMe.Checked = false;
        }
    }
}
