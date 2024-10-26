using DataAccessDVLD;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsDriver
    {
        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.FindUser(CreatedByUserID);
            this.CreatedDate = CreatedDate;
            Mode = enMode.Update;
        }
        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = 0, CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.Find(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID,PersonID, CreatedByUserID, CreatedDate);
            else return null;
        }
        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = 0, CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.Find(ref DriverID, PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else return null;
        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return this.DriverID != -1;
        }
        private bool _Update()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }
        public bool Save()
        {
            switch (Mode) { 
            case enMode.AddNew:
                 {
                     if (_AddNewDriver())
                     {
                         Mode = enMode.Update;
                         return true;
                     }
                     else return false;
                 }
                 case enMode.Update: {
                 return _Update();
                 }
            }
            return false;
        }
        public static bool Delete(int DriverID)
        {
            return clsDriverData.DeleteDriver(DriverID);
        }
        public static bool IsExist(int DriverID)
        {
            return clsDriverData.IsExist(DriverID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }
    }
}
