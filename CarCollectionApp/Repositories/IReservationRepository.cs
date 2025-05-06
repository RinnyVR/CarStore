using System.Collections.Generic;
using CarCollectionApp.Models;

namespace CarCollectionApp.Repositories
{
    public interface IReservationRepository
    {
        List<ReservationModel> GetAllReservations();
        void AddReservation(ReservationModel reservation);
        void SaveReservations(List<ReservationModel> reservations);
    }
}
