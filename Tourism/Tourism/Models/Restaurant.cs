using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Tourism.Models;
using Tourism.Models.Relations;

namespace Tourism.Models
{
    [Index(nameof(email), IsUnique = true)]
    public class Restaurant
    {
        [Key]
        public int id { get; set; }
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
        public List<Image> Images { get; set; } = new();
        [Required]
        public string address { get; set; }
        public List<Meal> meals { get; set; } = new();
        public List<Table> tables { get; set; } = new();
        public List<InboxMsg> msg { get; set; } = new();

        public byte[]? verificationDocuments { get; set; }

        [Required]
        public CreditCard creditCard { get; set; }
        public bool verified { get; set; } = false;
        [Required]
        [StringLength(60)]
        public string description { get; set; }
        public byte[]? Image { get; set; }

    }
}
