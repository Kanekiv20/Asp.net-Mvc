using Bank1;
using System;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace ServiceBank
{
     public class  BankOperation:IBankOperation
    {
        static string ConnectionstringPRATYUSH = ConfigurationManager.ConnectionStrings["LinkPRATYUSH"].ConnectionString;
        SqlConnection connection = new SqlConnection(ConnectionstringPRATYUSH);
        public decimal Balence(string username)
        {
            SqlCommand command = new SqlCommand("GetBalance", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Username",username);
            connection.Open();
            decimal bal = (decimal)command.ExecuteScalar();
            return bal;
        }
        public string Signup(BankUser user)
        {
            SqlCommand command = new SqlCommand("SpSignUpBank", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@AccNo", user.AccNo);
            command.Parameters.AddWithValue("@Balance", user.Balance);
            SqlParameter outputparamater = new SqlParameter()
            {
                ParameterName = "@MESSAGE",
                Size = 100,
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(outputparamater);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                string message = outputparamater.Value.ToString();
                connection.Close();
                return message;
            }
            catch (Exception ex)
            {
                connection.Close();
                return ex.Message;
            }
        }

        public string Login(BankUser user)
        {
            SqlCommand command = new SqlCommand("SpLoginBank", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            SqlParameter outputparamater = new SqlParameter()
            {
                ParameterName = "@MESSAGE",
                Size = 100,
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(outputparamater);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                string message = outputparamater.Value.ToString();
                connection.Close();
                return message;
            }
            catch (Exception ex)
            {
                connection.Close();
                return ex.Message;
            }
        }

        string IBankOperation.Transfer(string SenderUsername, string ReceiverUsername, decimal Amount)
        {
            SqlCommand command = new SqlCommand("TransferMoney", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SenderUsername", SenderUsername);
            command.Parameters.AddWithValue("@ReceiverUsername", ReceiverUsername);
            command.Parameters.AddWithValue("@Amount", Amount);
            SqlParameter outputparamater = new SqlParameter()
            {
                ParameterName = "@MESSAGE",
                Size = 100,
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(outputparamater);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                string message = outputparamater.Value.ToString();
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
