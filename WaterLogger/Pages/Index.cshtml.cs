using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<DrinkingWaterModel> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
       Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM drinking_water ";

            var tableData = new List<DrinkingWaterModel>();
            SqlDataReader reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                string rawDateString = reader.GetString(1);
                string cleanDateString = rawDateString.Replace("?", "");
                DateTime parsedDate = DateTime.Parse(cleanDateString, CultureInfo.CurrentUICulture.DateTimeFormat);

                tableData.Add(
                new DrinkingWaterModel
                {
                    Id = reader.GetInt32(0),
                    Date = parsedDate,
                    Quantity = reader.GetInt32(2)
                }); ;
            }

            return tableData;
        }
    }
}
