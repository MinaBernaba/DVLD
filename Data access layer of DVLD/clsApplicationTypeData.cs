using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessDVLD
{
    public class clsApplicationTypeData
    {
        /*  
         string ApplicationTypeTitle ,decimal ApplicationFees, byte ApplicationTypeID
         */
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM ApplicationTypes";
            SqlCommand cmd = new SqlCommand(Query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return dt;
        }
        public static bool UpdateApplicationTypes(byte ApplicationTypeID, string ApplicationTypeTitle,
            decimal ApplicationFees)
        {
            int RowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[ApplicationTypes]" +
                " SET [ApplicationTypeTitle] = @ApplicationTypeTitle" +
                " ,[ApplicationFees] = @ApplicationFees " +
                " WHERE ApplicationTypeID = @ApplicationTypeID ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            cmd.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return RowsAffected > 0;

        }
        public static bool FindApplicationType(byte ApplicationTypeID, ref string ApplicationTypeTitle,
            ref decimal ApplicationFees)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (decimal)reader["ApplicationFees"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }

    }
}