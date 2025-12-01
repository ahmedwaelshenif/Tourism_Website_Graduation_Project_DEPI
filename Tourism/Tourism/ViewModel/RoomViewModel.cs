using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class RoomViewModel
    {
        public int id { get; set; }
        public int hotelid  { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        [Display(Name = "Room Number")]
        public string number { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price per Night")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Please specify room capacity")]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int capacity { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(500, ErrorMessage = "Description must be less than 500 characters")]
        public string description { get; set; }

        [Display(Name = "Has Offer?")]
        public bool offer { get; set; }

        [Display(Name = "Discount Percentage")]
        [Range(0, 100, ErrorMessage = "Offer must be between 0 and 100")]
        public int? discount { get; set; }

        [Display(Name = "Available Rooms")]
        [Range(1, 100, ErrorMessage = "Count must be between 1 and 100")]
        public int count { get; set; }

        [Display(Name = "Upload Images")]
        public List<IFormFile> UploadedImages { get; set; } = new List<IFormFile>();

        public bool accepted { get; set; } = false;
        public List<RoomAddViewModel> Rooms { get; set; } = new List<RoomAddViewModel>();

        public string? Message { get; set; }
        public List<int> ImageIds { get; set; } = new List<int>();
    }
}
