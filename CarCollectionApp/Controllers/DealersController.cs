using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarCollectionApp.Models;
using CarCollectionApp.Services;

namespace CarCollectionApp.Controllers
{
    public class DealersController : Controller
    {
        private readonly IDealerService _dealerService;

        public DealersController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        public IActionResult Index()
        {
            var dealers = _dealerService.GetAllDealers();
            return View(dealers);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = _dealerService.GetDealerById(id.Value);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,City,Country,Email,Phone")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                _dealerService.AddDealer(dealer);
                return RedirectToAction(nameof(Index));
            }
            return View(dealer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = _dealerService.GetDealerById(id.Value);
            if (dealer == null)
            {
                return NotFound();
            }
            return View(dealer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,City,Country,Email,Phone")] Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dealerService.UpdateDealer(dealer);
                }
                catch (Exception)
                {
                    if (_dealerService.GetDealerById(dealer.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dealer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = _dealerService.GetDealerById(id.Value);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _dealerService.DeleteDealer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
