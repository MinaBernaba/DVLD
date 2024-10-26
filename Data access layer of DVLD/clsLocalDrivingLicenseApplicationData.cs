﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessDVLD
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM [LocalDrivingLicenseApplications]";
            SqlCommand cmd = new SqlCommand(Query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) dataTable.Load(reader);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return dataTable;
        }
        public static int  AddNew(int ApplicationID , byte LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO [dbo].[LocalDrivingLicenseApplications]" +
                "  ([ApplicationID] ,[LicenseClassID]) " +
                " VALUES  (@ApplicationID ,@LicenseClassID);" +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out LocalDrivingLicenseApplicationID);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return LocalDrivingLicenseApplicationID;
        }
        public static bool Update(int LocalDrivingLicenseApplicationID, int ApplicationID, byte LicenseClassID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[LocalDrivingLicenseApplications]" +
                " SET [ApplicationID] =@ApplicationID " +
                " ,[LicenseClassID] =@LicenseClassID " +
                " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool Delete(int LocalDrivingLicenseApplicationID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "DELETE FROM [dbo].[LocalDrivingLicenseApplications]" +
                " WHERE  LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool Find(int LocalDrivingLicenseApplicationID,ref int ApplicationID,ref byte LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM LocalDrivingLicenseApplications" +
                " WHERE LocalDrivingLicenseApplications = @LocalDrivingLicenseApplications";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { 
                IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool Find(ref int LocalDrivingLicenseApplicationID, int ApplicationID, ref byte LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM LocalDrivingLicenseApplications" +
                " WHERE ApplicationID = @ApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool IsExist(int LocalDrivingLicenseApplicationID)
        {
            bool IsExist = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Found = 1 FROM [dbo].[LocalDrivingLicenseApplications]" +
                " WHERE  LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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
        public static int DoesApplicantHaveAnActiveLocalApplication
            (int ApplicantPersonID,byte ApplicationTypeID, byte LicenseClassID)
        {
            int ActiveApplication = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT IsFound = 1 FROM LocalDrivingLicenseApplications JOIN Applications " +
                "ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID " +
                "WHERE Applications.ApplicantPersonID = @ApplicantPersonID " +
                "AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID " +
                "And Applications.ApplicationTypeID = @ApplicationTypeID " +
                "And Applications.ApplicationStatus = 1;";
            SqlCommand cmd = new SqlCommand (Query, conn);
                
            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if(reader != null) int.TryParse(reader.ToString(),out ActiveApplication);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return ActiveApplication;
        }
        public static bool DoesApplicantAlreadyHaveALicenseInTheSameLicenseClass
            (int ApplicationID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT ISExist = 1 FROM Licenses " +
                "WHERE Licenses.ApplicationID = @ApplicationID; ";
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

    }
}