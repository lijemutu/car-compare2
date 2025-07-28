using CarCompare.Models;
using CarCompare.Services;
using CarCompare.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCompare.Controllers
{
    public class CompareController : Controller
    {
        private readonly CarRepository _carRepository;

        public CompareController(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCompare(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car != null)
            {
                var compareModel = HttpContext.Session.Get<CompareModel>("Compare") ?? new CompareModel();
                compareModel.Cars.Add(car);
                HttpContext.Session.Set("Compare", compareModel);
            }
            return PartialView("_Compare", HttpContext.Session.Get<CompareModel>("Compare"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var compareModel = HttpContext.Session.Get<CompareModel>("Compare");
            return View(compareModel);
        }
    }
}
