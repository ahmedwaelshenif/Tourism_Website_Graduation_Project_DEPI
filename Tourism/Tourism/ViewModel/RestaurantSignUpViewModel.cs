using System.ComponentModel.DataAnnotations;
using Tourism.Models;

namespace Tourism.ViewModel
{
    public class RestaurantSignUpViewModel
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [EmailAddress]
        [Required]
        public string email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string passwordHash { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{3,5}$", ErrorMessage = "Please Enter a Valid HotLine(3-5 Numbers)")]
        public string hotline { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        [StringLength(60)]
        public string description { get; set; }
        [Required]
        public CreditCard creditCard { get; set; }
        [Required]
        public IFormFile UploadedImage { get; set; } //upload
        public byte[]? ImageBytes { get; set; } //view
    }
}
