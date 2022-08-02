using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace MyApi.Controllers
{
  [ApiController]
  [Route("")]
  public class LatihanController : ControllerBase
  {
    private readonly ILogger<LatihanController> _logger;
    private readonly IOptions<AppSetting> _option;
    public LatihanController(ILogger<LatihanController> logger, IOptions<AppSetting> option)
    {
      _logger = logger;
      _option = option;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    [HttpGet("baca")]
    public ActionResult baca()
    {
      string CS = _option.Value.DefaultConnection;

      List<Employee> employees = new List<Employee>();
      using (SqlConnection con = new SqlConnection(CS))
      {
        SqlCommand cmd = new SqlCommand("SELECT * FROM Employees;", con);
        cmd.CommandType = CommandType.Text;
        con.Open();

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            Employee employee = new Employee();
            employee.Id = reader["ID"].ToString();
            employee.Name = reader["Name"].ToString();
            employee.Location = reader["Location"].ToString();
            employee.Waktu = Convert.ToDateTime(reader["Waktu"]);

            employees.Add(employee);
          }
          reader.Close();
        }
        con.Close();
        con.Dispose();
      }
      return new OkObjectResult(new { stat_code = 200, stat_msg = "Sukses", data = employees });
    }

    [HttpGet("bacasatu/{id}")]
    public ActionResult bacasatu(string id)
    {
      string CS = _option.Value.DefaultConnection;

      Employee employee = new Employee();
      using (SqlConnection con = new SqlConnection(CS))
      {
        SqlCommand cmd = new SqlCommand($"SELECT * FROM Employees where ID={id};", con);
        cmd.CommandType = CommandType.Text;
        con.Open();

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          if (reader.Read())
          {
            employee.Id = reader["ID"].ToString();
            employee.Name = reader["Name"].ToString();
            employee.Location = reader["Location"].ToString();
            employee.Waktu = Convert.ToDateTime(reader["Waktu"]);

          }
          reader.Close();
        }
        con.Close();
        con.Dispose();
      }
      return new OkObjectResult(new { stat_code = 200, stat_msg = "Sukses", data = employee });
    }
  }
}