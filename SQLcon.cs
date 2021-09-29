using System;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Ticketing_System
{
    public class SQLcon
    {
     
        public  String MakeRequest (String comm)
        {
            MySqlConnection conn;
            string myConnectionString = "server=127.0.0.1;port=3306;uid=root;pwd=pass;database=ticketing_sys;SSL Mode=None";
            conn = new MySqlConnection(myConnectionString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = comm;
            String result = comm+" | ";
            try
            {
               conn.Open();
               MySqlDataReader read = command.ExecuteReader();
               var dataTable = new DataTable();
               dataTable.Load(read);
               
               string JSONString = string.Empty;
               JSONString = JsonConvert.SerializeObject(dataTable);
               result= JSONString;
               conn.Close();
               return result;
            }
            catch (MySqlException ex)
            {
                return "Username is taken.";
            }

        }
    }
}