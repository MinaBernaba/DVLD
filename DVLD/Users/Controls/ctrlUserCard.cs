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

namespace DVLD.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        public int UserID { get { return _UserID; } }
        private clsUser _User = new clsUser();
        public clsUser User {  get { return _User; } }

        public ctrlUserCard()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindUser(_UserID);
            {
                if (_User == null)
                {
                    MessageBox.Show("No User with ID : " + UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            ctrlPersonCard.LoadPersonInfo(User.PersonID);
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName.ToString();
            lblIsActive.Text = (User.IsActive) ? "Yes" : "No"; 
        }

    }
}
