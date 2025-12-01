using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class RestaurantHomePageViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        public bool verified { get; set; } 
        [Required]
        [StringLength(60)]
        public string description { get; set; }
        public byte[]? Image { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{3,5}$", ErrorMessage = "Please Enter a Valid HotLine(3-5 Numbers)")]
        public string hotline { get; set; }

    }
}
