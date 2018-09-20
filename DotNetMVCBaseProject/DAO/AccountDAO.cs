using DotNetMVCBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DotNetMVCBaseProject.DAO
{
    public class AccountDAO : DBContext<Account>
    {
        public List<Account> GetAll()
        {
            List<Account> userAccounts = new List<Account>();
            try
            {
                string strCommand = "SELECT [ID],[Username],[Passsword],[Name],[DateOfBirth],[Gender],[Roles] FROM [Account]";
                SqlCommand command = new SqlCommand(strCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"]);
                    string username = reader["Username"].ToString();
                    string password = reader["Passsword"].ToString();
                    string name = reader["Name"].ToString();
                    DateTime dob = Convert.ToDateTime(reader["DateOfBirth"]);
                    bool gender = Convert.ToBoolean(reader["Gender"]);
                    Common.Enums.AccountRole role = (Common.Enums.AccountRole)Enum.Parse(typeof(Common.Enums.AccountRole), reader["Roles"].ToString());
                    userAccounts.Add(new Account()
                    {
                        ID = ID,
                        Username = username,
                        Password = password,
                        Name = name,
                        BirthDay = dob,
                        Gender = gender,
                        Role = role
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAll() function of AccountDAO :" + ex.Message);
                throw ex;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            return userAccounts;
        }
        public Account GetOne(string username, string password)
        {
            List<Account> userAccounts = new List<Account>();
            try
            {
                string strCommand = "select * from Account where Username = '" + username + "' and Passsword='" + password + "'";
                SqlCommand command = new SqlCommand(strCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"]);
                    string name = reader["Name"].ToString();
                    DateTime dob = Convert.ToDateTime(reader["DateOfBirth"]);
                    bool gender = Convert.ToBoolean(reader["Gender"]);
                    Common.Enums.AccountRole role = (Common.Enums.AccountRole)Enum.Parse(typeof(Common.Enums.AccountRole), reader["Roles"].ToString());
                    userAccounts.Add(new Account()
                    {
                        ID = ID,
                        Username = username,
                        Password = password,
                        Name = name,
                        BirthDay = dob,
                        Gender = gender,
                        Role = role
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetOne() function of AccountDAO :" + ex.Message);
                throw(ex);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            if (userAccounts.Count != 1)
            {
                return null;
            }
            return userAccounts[0];
        }
        public Account GetOne(string username)
        {
            List<Account> userAccounts = new List<Account>();
            try
            {
                string strCommand = "select * from Account where Username = '" + username + "'";
                SqlCommand command = new SqlCommand(strCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"]);
                    string password = reader["Passsword"].ToString();
                    string name = reader["Name"].ToString();
                    DateTime dob = Convert.ToDateTime(reader["DateOfBirth"]);
                    bool gender = Convert.ToBoolean(reader["Gender"]);
                    Common.Enums.AccountRole role = (Common.Enums.AccountRole)Enum.Parse(typeof(Common.Enums.AccountRole), reader["Roles"].ToString());
                    userAccounts.Add(new Account()
                    {
                        ID = ID,
                        Username = username,
                        Password = password,
                        Name = name,
                        BirthDay = dob,
                        Gender = gender,
                        Role = role
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetOne() function of AccountDAO :" + ex.Message);
                throw ex;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            if (userAccounts.Count != 1)
            {
                return null;
            }
            return userAccounts[0];
        }
        public int Insert(Account newAccount)
        {
            int i = 0;

            if (GetOne(newAccount.Username) != null)
                return i;

            try
            {
                int gender = 1;
                if (newAccount.Gender != true)
                    gender = 0;
                string dob = newAccount.BirthDay.Year + "/" + newAccount.BirthDay.Month + "/" + newAccount.BirthDay.Day;
                var strCommand = "Insert into Account values('" + newAccount.Username + "','" + newAccount.Password + "','" +
                    newAccount.Name + "','" + dob + "','" + gender + "','" + newAccount.Role.ToString() + "')";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = strCommand;
                cmd.Connection = connection;
                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Insert() function of AccountDAO :" + ex.Message);
                throw ex;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            return i;
        }
    }
}