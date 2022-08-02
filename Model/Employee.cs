using System;
using System.Text.Json.Serialization;

namespace MyApi.Models
{
  class Employee
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    [JsonPropertyName("input_date")]
    public DateTime Waktu { get; set; }
  }
}