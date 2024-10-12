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

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public delegate void EventHandlerDataBack(int UserID);
        public event EventHandlerDataBack DataBack;
        private clsUser _User;
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
                        lblTitle.Text = "Add new user";

                        tcLoginInfo.Enabled = false;
                        break;
                    }
                case enMode.Update:
                    {
                        lblTitle.Text = "Update user";
                        tcPersonInfo.Enabled = false;
                        ctrlPersonCardWithFilter.LoadPersonInfo(_User.PersonID);
                        lblUserID.Text = _UserID.ToString();
                        txtUserName.Text = _User.UserName;
                        txtPassword.Text = _User.Password;
                        txtConfirmPassword.Text = _User.Password;
                        break;
                    }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
