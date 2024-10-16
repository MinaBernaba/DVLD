using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogicOfDVLD
{
    public class clsUser
    {
        enum enMode {AddNew = 0 , Update = 1}
        enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            Mode = enMode.AddNew;
        }
        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }
        public static clsUser FindUser(int UserID)
        {
            int PersonID = -1;
            string UserName = string.Empty, Password = string.Empty;
            bool IsActive = false;
            if (clsUserData.FindByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else return null;
        }
        public static clsUser FindUser(string Username , string Password)
        {
            int PersonID = -1;
            int UserID = -1;
            bool IsActive = false;
            if (clsUserData.FindByUsernameAndPassword(ref UserID, ref PersonID, Username, Password, ref IsActive))
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            else return null;
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = string.Empty, Password = string.Empty;
            bool IsActive = false;
            if (clsUserData.FindByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else return null;
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static bool IsUserIDExist(int UserID)
        {
            return clsUserData.IsExist(UserID);
        }
        public static bool IsUserNameExist(string UserName)
        {
            return clsUserData.IsExistByPersonID(UserName);
        }
        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserData.IsExist(PersonID);
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID,this.UserName,this.Password,this.IsActive);
            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID,this.PersonID, this.UserName, this.Password, this.IsActive);
            
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else return false;
                    }
                case enMode.Update:
                    {
                        if (_UpdateUser()) return true;
                        else return false;
                    }
            }
            return false;
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
    }
}
