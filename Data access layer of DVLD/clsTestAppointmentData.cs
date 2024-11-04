using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class clsTestAppointmentData
    {
        public static DataTable GetAllTestAppointments()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM TestAppointments";
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
        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
         DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID,
        bool IsLocked, int RetakeTestApplicationID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[TestAppointments]" +
                " SET [TestTypeID] = @TestTypeID  " +
                " ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID " +
                " ,[AppointmentDate] = @AppointmentDate " +
                " ,[PaidFees] = @PaidFees " +
                " ,[CreatedByUserID] = @CreatedByUserID " +
                " ,[IsLocked] = @IsLocked " +
                " ,[RetakeTestApplicationID] = @RetakeTestApplicationID " +
                " WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (RetakeTestApplicationID == -1) 
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
         DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID,
        bool IsLocked, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO [dbo].[TestAppointments]" +
                "  ([TestTypeID] ,[LocalDrivingLicenseApplicationID] ,[AppointmentDate] ,[PaidFees]" +
                " ,[CreatedByUserID] ,[IsLocked] ,[RetakeTestApplicationID])" +
                " VALUES (@TestTypeID ,@LocalDrivingLicenseApplicationID ,@AppointmentDate ,@PaidFees ,@CreatedByUserID ,@IsLocked ,@RetakeTestApplicationID);" +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (RetakeTestApplicationID == -1)
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out TestAppointmentID);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return TestAppointmentID;
        }
        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "DELETE FROM [dbo].[TestAppointments] WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool Find(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,
        ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,
        ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else 
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool IsExist(int TestAppointmentID)
        {
            bool IsExist = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT EXIST = 1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
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
        public static bool LockTheTestAppointment(int TestAppointmentID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = " UPDATE TestAppointments  SET IsLocked = 1  WHERE TestAppointmentID = @TestAppointmentID;";
            SqlCommand cmd = new SqlCommand(Query,conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentID);
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
