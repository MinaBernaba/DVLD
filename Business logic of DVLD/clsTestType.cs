using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLD
{
    public class clsTestType
    {

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }
        private clsTestType(enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees) {
            
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
        public static clsTestType Find(enTestType TestTypeID)
        {
          string TestTypeTitle = string.Empty , TestTypeDescription = string.Empty ;
            decimal TestTypeFees = -1 ;
            if (clsTestTypesData.Find(Convert.ToByte(TestTypeID), ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else return null;
        }
        public bool Save()
        {
            return clsTestTypesData.Update(Convert.ToByte(TestTypeID), TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

    }
}
