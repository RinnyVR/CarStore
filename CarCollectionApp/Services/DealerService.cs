using System.Collections.Generic;
using System.Linq;
using CarCollectionApp.Models;

namespace CarCollectionApp.Services
{
    public class DealerService : IDealerService
    {
        private readonly CarCollectionContext _context;

        public DealerService(CarCollectionContext context)
        {
            _context = context;
        }

        public List<Dealer> GetAllDealers()
        {
            return _context.Dealers.ToList();
        }

        public Dealer? GetDealerById(int id)
        {
            return _context.Dealers.FirstOrDefault(d => d.Id == id);
        }

        public void AddDealer(Dealer dealer)
        {
            _context.Dealers.Add(dealer);
            _context.SaveChanges();
        }

        public void UpdateDealer(Dealer dealer)
        {
            _context.Dealers.Update(dealer);
            _context.SaveChanges();
        }

        public void DeleteDealer(int id)
        {
            var dealer = GetDealerById(id);
            if (dealer != null)
            {
                _context.Dealers.Remove(dealer);
                _context.SaveChanges();
            }
        }
    }
}
