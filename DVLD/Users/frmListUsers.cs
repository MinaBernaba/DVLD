using BusinessLogicOfDVLD;
using DVLD.Controls;
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
                dgvUsers.Columns[0].Width = 110;
                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;
                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 500;
                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 150;
                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 110;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilter.Text != "None" && cbFilter.Text != "Is Active");
            cbIsActiveOptions.Visible = (cbFilter.Text == "Is Active");
            if (cbFilter.Text == "None")
            {
                dataViewUsers.RowFilter = "";
                lblRecords.Text = dataViewUsers.Count.ToString();
                return;
            }
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
                        dataViewUsers.RowFilter = "";
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
            _Refresh();
        }
        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }
        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to delete this user?","Confirm!",
                MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK){
                if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User deleted successfully!", "User Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Refresh();
                }
                else  MessageBox.Show("This user can't be deleted due to data linked to him!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature doesn't implemented yet!", "Not ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature doesn't implemented yet!", "Not ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
