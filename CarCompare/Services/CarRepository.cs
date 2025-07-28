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


        public IEnumerable<CarModel> Filter(string make = null, string model = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = _cars.AsQueryable();
            if (!string.IsNullOrEmpty(make))
                query = query.Where(c => c.Make == make);
            if (!string.IsNullOrEmpty(model))
                query = query.Where(c => c.Model == model);
            if (minPrice.HasValue)
                query = query.Where(c => decimal.Parse(c.PriceMxn) >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(c => decimal.Parse(c.PriceMxn) <= maxPrice.Value);
            return query.ToList();
        }
    }
}
