using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicOfDVLV
{
    public class clsCountry
    {
        public byte CountryID { get; set; }
        public string CountryName { get; set; }
        public clsCountry() {
            this.CountryID = 0;
            this.CountryName = "";
        }
        private clsCountry(byte CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static clsCountry FindCountryByID(byte CountryID)
        {
            string CountryName = "";
            if (clsCountryData.FindCountryByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else return null;
        }
        public static clsCountry FindCountryByName(string CountryName)
        {
            byte CountryID = 0;
            if (clsCountryData.FindCountryByName(CountryName, ref CountryID))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else return null;
        }
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
