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
        public static string CountryName(int CountryID)
        {
            return clsCountryData.GetCountryName(CountryID);
        }
        public static byte CountryID(string CountryName)
        {
            return clsCountryData.GetCountryID(CountryName);
        }
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
