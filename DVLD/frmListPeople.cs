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
        public frmListPeople()
        {
            InitializeComponent();
           
        }
        private void _RefreshDataGridView() {
            _dataTable=clsPerson.GetAllPeople();
            DataTable dt = _dataTable.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
           "DateOfBirth", "Gender", "Phone", "Email", "Nationality");
            dgvListPeople.DataSource = dt;
            lblNumberOfRecords.Text = dgvListPeople.RowCount.ToString();
        }
        private void ManagePeople_Load(object sender, EventArgs e)
        {
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
            if(MessageBox.Show("Are you sure that you wanted to delete this person?", "Confirm",
                MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dgvListPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Person Successfully!", "Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Deletion Failed!", "Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _RefreshDataGridView();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature doesn't implemented yet!" , "Not ready!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature doesn't implemented yet!", "Not ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
