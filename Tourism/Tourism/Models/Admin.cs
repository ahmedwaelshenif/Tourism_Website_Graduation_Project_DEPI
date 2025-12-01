using System.ComponentModel.DataAnnotations;

namespace Tourism.Models
{
    public class Admin
    {
        [Key]
        public int id { get; set; }


        [EmailAddress]
        [Required]
        public string email { get; set; }

        [Required]
        public string passwordHash { get; set; }

    }
}
