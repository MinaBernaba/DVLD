using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessDVLD;

namespace BusinessLogicOfDVLV
{
    public class clsPerson
    {
        public enum enMode {AddNew , Update};
        enMode Mode = enMode.AddNew;
        public int    PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public sbyte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte CountryID { get; set; }
        public string ImagePath { get; set; }
        public clsPerson()
        {
            PersonID = -1;
            NationalNo = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Gender = -1;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CountryID = 0;
            ImagePath = string.Empty;
            Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, sbyte Gender, string Address, string Phone, string Email, byte CountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            this.Mode = enMode.Update;
        }
        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "",
           Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.MinValue;
            sbyte Gender = -1 ;
            byte CountryID = 0;

            if (clsPeopleData.Find(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
               ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone, Email, CountryID, ImagePath);
            }
            else return null;
        }
        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "",
           Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.MinValue;
            int PersonID = -1;
            sbyte Gender = -1;
            byte CountryID = 0;

            if (clsPeopleData.Find(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
               ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone, Email, CountryID, ImagePath);
            }
            else return null;
        }
        public static bool IsExist(int PersonID)
        {
            return clsPeopleData.IsExist(PersonID);
        }
        public static bool IsExist(string NationalNo)
        {
            return clsPeopleData.IsExist(NationalNo);
        }
        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeopleFromDB();
        }
        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.CountryID, this.ImagePath);
            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return (clsPeopleData.UpdatePerson(this.PersonID,this.NationalNo, this.FirstName, this.SecondName,
                  this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                  this.Address, this.Phone, this.Email, this.CountryID, this.ImagePath));
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else return false;
                    }
                    case enMode.Update: {
                        return _UpdatePerson();
                    }
            }
            return false;

        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }
    }
}
