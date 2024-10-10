using BusinessLogicOfDVLV;
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
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set
            {
                _EnableFilter = value;
                gbFilter.Visible = _EnableFilter;
            }
        }
        private bool _ShowAddNew = true;
        public bool ShowAddNew
        {
            get { return _ShowAddNew; }
            set {
                _ShowAddNew = value;
                btnAddNewPerson.Visible = _ShowAddNew;
            }
        }
        private int _PeronID = -1;
        public int PeronID { get { return ctrlPersonCard.PersonID; } }
        public clsPerson SelectedPerson { get { return ctrlPersonCard.SelectedPerson; } }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFindBy.SelectedItem = "Person ID";
            txtFilter.Text = PersonID.ToString();
            FindNow();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            cbFindBy.SelectedItem = "National Number";
            txtFilter.Text = NationalNo;
            FindNow();
        }
        private void FindNow()
        {
            switch (cbFindBy.SelectedText)
            {
                case "Person ID":
                    {
                        ctrlPersonCard.LoadPersonInfo(Convert.ToInt16(txtFilter.Text));
                        return;
                    }
                case "National Number":
                    {
                        ctrlPersonCard.LoadPersonInfo(txtFilter.Text);
                        return;
                    }
                    default:break;
            }
            if(EnableFilter)
            OnPersonSelected?.Invoke(ctrlPersonCard.PersonID);

        }
        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (txtFilter.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "This field is required");
            }
            else errorProvider1.SetError(txtFilter, null);
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) btnFind.PerformClick();
            if (cbFindBy.SelectedText == "Person ID") 
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid, put the mouse over the red icon(s) to see the error(s).",
                   "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                
            }
            FindNow();
        }
        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedItem = "Person ID";
            txtFilter.Focus();
        }
        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;
            txtFilter.Focus();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            LoadPersonInfo(PersonID);
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
        public void FilterFocus()
        {
            txtFilter.Focus();
        }
    }
}