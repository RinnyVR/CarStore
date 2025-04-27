namespace CarCollectionApp.Models
{
    public class ReservationModel
    {
        public string CarName { get; set; }
        public string Location { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public decimal FinalPrice { get; set; }
        public string DiscountsApplied { get; set; }
        public DateTime Timestamp { get; set; }

        // NEW FIELDS
        public int RentalDays => (DropoffDate - PickupDate).Days;
        public string? UserNotes { get; set; }
    }
}
