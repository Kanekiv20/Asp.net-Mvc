using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspAssignmentII.Models
{
    public class SearchPeople
    {
        string ConnectionstringPRATYUSH = ConfigurationManager.ConnectionStrings["LinkPRATYUSH"].ConnectionString;
        public People_Details SearchMethod(string searchItem)
        {

            SqlConnection connection = new SqlConnection(ConnectionstringPRATYUSH);
            SqlCommand command = new SqlCommand("spSearchPerson", connection);
            command.CommandType = CommandType.StoredProcedure;
            int id =0;
            bool status = int.TryParse(searchItem, out id);
            if (status)
            {
                searchItem = "";
            }
            else
            {
                id = -1;
            }
            command.Parameters.AddWithValue("@name", searchItem);
            command.Parameters.AddWithValue("@id", id);
            People_Details user = new People_Details();
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.id = reader.GetInt32(0);
                    user.username = reader.GetString(1);
                    user.name = reader.GetString(2);
                    user.gender = reader.GetString(3);
                    user.address = reader.GetString(4);
                }
            }
            connection.Close();
            return user;
        }
        public DataSet GetName()
        {
            SqlConnection connection = new SqlConnection(ConnectionstringPRATYUSH);
            SqlCommand command = new SqlCommand("spShowPeople", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}