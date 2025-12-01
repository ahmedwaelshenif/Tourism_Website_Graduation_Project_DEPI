using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class CreditCardViewModel
    {
     
        public string CardNumber { get; set; } // 👈 تم التعديل

       
        public string CVV { get; set; } // 👈 تم التعديل

       
        public string ExpiryDate { get; set; } // 👈 تم التعديل
        public string CardHolder { get; set; }
    }
}