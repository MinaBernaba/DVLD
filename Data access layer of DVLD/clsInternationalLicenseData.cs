using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class clsInternationalLicenseData
    {
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM InternationalLicenses";
            SqlCommand cmd = new SqlCommand(Query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return dataTable;
        }
        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[InternationalLicenses] " +
                "SET [ApplicationID] = @ApplicationID " +
                " ,[DriverID] = @DriverID " +
                ",[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID " +
                ",[IssueDate] = @IssueDate " +
                ",[ExpirationDate] = @ExpirationDate " +
                ",[IsActive] = @IsActive " +
                ",[CreatedByUserID] = @CreatedByUserID " +
                " WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO [dbo].[InternationalLicenses] ([ApplicationID],[DriverID],[IssuedUsingLocalLicenseID],[IssueDate]," +
                " [ExpirationDate],[IsActive],[CreatedByUserID]) " +
                "VALUES  (@ApplicationID ,@DriverID ,@IssuedUsingLocalLicenseID ,@IssueDate ,@ExpirationDate ,@IsActive ,@CreatedByUserID );" +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out InternationalLicenseID);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return InternationalLicenseID;
        }
        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "DELETE FROM [dbo].[InternationalLicenses] WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool Find(int InternationalLicenseID,ref int ApplicationID,ref int DriverID, ref int IssuedUsingLocalLicenseID,
         ref DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool IsExist(int InternationalLicenseID)
        {
            bool IsExist = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT EXIST = 1 FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) IsExist = true;
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsExist;
        } 

    }
}
