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
    public class clsTestData
    {
        public static DataTable GetAllTests()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM tests";
            SqlCommand cmd = new SqlCommand(Query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex)  { }
            finally { conn.Close(); }
            return dataTable;
        }
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[Tests]" +
                " SET [TestAppointmentID] = @TestAppointmentID " +
                " ,[TestResult] = @TestResult " +
                "  ,[Notes] = @Notes " +
                " ,[CreatedByUserID] = @CreatedByUserID" +
                " WHERE TestID = @TestID ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestID", TestID);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes == string.Empty)
            cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Notes", Notes);
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
        public static int AddTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO [dbo].[Tests] ([TestAppointmentID] ,[TestResult] ,[Notes] ,[CreatedByUserID]) " +
                " VALUES ( @TestAppointmentID  , @TestResult   , @Notes  , @CreatedByUserID);" +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes == string.Empty)
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
               object reader = cmd.ExecuteScalar();
                int.TryParse(reader.ToString(), out TestID);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return TestID;
        }
        public static bool FindTest(int TestID,ref int TestAppointmentID,ref bool TestResult,ref string Notes,ref int CreatedByUserID)
        {
            bool  IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM tests WHERE TestID = @TestID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)
                        Notes = string.Empty;
                    else
                        Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool DeleteTest(int TestID) {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "DELETE FROM [dbo].[Tests] WHERE TestID = @TestID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
        public static bool IsExist(int TestID)
        {
            bool IsExist = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT IsExist = 1 FROM tests WHERE TestID = @TestID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestID", TestID);
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
