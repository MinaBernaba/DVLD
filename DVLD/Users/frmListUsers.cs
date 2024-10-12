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
    public partial class frmListUsers : Form
    {
        DataView dataViewUsers = new DataView();
        public frmListUsers()
        {
            InitializeComponent();
        }
        private void _Refresh()
        {
            DataTable dtUsers = clsUser.GetAllUsers().DefaultView.ToTable(false, 
                "UserID", "PersonID", "FullName", "UserName", "IsActive"); ;
            dataViewUsers = dtUsers.DefaultView;
            dgvUsers.DataSource = dtUsers;
            lblRecords.Text = dataViewUsers.Count.ToString();
        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            _Refresh();
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[4].HeaderText = "Is Active";
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilter.Text != "None" && cbFilter.Text != "Is Active");
            cbIsActiveOptions.Visible = (cbFilter.Text == "Is Active");
            if (cbIsActiveOptions.Visible)
            {
                cbIsActiveOptions.SelectedIndex = 0;
                cbIsActiveOptions.Focus();
            }
            if (txtFilter.Visible)
            {
                txtFilter.Text = string.Empty;
                txtFilter.Focus();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;
            switch (cbFilter.Text)
            {
                case "None":
                    {
                        RowFilter = "None";
                        break;
                    }
                case "User ID":
                    {
                        RowFilter = "UserID";
                        break;
                    }
                case "Person ID":
                    {
                        RowFilter = "PersonID";
                        break;
                    }
                case "UserName":
                    {
                        RowFilter = "UserName";
                        break;
                    }
                case "Full Name":
                    {
                        RowFilter = "FullName";
                        break;
                    }
                case "Is Active":
                    {
                        RowFilter = "IsActive";
                        break;
                    }
            }
            if (txtFilter.Text == string.Empty)
            {
                dataViewUsers.RowFilter = "";
                lblRecords.Text = dataViewUsers.Count.ToString();
                return;
            }
            if (RowFilter == "UserID" || RowFilter == "PersonID")
                dataViewUsers.RowFilter = string.Format($"{RowFilter} = {txtFilter.Text.Trim()}");
            else dataViewUsers.RowFilter = string.Format($"{RowFilter} LIKE '{txtFilter.Text.Trim()}%'");
            lblRecords.Text = dataViewUsers.Count.ToString();
        }

        private void cbIsActiveOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;
            switch (cbIsActiveOptions.Text)
            {
                case "All":
                    {
                        return;
                    }
                case "Yes":
                    {
                        dataViewUsers.RowFilter = " IsActive = true";
                        return;
                    }
                case "No":
                    {
                        dataViewUsers.RowFilter = " IsActive = false";
                        return;
                    }
            }
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
        }
        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
