using BusinessLogicOfDVLD;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD.Classes;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        
        enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;
        enum enGender { Male, Female }
        private clsPerson _Person = new clsPerson();
        private int _PersonID = -1;
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void _LoadData()
        {
            _FillingComboBoxOfCountries();
            DTDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            DTDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        this.Text = "Add New Person";
                        lblTitle.Text = "Add New Person";
                        cbCountries.SelectedIndex = cbCountries.FindString("Egypt");
                        return;
                    }
                case enMode.Update:
                    {
                        _Person = clsPerson.Find(_PersonID);
                        if (_Person == null)
                        {
                            MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.Close();
                            return;
                        }
                        this.Text = "Edit Person";
                        lblTitle.Text = "Edit Person";
                        lblShowingID.Text = _Person.PersonID.ToString();
                        txtFirstName.Text = _Person.FirstName;
                        txtSecondName.Text = _Person.SecondName;
                        txtThirdName.Text = _Person.ThirdName;
                        txtLastName.Text = _Person.LastName;
                        txtNationalNo.Text = _Person.NationalNo;
                        DTDateOfBirth.Value = _Person.DateOfBirth;

                        if (_Person.Gender == 0) rbMale.Checked = true;
                        else rbFemale.Checked = true;

                        txtPhone.Text = _Person.Phone;
                        txtEmail.Text = _Person.Email;
                        txtAddress.Text = _Person.Address;
                        cbCountries.SelectedIndex = cbCountries.FindString(_Person.Country.CountryName);
                        if (_Person.ImagePath != "")
                        {
                            PicBoxM_F.ImageLocation = _Person.ImagePath;
                            lnkRemoveImage.Visible = true;
                        }
                        return;
                    }
            }
        }
        private bool _HandleImage()
        {
            if (_Person.ImagePath != PicBoxM_F.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException) { }
                }
                if (PicBoxM_F.ImageLocation != null)
                {
                    string SourceImageFile = PicBoxM_F.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectFolderImages(ref SourceImageFile))
                    {
                        PicBoxM_F.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }
        private void _FillingComboBoxOfCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow country in dt.Rows)
            {
                cbCountries.Items.Add(country["CountryName"].ToString());
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid, put the mouse over the red icon(s) to see the error(s).",
                   "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_HandleImage()) return;
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Gender = (rbMale.Checked == true) ? (sbyte)enGender.Male : (sbyte)enGender.Female;
            _Person.DateOfBirth = DTDateOfBirth.Value;
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.CountryID = clsCountry.FindCountryByName(cbCountries.SelectedItem.ToString()).CountryID;
            _Person.ImagePath = PicBoxM_F.ImageLocation == null ? "" : PicBoxM_F.ImageLocation;
            
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_Person.Save())
                        {
                            lblShowingID.Text = _Person.PersonID.ToString();
                            _Mode = enMode.Update;
                            MessageBox.Show($"Added Person Successfully with ID : {_Person.PersonID} !",
                                "New Person!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lblTitle.Text = "Edit Person";
                            this.Text = "Edit Person";
                            DataBack?.Invoke(this, _Person.PersonID);
                        }
                        else MessageBox.Show("Added Person Failed !",
                                "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                case enMode.Update:
                    {
                        lblShowingID.Text = _Person.PersonID.ToString();
                        if (_Person.Save())
                        {
                            MessageBox.Show($"Person with ID : {_Person.PersonID} Updated Successfully !",
                                "Updated!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataBack?.Invoke(this, _Person.PersonID);
                        }
                        else MessageBox.Show("Update Failed !",
                                "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (PicBoxM_F.ImageLocation == null)
                PicBoxM_F.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (PicBoxM_F.ImageLocation == null)
                PicBoxM_F.Image = Resources.Female_512;
        }

        private void lnkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFilePics.InitialDirectory = "C:\\Users\\AIO\\source\\Images";
            OpenFilePics.Title = "Choose a pic";
            OpenFilePics.Filter = "All Images (*.*)|*.*|Images (.png)|*.png|Images (.jpg)|*.jpg|Images (.ico)|*.ico";
            OpenFilePics.FilterIndex = 1;
            if (OpenFilePics.ShowDialog() == DialogResult.OK)
            {
                PicBoxM_F.Load(OpenFilePics.FileName);
                lnkRemoveImage.Visible = true;
            }
        }
        private void lnkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            PicBoxM_F.ImageLocation = null;
            PicBoxM_F.Image = rbMale.Checked == true ? Resources.Male_512 : Resources.Female_512;
            lnkRemoveImage.Visible = false;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == string.Empty)
            {
                errorProvider1.SetError(txtEmail, null);
                return;
            }
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid Email address format");
                return;
            }
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            
    }

        private void txt_Validating(object sender, CancelEventArgs e)
        {
            // Cast sender to TextBox
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "This field is required!"); // Set the error message
            }
            else
            {
                e.Cancel = false; // Validation is successful
                errorProvider1.SetError(textBox, null); // Clear the error message
            }
        }
        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null); 
            }
            if (clsPerson.IsNationalNoExist(txtNationalNo.Text) && _Person.NationalNo != txtNationalNo.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This national number is used for another person!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
