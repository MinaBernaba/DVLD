using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicOfDVLD;

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private DataView _DataViewApplicationTypes;

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }
        private void _Refresh()
        {
            DataTable dt = clsApplicationType.GetAllApplicationTypes();
            _DataViewApplicationTypes = dt.DefaultView;
            dgvApplicationTypes.DataSource = _DataViewApplicationTypes;
            lblRecords.Text = _DataViewApplicationTypes.Count.ToString();
        }
        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _Refresh();
            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 100;
            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 450;
            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType(Convert.ToByte(dgvApplicationTypes.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _Refresh();
        }
    }
}
