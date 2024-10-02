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

namespace DataAccessDVLD
{
    public class clsPeopleData
    {
        public static bool Find(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref sbyte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsSettingsData.Connection);

            string Query = "Select * from People where PersonID = @PersonID ";

            SqlCommand cmd = new SqlCommand(Query, conn);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (sbyte)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalityCountryID = (byte)reader["NationalityCountryID"];
                    ImagePath = (string)reader["ImagePath"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
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
    }
}