using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogicOfDVLD
{
    public class clsApplicationType
    {
        public byte ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }
        private clsApplicationType(byte ApplicationTypeID, string ApplicationTypeTitle,
            decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
        public static clsApplicationType Find(byte ApplicationTypeID)
        {
            string ApplicationTypeTitle = string.Empty;
            decimal ApplicationFees = -1;
            if (clsApplicationTypeData.FindApplicationType(ApplicationTypeID,
                ref ApplicationTypeTitle, ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else return null;
            
        }
        public bool Save()
        {
            return clsApplicationTypeData.UpdateApplicationTypes
               (this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }
    }
}
