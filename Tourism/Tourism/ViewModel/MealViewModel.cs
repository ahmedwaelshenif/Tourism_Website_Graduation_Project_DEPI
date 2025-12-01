using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class MealViewModel
    {
        [Required]
        [StringLength(20)]
        public string name { get; set; }
        public int? id { get; set; }
        [Required]
        [StringLength(60)]
        public string description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double price { get; set; }
        public int mealId { get; set; }
        public string? RestaurantName { get; set; }
        public bool accepted { get; set; } 
        public int rate { get; set; }
        public string status { get; set; } = "Pending";
        [Required]
        public IFormFile UploadedImage { get; set; } //upload
        public byte[]? ImageBytes { get; set; } //view
        public int RestaurantId { get; set; }
        public DateTime dateAdded { get; set; }
        public byte[]? ExistingImage { get; set; }
    }
}