using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogicOfDVLD
{
    public class clsLicenseClass
    {
        enum enMode { AddNew, Update }
        enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        public clsLicenseClass()
        {
            LicenseClassID = 0;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinimumAllowedAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0;
            Mode = enMode.AddNew;
        }
        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }
        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = string.Empty, ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;
            if (clsLicenseClassData.Find(LicenseClassID, ref ClassName, ref ClassDescription,
               ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
              MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else return null;
        }
        public static clsLicenseClass Find(string ClassName)
        {
            string ClassDescription = string.Empty;
            int LicenseClassID = -1;
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;
            if (clsLicenseClassData.Find(ref LicenseClassID, ClassName, ref ClassDescription,
               ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
              MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else return null;
        }
        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNew(this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
            return this.LicenseClassID != -1;
        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.Update(this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicenseClass())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }

                case enMode.Update:
                    {
                        return _UpdateLicenseClass();
                    }
            }
            return false;
        }
        public static bool Delete(int LicenseClassID)
        {
            return clsLicenseClassData.Delete(LicenseClassID);
        }
        public static bool IsExist(int LicenseClassID)
        {
            return clsLicenseClassData.IsExist(LicenseClassID);
        }
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
    }
}
