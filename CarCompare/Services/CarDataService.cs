using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CarCompare.Models;

namespace CarCompare.Services
{
    public class CarDataService
    {
        private readonly string _jsonPath;
        public CarDataService(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public async Task<List<CarModel>> LoadCarsAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_jsonPath);
                return JsonSerializer.Deserialize<List<CarModel>>(json);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error loading car data: {ex.Message}");
                return new List<CarModel>();
            }
        }
    }
}
