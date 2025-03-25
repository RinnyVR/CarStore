using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarCollectionApp.Models;

namespace CarCollectionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Reviews = GetCustomerReviews();
            return View();
        }

        public IActionResult Cars()
        {
            return View();
        }

        public IActionResult CarDetails(string? car)
        {
            var cars = GetCarCollection();

            if (!string.IsNullOrEmpty(car) && cars.ContainsKey(car))
            {
                ViewBag.Car = cars[car];
                return View();
            }

            return RedirectToAction("Cars");
        }

        public IActionResult Compare()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Reviews()
        {
            ViewBag.Reviews = GetCustomerReviews();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Dictionary<string, CarModel> GetCarCollection()
        {
            return new Dictionary<string, CarModel>
            {
                { "bmw-m4", new CarModel("BMW M4", "3.0L Twin-Turbo Inline-6", "503 HP", "479 lb-ft", "180 mph", "bmw-m4.jpg") },
                { "audi-r8", new CarModel("Audi R8", "5.2L V10", "562 HP", "406 lb-ft", "205 mph", "audi-r8.jpg") },
                { "corvette-c8", new CarModel("Corvette C8", "6.2L V8", "495 HP", "470 lb-ft", "194 mph", "corvette-c8.jpg") },
                { "ferrari-f8", new CarModel("Ferrari F8", "3.9L Twin-Turbo V8", "710 HP", "568 lb-ft", "211 mph", "ferrari-f8.jpg") },
                { "lamborghini-huracan", new CarModel("Lamborghini Huracan", "5.2L V10", "631 HP", "443 lb-ft", "202 mph", "lamborghini-huracan.jpg") },
                { "mclaren-720s", new CarModel("McLaren 720S", "4.0L Twin-Turbo V8", "710 HP", "568 lb-ft", "212 mph", "mclaren-720s.jpg") },
                { "mercedes-amg-gt", new CarModel("Mercedes-AMG GT", "4.0L V8 Biturbo", "523 HP", "494 lb-ft", "194 mph", "mercedes-amg-gt.jpg") },
                { "mustang-gt", new CarModel("Ford Mustang GT", "5.0L V8", "450 HP", "410 lb-ft", "155 mph", "mustang-gt.jpg") },
                { "porsche-911", new CarModel("Porsche 911", "3.0L Twin-Turbo Flat-6", "443 HP", "390 lb-ft", "191 mph", "porsche-911.jpg") },
                { "tesla-model-s", new CarModel("Tesla Model S", "Dual Motor Electric", "1020 HP", "1050 lb-ft", "200 mph", "tesla-model-s.jpg") }
            };
        }

        private List<ReviewModel> GetCustomerReviews()
        {
            return new List<ReviewModel>
    {
        new ReviewModel { Name = "John Smith", ReviewText = "The best experience for buying luxury cars!" },
        new ReviewModel { Name = "Emily Johnson", ReviewText = "The website is incredibly easy to use." },
        new ReviewModel { Name = "Michael Brown", ReviewText = "The car collection is simply outstanding!" },
        new ReviewModel { Name = "Sarah Williams", ReviewText = "Fast and reliable service." }
    };
        }

    }
}
