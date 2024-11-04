using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsTest
    {
        enum enMode { AddNew, Update }
        enMode Mode = enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get; set; }
        public clsUser UserInfo { get; set; }
        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            UserInfo = clsUser.FindUser(CreatedByUserID);
            Mode = enMode.Update;
        }
        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1; bool TestResult = false; string Notes = string.Empty;
            if (clsTestData.FindTest(TestID , ref TestAppointmentID,ref TestResult,ref Notes,ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else return null;
        }
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return this.TestID != -1;
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {

                        if (_AddNewTest())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateTest();
                    }
            }
            return false;
        }
        public static bool Delete(int TestID)
        {
            return clsTestData.DeleteTest(TestID);
        }
        public bool Delete()
        {
            return clsTestData.DeleteTest(this.TestID);
        }
        public static bool IsExist(int TestID)
        {
            return clsTestData.IsExist(TestID);
        }
        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }
    }
}
