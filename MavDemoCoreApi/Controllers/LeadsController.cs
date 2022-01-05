using MavDemoCoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace MavDemoCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LeadsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

    public JsonResult Get()
        {
            string query = @"select * from leads";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using(MySqlCommand myCommand = new MySqlCommand(query, myconn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Lead lead)
        {
            string query = @"INSERT INTO leads (fname, lname, email) VALUES (@fname, @lname, @email);";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myconn))
                {
                    myCommand.Parameters.AddWithValue("@fname", lead.fname);
                    myCommand.Parameters.AddWithValue("@lname", lead.lname);
                    myCommand.Parameters.AddWithValue("@email", lead.email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }
            }
            return new JsonResult("Added Successfully.");
        }
        [HttpPut]
        public JsonResult Put(Lead lead)
        {
            string query = @"UPDATE leads SET 
                                fname = @fname,
                                lname = @lname,
                                email = @email
                            WHERE id = @id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myconn))
                {
                    myCommand.Parameters.AddWithValue("@id", lead.id);
                    myCommand.Parameters.AddWithValue("@fname", lead.fname);
                    myCommand.Parameters.AddWithValue("@lname", lead.lname);
                    myCommand.Parameters.AddWithValue("@email", lead.email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }
            }
            return new JsonResult("Updated Successfully.");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM leads WHERE id = @id;";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myconn))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }
            }
            return new JsonResult("Deleted Successfully.");
        }
    }
}
