using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessDVLD  
{
    internal class clsPeople
    {
        public static bool IsFound(int PersonID, ref string NationalNo, ref string FirstName ,ref string SecondName, ref string ThirdName, ref string LastName, 
            ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref byte NationalityCountryID, ref string ImagePath )
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsSettings.Connection);

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
                    Gender = (byte)reader["Gender"];
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
            finally {
            conn.Close();
            }
            return IsFound;
        }
    }   
}
