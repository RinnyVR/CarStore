using System.Collections.Generic;
using CarCollectionApp.Models;
using CarCollectionApp.Repositories;

namespace CarCollectionApp.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public List<ReservationModel> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }

        public void AddReservation(ReservationModel reservation)
        {
            _reservationRepository.AddReservation(reservation);
        }

        public void SaveReservations(List<ReservationModel> reservations) // ADD THIS METHOD
        {
            _reservationRepository.SaveReservations(reservations);
        }
    }
}
