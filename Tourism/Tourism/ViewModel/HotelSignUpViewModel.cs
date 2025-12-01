using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tourism.ViewModel
{
    public class HotelSignUpViewModel
    {
        [Required(ErrorMessage = "Hotel name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // 🚨 تصحيح: يجب أن يكون مطلوباً للمقارنة
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hotline is required")]
        [Phone]
        public string Hotline { get; set; }

        [Required(ErrorMessage = "Hotel picture is required")]
        [Display(Name = "Hotel Picture")]
        public IFormFile PicFile { get; set; }

        [Display(Name = "Verification Document (PDF)")]
        public IFormFile VerificationDocumentFile { get; set; }

        [Required(ErrorMessage = "Credit Card details are required")] // تم تعديل الرسالة
        public CreditCardViewModel CreditCard { get; set; } = new CreditCardViewModel();

        // ✅ هذا هو الحقل الذي يحمل القيمة المختارة، وهو مطلوب
        [Required(ErrorMessage = "Governorate selection is required.")]
        [Display(Name = "Governorate")]
        public string Governorate { get; set; }

        
        public IEnumerable<SelectListItem> GovernorateList { get; set; }= new List<SelectListItem>();

        [ValidateNever]
        public string OtherGovernorate { get; set; }
    }


}
