using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspAssignmentII.Models
{
    public class InsertPeople
    {
        public string InsertMethod(People_Details people)
        {
            string ConnectionstringPRATYUSH = ConfigurationManager.ConnectionStrings["LinkPRATYUSH"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionstringPRATYUSH);
            SqlCommand command = new SqlCommand("spInsertPeople", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", people.username);
            command.Parameters.AddWithValue("@name", people.name);
            command.Parameters.AddWithValue("@gender", people.gender);
            command.Parameters.AddWithValue("@address", people.address);
            SqlParameter outputparamater = new SqlParameter()
            {
                ParameterName = "@result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(outputparamater);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                int result = Convert.ToInt32(outputparamater.Value);
                string message;
                if (result == 1)
                {
                    message = "Inserted Sucessfully";
                }
                else
                {
                    message = "User already exists";
                }
                connection.Close();
                return message;
            }
            catch (Exception ex)
            {
                connection.Close();
                return ex.Message;
            }
        }
    }
}