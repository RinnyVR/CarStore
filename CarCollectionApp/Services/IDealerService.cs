using System.Collections.Generic;
using CarCollectionApp.Models;

namespace CarCollectionApp.Services
{
    public interface IDealerService
    {
        List<Dealer> GetAllDealers();
        Dealer? GetDealerById(int id);
        void AddDealer(Dealer dealer);
        void UpdateDealer(Dealer dealer);
        void DeleteDealer(int id);
    }
}
