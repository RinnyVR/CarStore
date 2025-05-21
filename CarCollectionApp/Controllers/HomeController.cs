using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarCollectionApp.Models;
using CarCollectionApp.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace CarCollectionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarStatsService _carStatsService;
        private readonly IReservationService _reservationService;

        private static readonly List<ReviewModel> _reviews = new List<ReviewModel>
        {
            new ReviewModel { Name = "John Smith", ReviewText = "The best experience for buying luxury cars!", Rating = 5, Timestamp = DateTime.Now.AddDays(-3), HelpfulCount = 12 },
            new ReviewModel { Name = "Emily Johnson", ReviewText = "The website is incredibly easy to use.", Rating = 4, Timestamp = DateTime.Now.AddDays(-5), HelpfulCount = 9 },
            new ReviewModel { Name = "Michael Brown", ReviewText = "The car collection is simply outstanding!", Rating = 5, Timestamp = DateTime.Now.AddDays(-10), HelpfulCount = 14 },
            new ReviewModel { Name = "Sarah Williams", ReviewText = "Fast and reliable service.", Rating = 4, Timestamp = DateTime.Now.AddDays(-2), HelpfulCount = 7 },
            new ReviewModel { Name = "David Lee", ReviewText = "Excellent variety of high-performance vehicles!", Rating = 5, Timestamp = DateTime.Now.AddDays(-1), HelpfulCount = 5 },
            new ReviewModel { Name = "Sophia Chen", ReviewText = "Very user-friendly and sleek design.", Rating = 4, Timestamp = DateTime.Now.AddDays(-6), HelpfulCount = 8 }
        };

        public HomeController(ILogger<HomeController> logger, ICarStatsService carStatsService, IReservationService reservationService)
        {
            _logger = logger;
            _carStatsService = carStatsService;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            ViewBag.Reviews = _reviews;
            return View();
        }

        public IActionResult Cars() => View();

        public IActionResult RentPurchase()
        {
            var cars = GetCarCollection();
            var pricing = new Dictionary<string, (int RentalPerDay, int PurchasePrice)>
            {
                { "BMW M4", (150, 85000) }, { "Audi R8", (200, 150000) }, { "Corvette C8", (170, 67000) },
                { "Ferrari F8", (300, 276000) }, { "Lamborghini Huracan", (290, 261000) }, { "McLaren 720S", (310, 299000) },
                { "Mercedes-AMG GT", (220, 130000) }, { "Ford Mustang GT", (120, 55000) }, { "Porsche 911", (180, 110000) },
                { "Tesla Model S", (200, 104000) }
            };
            ViewBag.Cars = cars;
            ViewBag.Pricing = pricing;
            return View();
        }

        [HttpGet]
        public IActionResult Reserve(string carName)
        {
            ViewBag.CarName = carName;
            return View();
        }

        [HttpPost]
        public IActionResult Reserve(string carName, string location, DateTime pickup, DateTime dropoff, string userNotes)
        {
            _logger.LogInformation($"Reservation made: {carName}, {location}, from {pickup:MM/dd/yyyy} to {dropoff:MM/dd/yyyy}");

            int days = (dropoff - pickup).Days;
            if (days <= 0)
            {
                ViewBag.Message = "Invalid date range. Please ensure drop-off is after pick-up.";
                ViewBag.CarName = carName;
                return View();
            }

            int dailyRate = GetRentalPrice(carName);
            decimal total = days * dailyRate;
            List<string> discountReasons = new();
            decimal discount = 0;

            if (pickup.Month == 12)
            {
                discount += 0.15m;
                discountReasons.Add("15% December Discount");
            }

            if (days >= 7)
            {
                discount += 0.10m;
                discountReasons.Add("10% Long-Term Rental Discount");
            }

            decimal finalPrice = total * (1 - discount);

            var reservation = new ReservationModel
            {
                CarName = carName,
                Location = location,
                PickupDate = pickup,
                DropoffDate = dropoff,
                FinalPrice = finalPrice,
                DiscountsApplied = discountReasons.Count > 0 ? string.Join(" + ", discountReasons) : "None",
                UserNotes = userNotes,
                Timestamp = DateTime.Now
            };

            var reservations = _reservationService.GetAllReservations();
            reservations.Add(reservation);
            _reservationService.AddReservation(reservation);

            ViewBag.Message = $"Reservation confirmed for {carName} from {pickup:MMM d} to {dropoff:MMM d} at {location}.";
            ViewBag.CarName = carName;
            ViewBag.FinalPrice = finalPrice;
            ViewBag.DiscountReasons = reservation.DiscountsApplied;

            return View();
        }

        [HttpPost]
        public IActionResult EditReservation(long timestamp, string location, DateTime pickup, DateTime dropoff, string userNotes)
        {
            var reservations = _reservationService.GetAllReservations();
            var res = reservations.FirstOrDefault(r => r.Timestamp.Ticks == timestamp);
            if (res == null) return RedirectToAction("Reservations");

            res.Location = location;
            res.PickupDate = pickup;
            res.DropoffDate = dropoff;
            res.UserNotes = userNotes;

            int days = (dropoff - pickup).Days;
            decimal rate = GetRentalPrice(res.CarName);
            decimal total = days * rate;
            decimal discount = 0;
            List<string> discountNotes = new();

            if (pickup.Month == 12)
            {
                discount += 0.15m;
                discountNotes.Add("15% December Discount");
            }
            if (days >= 7)
            {
                discount += 0.10m;
                discountNotes.Add("10% Long-Term Rental");
            }

            res.FinalPrice = total * (1 - discount);
            res.DiscountsApplied = discountNotes.Any() ? string.Join(" + ", discountNotes) : "None";

            _reservationService.SaveReservations(reservations);
            TempData["Toast"] = "Reservation updated successfully!";
            return RedirectToAction("Reservations");
        }

        [HttpPost]
        public IActionResult CancelReservation(long timestamp)
        {
            var reservations = _reservationService.GetAllReservations();
            var target = reservations.FirstOrDefault(r => r.Timestamp.Ticks == timestamp);
            if (target != null)
            {
                reservations.Remove(target);
                _reservationService.SaveReservations(reservations);
            }

            TempData["Toast"] = "Reservation canceled.";
            return RedirectToAction("Reservations");
        }

        public IActionResult Reservations(string? search, string? sort)
        {
            var reservations = _reservationService.GetAllReservations();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                reservations = reservations
                    .Where(r => r.CarName.ToLower().Contains(search) || r.Location.ToLower().Contains(search))
                    .ToList();
            }

            reservations = sort switch
            {
                "pickup" => reservations.OrderBy(r => r.PickupDate).ToList(),
                "price" => reservations.OrderByDescending(r => r.FinalPrice).ToList(),
                _ => reservations.OrderByDescending(r => r.Timestamp).ToList()
            };

            ViewBag.Sort = sort;
            ViewBag.Search = search;
            return View(reservations);
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
            var cars = GetCarCollection();
            ViewBag.CarCollection = cars;

            var ratings = cars.ToDictionary(c => c.Key, c =>
            {
                var hp = int.Parse(c.Value.HP.Split(' ')[0]);
                var price = GetEstimatedPrice(c.Key);
                return _carStatsService.EvaluatePerformance(hp, price);
            });

            ViewBag.PerformanceRatings = ratings;
            return View();
        }

        public IActionResult About() => View();
        public IActionResult Contact() => View();
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reviews() => View(_reviews);
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SubmitReview(string name, string reviewText, int rating)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(reviewText) && rating > 0)
            {
                _reviews.Add(new ReviewModel
                {
                    Name = name,
                    ReviewText = reviewText,
                    Rating = rating,
                    Timestamp = DateTime.Now,
                    HelpfulCount = 0
                });
            }
            return RedirectToAction("Reviews");
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

        private decimal GetEstimatedPrice(string carKey)
        {
            return carKey switch
            {
                "bmw-m4" => 85000,
                "audi-r8" => 145000,
                "corvette-c8" => 67000,
                "ferrari-f8" => 276000,
                "lamborghini-huracan" => 261000,
                "mclaren-720s" => 299000,
                "mercedes-amg-gt" => 118000,
                "mustang-gt" => 55000,
                "porsche-911" => 110000,
                "tesla-model-s" => 104000,
                _ => 100000
            };
        }

        private int GetRentalPrice(string carName)
        {
            return carName switch
            {
                "BMW M4" => 150,
                "Audi R8" => 200,
                "Corvette C8" => 170,
                "Ferrari F8" => 300,
                "Lamborghini Huracan" => 290,
                "McLaren 720S" => 310,
                "Mercedes-AMG GT" => 220,
                "Ford Mustang GT" => 120,
                "Porsche 911" => 180,
                "Tesla Model S" => 200,
                _ => 150
            };
        }
    }
}
