using BusinessLogicOfDVLV;
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

namespace DVLD
{
    public partial class frmListPeople : Form
    {
        private DataTable _dataTable = new DataTable();
        DataView _dataView = new DataView();
        public frmListPeople()
        {
            InitializeComponent();

        }
        private void _RefreshDataGridView()
        {
            txtFilter.Clear();
            _dataTable = clsPerson.GetAllPeople().DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
           "DateOfBirth", "Gender", "Phone", "Email", "Nationality");
            _dataView = _dataTable.DefaultView;
            dgvListPeople.DataSource = _dataView;
            lblNumberOfRecords.Text = dgvListPeople.RowCount.ToString();
        }
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            _RefreshDataGridView();
        }

        private void AddNew(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(-1);
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
            if (cbFilter.SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
            }
            else
            {
                _RefreshDataGridView();
                txtFilter.Visible = false;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            switch (cbFilter.SelectedItem.ToString())
            {
                case "None":
                    {
                        break;
                    }
                case "PersonID":
                    {
                        _dataView.RowFilter = $"PersonID = '{Convert.ToInt32(txtFilter.Text)}%'";
                        break;
                    }
                case "National No.":
                    {
                        _dataView.RowFilter = $"NationalNo like '{txtFilter.Text}%'";
                        break;
                    }
                case "First Name":
                    {
                        _dataView.RowFilter = $"FirstName like '{txtFilter.Text}%'";
                        break;
                    }
                case "Second Name":
                    {
                        _dataView.RowFilter = $"SecondName like '{txtFilter.Text}%'";
                        break;
                    }
                case "Third Name":
                    {
                        _dataView.RowFilter = $"ThirdName like '{txtFilter.Text}%'";
                        break;
                    }
                case "Last Name":
                    {
                        _dataView.RowFilter = $"LastName like '{txtFilter.Text}%'";
                        break;
                    }
                case "Nationality":
                    {
                        _dataView.RowFilter = $"Nationality like '{txtFilter.Text}%'";
                        break;
                    }
                case "Gender":
                    {
                        _dataView.RowFilter = $"Gender like '{txtFilter.Text}%'";
                        break;
                    }
                case "Phone":
                    {
                        _dataView.RowFilter = $"Phone like '{txtFilter.Text}%'";
                        break;
                    }
                case "Email":
                    {
                        _dataView.RowFilter = $"Email like '{txtFilter.Text}%'";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
            }
            lblNumberOfRecords.Text = dgvListPeople.RowCount.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}