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
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person = new clsPerson();
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void ctrlPersonCard_Load(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            _LoadData();
        }
        private void ctrlPersonCard_Load(String NationalNo)
        {
        //    _Person = clsPerson.Find(PersonID);
            _LoadData();
        }
        private void _LoadData()
        {
            lblShowingID.Text = _Person.PersonID.ToString();
            lblName.Text = (_Person.ThirdName != "") ? _Person.FirstName + " " + _Person.SecondName +
                " " + _Person.ThirdName + " " + _Person.LastName
                : _Person.FirstName + " " + _Person.SecondName + " " + _Person.LastName;
            lblNationalNo.Text = _Person.NationalNo;
            if (_Person.Gender == 0)
            {
                lblGender.Text = "Male";
                PicGender.Image = Resources.Man_32;
            }
            else
            {
                lblGender.Text = "Female";
                PicGender.Image = Resources.Woman_32;
            }
            lblEmail.Text = (_Person.Email == "") ? "Unknown" : _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsCountry.CountryName(_Person.CountryID);
            if (_Person.ImagePath != "")
            {
                PicBoxPerson.Load(_Person.ImagePath);
            }
            else
            {
                if (_Person.Gender == 1) PicBoxPerson.Image = Resources.Female_512;
            }
        }
        private void lnkEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson EditForm = new frmAddUpdatePerson(_Person.PersonID);
            EditForm.ShowDialog();
            ctrlPersonCard_Load(_Person.PersonID);
        }
    }
}
