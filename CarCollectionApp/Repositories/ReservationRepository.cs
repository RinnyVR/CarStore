using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CarCollectionApp.Models;

namespace CarCollectionApp.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _filePath = "Data/reservations.json";

        public List<ReservationModel> GetAllReservations()
        {
            if (!File.Exists(_filePath))
            {
                return new List<ReservationModel>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<ReservationModel>>(json) ?? new List<ReservationModel>();
        }

        public void AddReservation(ReservationModel reservation)
        {
            var reservations = GetAllReservations();
            reservations.Add(reservation);
            SaveReservations(reservations);
        }

        public void SaveReservations(List<ReservationModel> reservations)
        {
            var json = JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
