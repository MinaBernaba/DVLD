using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessDVLD
{
    public class clsApplicationData
    {
        public static DataTable GetAllApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM Applications";
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
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
         int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO [dbo].[Applications]" +
                " ([ApplicantPersonID]" +
                " ,[ApplicationDate]" +
                " ,[ApplicationTypeID]" +
                " ,[ApplicationStatus]" +
                " ,[LastStatusDate]" +
                " ,[PaidFees]" +
                " ,[CreatedByUserID])" +
                " VALUES" +
                " (@ApplicantPersonID ,@ApplicationDate ,@ApplicationTypeID ,@ApplicationStatus" +
                " ,@LastStatusDate ,@PaidFees ,@CreatedByUserID); " +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out ApplicationID);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return ApplicationID;

        }
        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
         int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[Applications]" +
                " SET [ApplicantPersonID] = @ApplicantPersonID " +
                "  ,[ApplicationDate] = @ApplicationDate " +
                " ,[ApplicationTypeID] = @ApplicationTypeID " +
                " ,[ApplicationStatus] = @ApplicationStatus " +
                " ,[LastStatusDate] = @LastStatusDate " +
                " ,[PaidFees] = @PaidFees " +
                " ,[CreatedByUserID] = @CreatedByUserID " +
                " WHERE ApplicationID = @ApplicationID ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
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
        public static bool DeleteApplication(int ApplicationID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "DELETE FROM [dbo].[Applications] WHERE ApplicationID = @ApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool Find(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref byte ApplicationTypeID,
         ref byte ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM [dbo].[Applications] WHERE ApplicationID = @ApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = Convert.ToByte(reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool IsExist(int ApplicationID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Found = 1 FROM [dbo].[Applications] WHERE ApplicationID = @ApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) IsFound = true;
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static int DoesApplicantHaveAnActiveLocalApplication
           (int ApplicantPersonID, byte ApplicationTypeID, byte LicenseClassID)
        {
            int ActiveApplication = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Applications.ApplicationID FROM LocalDrivingLicenseApplications JOIN Applications " +
                "ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID " +
                "WHERE Applications.ApplicantPersonID = @ApplicantPersonID " +
                "AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID " +
                "And Applications.ApplicationTypeID = @ApplicationTypeID " +
                "And Applications.ApplicationStatus = 1;";
            SqlCommand cmd = new SqlCommand(Query, conn);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out ActiveApplication);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return ActiveApplication;
        }
        public static bool UpdateStatus(int ApplicationID, byte ApplicationStatus)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[Applications]" +
                " SET [ApplicationStatus] = @ApplicationStatus, " +
                "[LastStatusDate] = @LastStatusDate " +
                " WHERE ApplicationID = @ApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
    }

}
