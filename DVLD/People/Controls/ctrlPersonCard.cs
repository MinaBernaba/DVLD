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
using System.IO;

namespace DVLD
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;
        private int _PersonID ;
        public clsPerson SelectedPerson {  get { return _Person; } }
        public int PersonID { get { return _PersonID; } }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _PersonID = _Person.PersonID;
            _FillPersonInfo();
        }
        public void LoadPersonInfo(String NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null) {
                _ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _PersonID = _Person.PersonID;
            _FillPersonInfo();
        }
        private void _FillPersonInfo()
        {
            lblName.Text = (_Person.ThirdName != "") ? _Person.FirstName + " " + _Person.SecondName +
                " " + _Person.ThirdName + " " + _Person.LastName
                : _Person.FirstName + " " + _Person.SecondName + " " + _Person.LastName;
            lblShowingID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";
            lblEmail.Text = (_Person.Email == "") ? "Unknown" : _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.Country.CountryName;
            _HandleImage();
        }
        private void _HandleImage()
        {
            PicGender.Image = _Person.Gender == 0 ? Resources.Man_32 : Resources.Woman_32;
            if (_Person.ImagePath != "")
            {
                if (File.Exists(_Person.ImagePath))
                    PicBoxPerson.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + _Person.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else PicBoxPerson.Image = (_Person.Gender == 0) ? Resources.Male_512 : Resources.Female_512;
        }
        private void lnkEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson EditForm = new frmAddUpdatePerson(_PersonID);
            EditForm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
        private void _ResetPersonInfo()
        {
            _PersonID = -1;
            lblShowingID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblName.Text = "[????]";
            PicGender.Image = Resources.Man_32;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            PicBoxPerson.Image = Resources.Male_512;

        }
    }
}
