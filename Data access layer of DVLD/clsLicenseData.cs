﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{

    public class clsLicenseData
    {
        public static DataTable GetAllLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM Licenses";
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
        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes,
         decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query =
                "UPDATE [dbo].[Licenses]" +
                " SET [ApplicationID] = @ApplicationID " +
                "  ,[DriverID] = @DriverID " +
                ",[LicenseClass] = @LicenseClass " +
                " ,[IssueDate] = @IssueDate " +
                " ,[ExpirationDate] = @ExpirationDate " +
                " ,[Notes] = @Notes " +
                " ,[PaidFees] = @PaidFees " +
                " ,[IsActive] = @IsActive " +
                " ,[IssueReason] = @IssueReason " +
                " ,[CreatedByUserID] = @CreatedByUserID " +
                " WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (Notes != string.Empty)
               cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex){ }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static int AddNewLicense(int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes,
         decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO [dbo].[Licenses]" +
                " ([ApplicationID] ,[DriverID] ,[LicenseClass] ,[IssueDate] ,[ExpirationDate] " +
                " ,[Notes] ,[PaidFees] ,[IsActive] ,[IssueReason] ,[CreatedByUserID])" +
                " VALUES (@ApplicationID ,@DriverID ,@LicenseClass ,@IssueDate ,@ExpirationDate ," +
                "@Notes ,@PaidFees ,@IsActive ,@IssueReason ,@CreatedByUserID); " +
                "SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (Notes != string.Empty)
                cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) { int.TryParse(reader.ToString(), out LicenseID); }
            }
            catch (Exception ex ){ }
            finally { conn.Close(); }
            return LicenseID;
        }
        public static bool DeleteLicense(int LicenseID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "DELETE FROM [dbo].[Licenses] WHERE LicenseID = @LicenseID;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex ){ }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool Find(int LicenseID,ref int ApplicationID, ref int DriverID,
           ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate,
           ref string Notes, ref decimal PaidFees, ref bool IsActive,
           ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID ;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"] != DBNull.Value) 
                        Notes = (string)reader["Notes"];
                    else
                        Notes = string.Empty;
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex ){ }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool IsExist(int LicenseID)
        {
            bool IsExist = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Found = 1 FROM Licenses WHERE LicenseID = @LicenseID;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) IsExist = true;
            }
            catch (Exception ex ){ }
            finally { conn.Close(); }
            return IsExist;
        }
    } 
}