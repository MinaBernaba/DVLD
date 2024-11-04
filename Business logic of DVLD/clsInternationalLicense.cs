using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;
        int InternationalLicenseID { get; set; }
        int ApplicationID { get; set; }
        int DriverID { get; set; }
        int IssuedUsingLocalLicenseID { get; set; }
        DateTime IssueDate { get; set; }
        DateTime ExpirationDate { get; set; }
        bool IsActive { get; set; }
        int CreatedByUserID { get; set; }
        clsApplication ApplicationInfo { get; set; }
        clsDriver DriverInfo { get; set; }
        clsUser UserInfo { get; set; }
        clsLicense LocalLicenseInfo { get; set; }
        public clsInternationalLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MaxValue;
            IsActive = false;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        public clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
        DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicationInfo = clsApplication.FindApplication(ApplicationID);
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.UserInfo = clsUser.FindUser(CreatedByUserID);
            this.LocalLicenseInfo = clsLicense.Find(IssuedUsingLocalLicenseID);
            Mode = enMode.Update;
        }
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue; bool IsActive = false;
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            if (clsInternationalLicenseData.Find(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else return null;
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
             this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }
        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID,this.ApplicationID, this.DriverID,
                this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewInternationalLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }
                case enMode.Update:
                    {
                        return (_UpdateInternationalLicense());
                    }
            }
            return false;
        }
        public static bool Delete(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseID);
        }
        public static bool IsExist(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.IsExist(InternationalLicenseID);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }
    }
}
