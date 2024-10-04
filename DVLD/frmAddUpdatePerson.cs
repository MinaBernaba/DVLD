using BusinessLogicOfDVLV;
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

namespace DVLD
{
    public partial class frmAddUpdatePerson : Form
    {
        enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;
        enum enGender { Male, Female }
        enGender _Gender = enGender.Male;
        private clsPerson _Person = new clsPerson();
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            if (PersonID == -1) _Mode = enMode.AddNew;
            else
            { 
               _Mode = enMode.Update;
                _Person = clsPerson.Find(PersonID);
            } 
        }
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void _LoadData()
        {
            _FillingComboBoxOfCountries();
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        lblTitle.Text = "Add New Person";
                        cbCountries.SelectedIndex = cbCountries.FindString("Egypt");
                        return;
                    }
                case enMode.Update:
                {

                        lblTitle.Text = "Edit Person";
                        lblShowingID.Text = _Person.PersonID.ToString();    
                        txtFirstName.Text = _Person.FirstName;
                        txtSecondName.Text = _Person.SecondName;
                        txtThirdName.Text = _Person.ThirdName;
                        txtLastName.Text = _Person.LastName;
                        txtNationalNo.Text = _Person.NationalNo;
                        DTDateOfBirth.Value = _Person.DateOfBirth;
                        if(_Person.Gender == 0) rbMale.Checked = true;
                        else rbFemale.Checked = true;
                        txtPhone.Text = _Person.Phone;
                        txtEmail.Text = _Person.Email;
                        txtAddress.Text = _Person.Address;
                        cbCountries.SelectedIndex = cbCountries.FindString(clsCountry.CountryName(_Person.CountryID));
                        if (_Person.ImagePath != "")
                        {
                            PicBoxM_F.Load(_Person.ImagePath);
                            lnkRemoveImage.Visible=true;
                        }
                return;
                }
            }
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
                MessageBox.Show("Please fill all required information!", "Incomplete Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Gender = (rbMale.Checked == true) ? enGender.Male : enGender.Female;
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Gender = (sbyte)_Gender;
            _Person.DateOfBirth = DTDateOfBirth.Value;
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.CountryID = clsCountry.CountryID(cbCountries.SelectedItem.ToString());
            _Person.ImagePath = PicBoxM_F.ImageLocation == null ? "" : PicBoxM_F.ImageLocation;
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if ( _Person.Save()){
                            lblShowingID.Text = _Person.PersonID.ToString();
                            MessageBox.Show($"Added Person Successfully with ID : {_Person.PersonID} !",
                                "New Person!", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            _Mode = enMode.Update;
                            lblTitle.Text = "Edit Person";
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
            if(OpenFilePics.ShowDialog() == DialogResult.OK)
            {
                PicBoxM_F.Load(OpenFilePics.FileName);
                lnkRemoveImage.Visible = true;
            }   
        }
        private void lnkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            PicBoxM_F.ImageLocation = null;
            PicBoxM_F.Image =  rbMale.Checked == true? Resources.Male_512 : Resources.Female_512;
            lnkRemoveImage.Visible=false;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == string.Empty || txtEmail.Text.Contains("@"))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
            else
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Please enter corrected Email");
            }
        }

        private void txt_Validating(object sender, CancelEventArgs e)
        { 
            // Cast sender to TextBox
                TextBox textBox = (TextBox)sender;

            if (textBox != null) // Check if the sender is a TextBox
                {
                if (string.IsNullOrWhiteSpace(textBox.Text)) // Check if the TextBox is empty
                {
                        e.Cancel = true; // Cancel the validation
                        textBox.Focus(); // Set focus back to the TextBox
                        errorProvider1.SetError(textBox, "Please fill the text box!"); // Set the error message
                }
                else
                {
                        e.Cancel = false; // Validation is successful
                        errorProvider1.SetError(textBox, ""); // Clear the error message
                }
            }
        }

    }
}
