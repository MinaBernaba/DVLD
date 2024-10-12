using BusinessLogicOfDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {
        //private DataTable _People = new DataTable();
        DataView _PeopleDV = new DataView();
        public frmListPeople()
        {
            InitializeComponent();

        }
        private void _RefreshDataGridView()
        {
            txtFilter.Clear();
            DataTable _People = clsPerson.GetAllPeople().DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
           "Gender", "DateOfBirth", "Nationality", "Phone", "Email");
            _PeopleDV = _People.DefaultView;
            dgvListPeople.DataSource = _PeopleDV;
            lblNumberOfRecords.Text = dgvListPeople.RowCount.ToString();
            
        }
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            _RefreshDataGridView();
            if (dgvListPeople.Rows.Count > 0)
            {
                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[1].HeaderText = "National No.";
                dgvListPeople.Columns[2].HeaderText = "First Name";
                dgvListPeople.Columns[3].HeaderText = "Second Name";
                dgvListPeople.Columns[4].HeaderText = "Third Name";
                dgvListPeople.Columns[5].HeaderText = "Last Name";
                dgvListPeople.Columns[6].HeaderText = "Gender";
                dgvListPeople.Columns[7].HeaderText = "Date of birth";
                dgvListPeople.Columns[8].HeaderText = "Nationality";
                dgvListPeople.Columns[9].HeaderText = "Phone";
                dgvListPeople.Columns[10].HeaderText = "Email";
            }
        }

        private void AddNew(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshDataGridView();
        }

        private void EditPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDataGridView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you wanted to delete this person?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dgvListPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Person Successfully!", "Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Deletion Failed!", "Person can't be deleted because it has data linked to him",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _RefreshDataGridView();
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

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilter.Text != "None");
            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = "";
            switch (cbFilter.Text)
            {
                case "None":
                    {
                        RowFilter = "None";
                        break;
                    }
                case "Person ID":
                    {
                        RowFilter = "PersonID";
                        break;
                    }
                case "National No.":
                    {
                        RowFilter = "NationalNo";
                        break;
                    }
                case "First Name":
                    {
                        RowFilter = "FirstName";
                        break;
                    }
                case "Second Name":
                    {
                        RowFilter = "SecondName";
                        break;
                    }
                case "Third Name":
                    {
                        RowFilter = "ThirdName";
                        break;
                    }
                case "Last Name":
                    {
                        RowFilter = "LastName";
                        break;
                    }
                case "Nationality":
                    {
                        RowFilter = "Nationality";
                        break;
                    }
                case "Gender":
                    {
                        RowFilter = "Gender";
                        break;
                    }
                case "Phone":
                    {
                        RowFilter = "Phone";
                        break;
                    }
                case "Email":
                    {
                        RowFilter = "Email";
                        break;
                    }
                default:
                    {
                        RowFilter = "None";
                        break;
                    }
            }
            if (txtFilter.Text == "" || RowFilter == "None")
            {
                _PeopleDV.RowFilter = "";
                lblNumberOfRecords.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }
            if (RowFilter == "PersonID")
            _PeopleDV.RowFilter = string.Format(" {0} = {1}", RowFilter , txtFilter.Text.Trim());
            else _PeopleDV.RowFilter = string.Format(" {0} LIKE '{1}%'", RowFilter, txtFilter.Text.Trim());
            lblNumberOfRecords.Text = dgvListPeople.RowCount.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Person ID")
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }
    }
}