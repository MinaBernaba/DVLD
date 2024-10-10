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
        public static bool FindCountryByID(byte CounteyID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "Select CountryName from Countries where CountryID = @CountryID ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@CountryID", CounteyID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return isFound;
        }
        public static bool FindCountryByName(string CountryName, ref byte CounteyID)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "Select * from Countries where CountryName = @CountryName ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    isFound = true;
                    CounteyID = (byte)(int)reader["CountryID"];
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return isFound;
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
