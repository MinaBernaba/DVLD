using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class clsCountryData
    {
        public static string GetCountryName(int CounteyID)
        {
            string CountryName = "";
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "Select CountryName from Countries where CountryID = @CountryID ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@CountryID", CounteyID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null)
                {
                    CountryName = reader.ToString();
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return CountryName;
        }
        public static byte GetCountryID(string CountryName)
        {
            byte CountryID = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "Select CountryID from Countries where CountryName = @CountryName ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null)
                {
                   byte.TryParse(reader.ToString(), out CountryID);
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return CountryID;
        }
        public static DataTable GetAllCountries()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "Select CountryName from Countries";
            SqlCommand cmd = new SqlCommand(Query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return dataTable;
        }
    }
}
