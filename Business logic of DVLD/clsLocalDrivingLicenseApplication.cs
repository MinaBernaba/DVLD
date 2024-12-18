﻿using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogicOfDVLD
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew, Update};
        new enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID {  get; set; }
        public byte LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = 0;
            Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,
            byte LicenseClassID , int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
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
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            byte LicenseClassID = 0; int ApplicationID = -1;
            if (clsLocalDrivingLicenseApplicationData.Find(LocalDrivingLicenseApplicationID,
                ref ApplicationID, ref LicenseClassID))
            {
                clsApplication Application = FindApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                    LicenseClassID, Application.ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID,
                    Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID);
            }
            else return null;
        }
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1; byte LicenseClassID = 0;
            if (clsLocalDrivingLicenseApplicationData.Find(ref LocalDrivingLicenseApplicationID,
                ApplicationID, ref LicenseClassID))
            {
                clsApplication Application = FindApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                    LicenseClassID, Application.ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID,
                    Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID);
            }
            else return null;
        }
        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if (clsLocalDrivingLicenseApplicationData.Delete(LocalDrivingLicenseApplicationID))
            {
                return DeleteApplication(localDrivingLicenseApplication.ApplicationID);
            }
            return false;
        }
        public bool DeleteLocalDrivingLicenseApplication()
        {
            if (clsLocalDrivingLicenseApplicationData.Delete(this.LocalDrivingLicenseApplicationID)) {
                return base.DeleteApplication();
            }
            return false;
        }
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNew(
                this.ApplicationID, this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.Update(this.LocalDrivingLicenseApplicationID,
                this.ApplicationID, this.LicenseClassID);
        }
        public new bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save()) return false;
                switch (Mode)
                {
                    case enMode.AddNew:
                        {
                            if (_AddNewLocalDrivingLicenseApplication())
                            {
                                Mode = enMode.Update;
                                return true;
                            }
                            return false;
                        }
                    case enMode.Update:
                        {
                            return _UpdateLocalDrivingLicenseApplication();
                        }
                }
                return false;
            }
        public static DataTable GetAllLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public static bool IsLocalDrivingLicenseApplicationExist(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsExist(LocalDrivingLicenseApplicationID);
        }
        public int NumberOfPassedTests()
        {
            return clsLocalDrivingLicenseApplicationData.PassedTests(this.LocalDrivingLicenseApplicationID);
        }
        public bool DoesApplicantHaveAnActiveTestAppointment()
        {
            return clsLocalDrivingLicenseApplicationData.DoesApplicationHaveAnActiveAppointment(this.LocalDrivingLicenseApplicationID);
        }
        public DataTable GetTestAppointmentsInfoAboutCertainTestType(byte TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.GetTestAppointmentsInfoAboutCertainTestType(this.LocalDrivingLicenseApplicationID,TestTypeID);
        }
        public bool DoesApplicationHaveAnAppointmentBeforeOnThisTest(byte TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesApplicationHaveAnAppointmentBeforeOnThisTest(this.LocalDrivingLicenseApplicationID,TestTypeID);
        }
        public static int TrailsCount(int LocalDrivingLicenseApplicationID , byte TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TrialsCount(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public int TrailsCount(byte TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TrialsCount(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public bool DoesApplicantPassThisTest(byte TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesApplicantPassThisTest(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public bool DoesLicenseIssuedForThisApplication()
        {
            return clsLocalDrivingLicenseApplicationData.DoesLicenseIssuedForThisApplication(this.LocalDrivingLicenseApplicationID);
        }
        public int IssueLicense(string Notes, int UserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = UserID;
            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }

            else
                return -1;

        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
    }
}
