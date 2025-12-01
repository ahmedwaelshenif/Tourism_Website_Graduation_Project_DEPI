using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class RoomAddViewModel
    {
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }

        [Required]
        [Range(1, 10)]
        public int GuestCapacity { get; set; }

        [Required]
        [Range(1.0, 10000.0)]
        public decimal Price { get; set; }

        
        [Display(Name = "Room Picture")]
        [Required(ErrorMessage = "A picture is required")]
        public List<IFormFile> PicFiles { get; set; }


        public bool IsApproved { get; set; } = false;
    }
}
