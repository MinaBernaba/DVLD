using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsTestAppointment
    {
        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew;
        int TestAppointmentID { get; set; }
        int TestTypeID { get; set; }
        int LocalDrivingLicenseApplicationID { get; set; }
        DateTime AppointmentDate { get; set; }
        decimal PaidFees { get; set; }
        int CreatedByUserID { get; set; }
        bool IsLocked { get; set; }
        int RetakeTestApplicationID { get; set; }
        clsTestType TestTypeInfo { get; set; }
        clsLocalDrivingLicenseApplication LDLA_Info { get; set; }
        clsUser UserInfo { get; set; }
        clsApplication RetakeTestApplication { get; set; }
        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.MinValue;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
         DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID,
        bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.TestTypeInfo = clsTestType.Find((clsTestType.enTestType)TestTypeID);
            this.LDLA_Info = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            this.UserInfo = clsUser.FindUser(CreatedByUserID);
            this.RetakeTestApplication = clsApplication.FindApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }
        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, RetakeTestApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = 0; bool IsLocked = false;
            if (clsTestAppointmentData.Find(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
             ref AppointmentDate, ref PaidFees, ref CreatedByUserID,
             ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
         AppointmentDate, PaidFees, CreatedByUserID,
         IsLocked, RetakeTestApplicationID);
            }
            else return null;
        }
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees,
                this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
            return this.TestAppointmentID != -1;
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees,
                this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }
        public bool Save()
        {
            switch (Mode) {
                case enMode.AddNew: {

                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                }
                case enMode.Update: { 
                    return _UpdateTestAppointment();
                }
            }
            return false;
        }
        public static bool Delete(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }
        public bool Delete()
        {
            return clsTestAppointmentData.DeleteTestAppointment(this.TestAppointmentID);
        }
        public static bool IsExist(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsExist(TestAppointmentID);
        }
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }
    }
}
