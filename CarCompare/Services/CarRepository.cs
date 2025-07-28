using System.Collections.Generic;
using System.Linq;
using CarCompare.Models;

namespace CarCompare.Services
{
    public class CarRepository
    {
        private readonly List<CarModel> _cars;
        public CarRepository(List<CarModel> cars)
        {
            _cars = cars;
        }

        public IEnumerable<CarModel> GetAll() => _cars;

        public Task<CarModel?> GetCarByIdAsync(int id)
        {
            // Assuming CarModel has an 'Id' property, or we can use index as ID for simplicity
            // For now, let's assume the index in the list can serve as a simple ID.
            // In a real application, cars would have unique IDs.
            if (id >= 0 && id < _cars.Count)
            {
                return Task.FromResult<CarModel?>(_cars[id]);
            }
            return Task.FromResult<CarModel?>(null);
        }

        public IEnumerable<CarModel> Filter(string make = null, string model = null, decimal? minPrice = null, decimal? maxPrice = null, string sortBy = null)
        {
            var query = _cars.AsQueryable();

            if (!string.IsNullOrEmpty(make))
                query = query.Where(c => c.Make.Contains(make, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(model))
                query = query.Where(c => c.Model.Contains(model, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                query = query.Where(c => decimal.Parse(c.PriceMxn) >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(c => decimal.Parse(c.PriceMxn) <= maxPrice.Value);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "price":
                        query = query.OrderBy(c => c.PriceMxn);
                        break;
                    case "year":
                        query = query.OrderByDescending(c => c.Year);
                        break;
                    case "fuel_efficiency":
                        query = query.OrderByDescending(c => c.FuelEfficiencyKml.Combined);
                        break;
                }
            }

            return query.ToList();
        }
    }
}
