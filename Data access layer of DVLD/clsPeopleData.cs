using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Reflection;
using System.Security.Policy;

namespace DataAccessDVLD
{
    public class clsPeopleData
    {
        public static bool Find(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref sbyte Gender, ref string Address, ref string Phone, ref string Email, ref byte NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);

            string Query = "select * from people where PersonID = @PersonID;";

            SqlCommand cmd = new SqlCommand(Query, conn);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = reader["ThirdName"] != DBNull.Value ? ThirdName = (string)reader["ThirdName"] : "";
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (sbyte)(byte)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                    NationalityCountryID = (byte)(int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                }
                reader.Close();
            }
            catch (Exception ex){}
            finally
            {
                conn.Close();
            }
            return IsFound;
        }
        public static bool Find(ref int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
           ref DateTime DateOfBirth, ref sbyte Gender, ref string Address, ref string Phone, ref string Email, ref byte NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "select * from people where NationalNo = @NationalNo;";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = reader["ThirdName"] != DBNull.Value ? ThirdName = (string)reader["ThirdName"] : "";
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (sbyte)(byte)(reader["Gender"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                    NationalityCountryID = (byte)(int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }
        public static DataTable GetAllPeopleFromDB()
        {
            DataTable dataTable = new DataTable();

            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);

            string query = "select People.PersonID, People.NationalNo , People.FirstName , People.SecondName,People.ThirdName,People.LastName , People.DateOfBirth , Gender =  case when People.Gender = 0 then 'Male'" +
                " when People.Gender = 1 then 'Female' else 'Unknown' end , People.Address , People.Phone, People.Email ,Nationality = Countries.CountryName , People.ImagePath from People inner join Countries on People.NationalityCountryID = Countries.CountryID;";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return dataTable;
        }
        public static bool IsExist(int PersonID) { 
        bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);

            string Query = "select top 1 Isfound = 5 from People where PersonID = @persionID";

            SqlCommand cmd = new SqlCommand(Query, conn);

            cmd.Parameters.AddWithValue("@persionID", PersonID);

            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if(reader != null)
                {
                    IsFound = true;
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static bool IsExist(String NationalNo)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "select top 1 Isfound = 5 from People where NationalNo = @NationalNo";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null)
                {
                    IsFound = true;
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return IsFound;
        }
        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
           DateTime DateOfBirth, sbyte Gender, string Address, string Phone, string Email, byte NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "INSERT INTO People (NationalNo ,FirstName ,SecondName ,ThirdName ,LastName ,DateOfBirth ,Gender ,Address ,Phone ,Email ,NationalityCountryID ,ImagePath) VALUES" +
                " (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gender,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath); " +
                " select SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName == "")
            cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", (byte)Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            if (Email == "")
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if(ImagePath == "")
            cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                conn.Open();
                object reader = cmd.ExecuteScalar();
                if (reader != null)
                {
                int.TryParse(reader.ToString(), out PersonID);
                }
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return PersonID;
        }
        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
           DateTime DateOfBirth, sbyte Gender, string Address, string Phone, string Email, byte NationalityCountryID, string ImagePath)
        {
            int RowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "UPDATE [dbo].[People] " +
                "SET [NationalNo] = @NationalNo ," +
                "[FirstName] = @FirstName ," +
                "[SecondName] =@SecondName ," +
                "[ThirdName] = @ThirdName ," +
                "[LastName] = @LastName ," +
                "[DateOfBirth] =  @DateOfBirth ," +
                "[Gender] = @Gender ," +
                "[Address] =@Address ," +
                "[Phone] = @Phone ," +
                "[Email] = @Email ," +
                "[NationalityCountryID] = @NationalityCountryID ," +
                "[ImagePath] = @ImagePath " +
                "WHERE PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName == "")
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", (byte)Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            if (Email == "")
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath == "")
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            try {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { }
            finally { conn.Close(); }
            return RowsAffected > 0;
        }
        public static bool DeletePerson(int PersonID)
        {
            int AffectedRows = 0;
            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);
            string Query = "delete from People where PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@PersonID" , PersonID);
            try {
                conn.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { }
            finally { conn.Close(); }
            return AffectedRows > 0;
        }
    }
} 