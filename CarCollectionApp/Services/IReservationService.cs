using System.Collections.Generic;
using CarCollectionApp.Models;

namespace CarCollectionApp.Services
{
    public interface IReservationService
    {
        List<ReservationModel> GetAllReservations();
        void AddReservation(ReservationModel reservation);
        void SaveReservations(List<ReservationModel> reservations); // ADD THIS LINE
    }
}
