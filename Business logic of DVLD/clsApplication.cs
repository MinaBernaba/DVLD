using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsApplication
    {
       public enum enMode { AddNew, Update }
       public enMode Mode = enMode.AddNew;
       //public enum enApplicationType
       // {
       //     NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
       //     ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
       // };
       public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
       public int ApplicationID { get; set; }
       public int ApplicantPersonID { get; set; }
       public clsPerson PersonInfo { get; set; }
       public string ApplicantFullName { get { return clsPerson.Find(ApplicantPersonID).FullName; }}
       public DateTime ApplicationDate { get; set; }
       public byte ApplicationTypeID { get; set; }
       public clsApplicationType ApplicationTypeInfo { get; set; }
       public enApplicationStatus ApplicationStatus { get; set; }
       public string StatusText {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            } 
        }
       public DateTime LastStatusDate { get; set; }
       public decimal PaidFees { get; set; }
       public int CreatedByUserID { get; set; }
       public clsUser UserInfo { get; set; }
        
       public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 0;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
       private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, byte ApplicationTypeID,
        enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
        decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.FindUser(CreatedByUserID);
            Mode = enMode.Update;
        }
       public static clsApplication FindApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1,CreatedByUserID = -1;
            byte ApplicationTypeID = 0, ApplicationStatus = 1;
            decimal PaidFees = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            
            if (clsApplicationData.Find(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
         ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
          (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else return null;
        }
       private bool _AddNewApplication()
        {
            this.ApplicantPersonID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
          Convert.ToByte(this.ApplicationStatus), this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return this.ApplicantPersonID != -1;
        }
       private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
          Convert.ToByte(this.ApplicationStatus), this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }
       public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                {
                        if (_AddNewApplication())
                        {
                            Mode = enMode.Update; 
                            return true;
                        }
                     return false;
                }
                case enMode.Update:
                    {
                        return (_UpdateApplication());
                    }
            }
            return false;
        }
       public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
       public  bool DeleteApplication()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
       public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsExist(ApplicationID);
        }
       public static DataTable GetAllApplications()
        {
            return (clsApplicationData.GetAllApplications());
        }
    }
}
