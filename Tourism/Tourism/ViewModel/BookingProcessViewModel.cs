using System.ComponentModel.DataAnnotations;
using Tourism.Models;

namespace Tourism.ViewModel
{
    public class BookingProcessViewModel : IValidatableObject
    {

        [Required]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Number of Rooms is required.")]
        [Range(1, 10, ErrorMessage = "Number of rooms must be between 1 and 10.")]
        [Display(Name = "Number of Rooms")]
        public int RequiredRoomsCount { get; set; } = 1;

        [Required(ErrorMessage = "Capacity of one room is required.")]
        [Range(1, 10, ErrorMessage = "Maximum number of members is 10.")]
        [Display(Name = "Minimum number of members")]
        public int RequiredCapacity { get; set; } = 2;

        [Required(ErrorMessage = "Start Date is Required.")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "End Date is Required.")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(2);

        public string? HotelName { get; set; }
        public int? SelectedRoomTypeId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int TotalNights { get; set; }
        public string? ErrorMessage { get; set; }
        public List<Room>? AvailableRoomTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CheckOutDate.Date <= CheckInDate.Date)
            {
                yield return new ValidationResult(
                    "End date must be after Start date.",
                    new[] { nameof(CheckOutDate) }
                );
            }

            if (CheckInDate.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Check-in date cannot be in the past.",
                    new[] { nameof(CheckInDate) }
                );
            }
        }
    }
}
