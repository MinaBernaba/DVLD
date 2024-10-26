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

namespace DVLD.Tests.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        DataView dvTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void _Refresh()
        {
            DataTable dt = clsTestType.GetAllTestTypes();
            dvTestTypes = dt.DefaultView;
            dgvTestTypes.DataSource = dvTestTypes;
            lblShowingRecords.Text = dvTestTypes.Count.ToString();
        }
        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _Refresh();
            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 50;
            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 140;
            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 711;
            dgvTestTypes.Columns[3].HeaderText = "Fees";
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
