using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsLicense
    {
        enum enMode { AddNew, Update }
        enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3,
            LostReplacement = 4 };

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID {  get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText { 
            get {
                    switch (this.IssueReason)
                    {
                        case enIssueReason.FirstTime:
                            return "First Time";
                        case enIssueReason.Renew:
                            return "Renew";
                        case enIssueReason.DamagedReplacement:
                            return "Replacement for Damaged";
                        case enIssueReason.LostReplacement:
                            return "Replacement for Lost";
                        default:
                            return "First Time";
                    }
                }
            }
        public int CreatedByUserID { get; set; }
        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        }
        public clsApplication ApplicationInfo { get; set; }
        public clsDriver DriverInfo { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public clsUser UserInfo { get; set; }
        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = 0;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes =string.Empty;
            PaidFees = 0;
            IsActive = false;
            IssueReason = enIssueReason.FirstTime;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
        DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees,
        bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicationInfo = clsApplication.FindApplication(ApplicationID);
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            this.UserInfo = clsUser.FindUser(CreatedByUserID);
            this.Mode = enMode.Update;
        }
        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, CreatedByUserID = -1, LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = string.Empty; decimal PaidFees = 0; bool IsActive = false;
            byte IssueReason = 0;
            if(clsLicenseData.Find(LicenseID,ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate , ref ExpirationDate, ref Notes, ref PaidFees,
                ref IsActive, ref IssueReason , ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID,
                IssueDate, ExpirationDate, Notes, PaidFees,
                IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            }
            else return null;
        }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive,Convert.ToByte(this.IssueReason), this.CreatedByUserID);
            return this.LicenseID != -1;
        }
        private bool _Update()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, Convert.ToByte(this.IssueReason), this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                         return false;
                    }
                case enMode.Update:
                    {
                        return _Update();
                    }
            }
            return false;
        }
        public static bool Delete(int LicenseID)
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }
        public static bool IsExist(int LicenseID)
        {
            return clsLicenseData.IsExist(LicenseID);
        }
        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }
        public static bool DoesApplicantAlreadyHaveALicenseInTheSameLicenseClass(int ApplicantPersonID, byte LicenseClassID)
        {
            return clsLicenseData.DoesApplicantAlreadyHaveALicenseInTheSameLicenseClass(ApplicantPersonID, LicenseClassID);
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
    }
}
