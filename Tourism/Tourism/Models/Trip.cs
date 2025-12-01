using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tourism.Models;
using Tourism.Models.Relations;

namespace Tourism.Models
{
    public class Trip
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Trip name is required")]
        [StringLength(100, ErrorMessage = "Trip name cannot exceed 100 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string description { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters")]
        public string destination { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public double cost { get; set; }

        public bool status { get; set; } = true;

        

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        [Required(ErrorMessage = "Tour guide is required")]
        [ForeignKey(nameof(tourGuide))]
        public int tourGuideId { get; set; }
        public TourGuide tourGuide { get; set; }
        public bool accepted { get; set; } = false;

        [Required]
        public DateTime dateAdded { get; set; }
    }
}
