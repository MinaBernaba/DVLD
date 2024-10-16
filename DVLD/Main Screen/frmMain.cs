using BusinessLogicOfDVLD;
using DVLD.Controls;
using DVLD.Login_Screen;
using DVLD.People;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{

    public partial class frmMain : Form
    {
        public clsUser User = new clsUser();
        public frmMain(int UserID)
        {
            InitializeComponent();
            User = clsUser.FindUser(UserID);
        }

        private void peropleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void usresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers usres = new frmListUsers();
            usres.ShowDialog();
        }

        private void signoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Tag = 1;
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(User.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword(User.UserID);
            frmChangePassword.ShowDialog();
        }
    }
}
