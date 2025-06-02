using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);

            return Page();
        }

        private DrinkingWaterModel GetById(int id)
        {
            var drinkingWaterRecord = new DrinkingWaterModel();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water WHERE Id = {id}";

                SqlDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    string rawDateString = reader.GetString(1);
                    string cleanDateString = rawDateString.Replace("?", "");
                    DateTime parsedDate = DateTime.Parse(cleanDateString, CultureInfo.CurrentUICulture.DateTimeFormat);

                    drinkingWaterRecord.Id = reader.GetInt32(0);
                    drinkingWaterRecord.Date = parsedDate;
                    drinkingWaterRecord.Quantity = reader.GetInt32(2);
                }

                return drinkingWaterRecord;

            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                   $"UPDATE drinking_water SET date ='{DrinkingWater.Date}', quantity = {DrinkingWater.Quantity} WHERE Id = {DrinkingWater.Id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
