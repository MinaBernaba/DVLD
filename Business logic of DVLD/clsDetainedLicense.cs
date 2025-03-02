using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID    { get; set; }
        public clsLicense LicenseInfo { get; set; }
        public clsUser UserCreateInfo { get; set; }
        public clsUser UserReleaseInfo { get; set; }
        public clsApplication ReleaseApplicationInfo { get; set; }

        public clsDetainedLicense()
        {
            int DetainID = -1;
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased
        , DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.UserCreateInfo = clsUser.FindUser(CreatedByUserID);
            this.UserReleaseInfo = clsUser.FindUser(CreatedByUserID);
            this.ReleaseApplicationInfo = clsApplication.FindApplication(ReleaseApplicationID);
            Mode = enMode.Update;
        }
        public static clsDetainedLicense FindByDetainID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MaxValue; decimal FineFees = 0; bool IsReleased = false;
            if (clsDetainedLicenseData.FindByDetainID(DetainID, ref LicenseID, ref DetainDate, ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else return null;
        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MaxValue; decimal FineFees = 0; bool IsReleased = false;
            if (clsDetainedLicenseData.FindByLicenseID(ref DetainID, LicenseID, ref DetainDate, ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else return null;
        }
        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased);
            return this.DetainID != -1;
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID,this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDetainedLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateDetainedLicense();
                    }
            }
            return false;
        }
        public static bool Delete(int DetainID)
        {
            return clsDetainedLicenseData.Delete(DetainID);
        }
        public static bool IsExist(int DetainID)
        {
            return clsDetainedLicenseData.IsExist(DetainID);
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }

    }
}
