using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessDVLD
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT * FROM LocalDrivingLicenseApplications_View " +
                " ORDER BY  [Application Date] DESC";
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
                " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
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
        public static int PassedTests(int LocalDrivingLicenseApplicationID)
        {
            int PassedTests = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            String Query = "SELECT [Passed Tests] FROM LocalDrivingLicenseApplications_View " +
                "WHERE [L.D.L.AppID] = @LocalDrivingLicenseApplicationID ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out PassedTests);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return PassedTests;
        }
        public static bool DoesApplicationHaveAnActiveAppointment(int LocalDrivingLicenseApplicationID)
        {
            bool HaveAnAppointment = false;
        SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Found = 1 FROM TestAppointments WHERE TestAppointments.IsLocked = 0 " +
                    "and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) HaveAnAppointment = true;

            } catch (Exception ex) { }
            finally { conn.Close(); }
            return HaveAnAppointment;
            }
        public static DataTable GetTestAppointmentsInfoAboutCertainTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT TestAppointments.TestAppointmentID,TestTypes.TestTypeTitle , TestAppointments.AppointmentDate, " +
                "TestAppointments.PaidFees , TestAppointments.IsLocked " +
                "FROM TestAppointments JOIN TestTypes ON TestTypes.TestTypeID = TestAppointments.TestTypeID   " +
                "WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID  " +
                "AND TestTypes.TestTypeID = @TestTypeID " +
                "ORDER BY TestAppointmentID DESC;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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
        public static bool DoesApplicationHaveAnAppointmentBeforeOnThisTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            bool HaveAnAppointmentBefore = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Found = 1 FROM TestAppointments WHERE  " +
                "LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID " +
                "And TestAppointments.TestTypeID = @TestTypeID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) HaveAnAppointmentBefore = true;

            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return HaveAnAppointmentBefore;
        }
        public static int TrialsCount(int LocalDrivingLicenseApplicationID , byte TestTypeID)
        {
            int TrialsCount = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT count(*) FROM TestAppointments JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                " WHERE TestAppointments.TestTypeID = @TestTypeID AND TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
        SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) int.TryParse(reader.ToString(), out TrialsCount);
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return TrialsCount;
        }
        public static bool DoesApplicantPassThisTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            bool PassTheTest = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT IsPassed = 1 FROM TestAppointments  " +
                "JOIN Tests ON tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                "WHERE tests.TestResult = 1 And TestAppointments.TestTypeID = @TestTypeID " +
                "And TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) PassTheTest = true;

            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return PassTheTest;
        }
        public static bool DoesLicenseIssuedForThisApplication(int LocalDrivingLicenseApplicationID)
        {
            bool IssuedLicense = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "SELECT Found = 1 FROM Licenses join Applications on Licenses.ApplicationID = Applications.ApplicationID " +
                "join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID " +
                "Where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
             try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null) IssuedLicense = true;

            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IssuedLicense;
        }
    }
    
}
