using System;

namespace CarCollectionApp.Models
{
    public class ReviewModel
    {
        public string Name { get; set; } = string.Empty;

        public string ReviewText { get; set; } = string.Empty;

        public int Rating { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public int HelpfulCount { get; set; } = 0;
    }
}
