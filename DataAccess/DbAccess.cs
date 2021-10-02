﻿using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ticketing_System.DataAccess
{
    public class DbAccess
    {
        private readonly IConfiguration _configuration;
        public DbAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public Boolean Post(string QueryStr)
        {
            Boolean cond;
            JsonResult json = MakeRequest(QueryStr);
            JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
            try
            {
                Array thing = o["Value"].ToObject<String[]>();
                cond = true;
            }
            catch (Exception e)
            {
                cond = false;
            }
            return cond;
        }
        [HttpGet]
        public JArray Get(string QueryStr)
        {
            try
            {
                JsonResult json = MakeRequest(QueryStr);
                JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
                var result = (o["Value"].ToString());
                JArray Jar = JArray.Parse(result);
                return Jar;
            }
            catch (Exception e)
            {
                return new JArray(e.ToString());

            }
        }
        public JsonResult MakeRequest(string comm)
        {
            MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("default"));
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = comm;
            string result = "Query Text: " + comm + "\nConnection String: " + conn.ConnectionString + "\nException: ";
            try
            {
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                reader.Close();
                conn.Close();
                return new JsonResult(dataTable);
            }
            catch (MySqlException ex)
            {
                result += ex;
                return new JsonResult(result);
            }

        }
    }
}