using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;

namespace Tourism.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        TourismDbContext _Context;
        public CreditCardRepository(TourismDbContext tourismDbContext)
        {
            _Context = tourismDbContext;
        }
        public CreditCard GetCreditCard(string cardNumber, string cvv, string expiryDate)
        {
            return _Context.CreditCards.FirstOrDefault(c =>
             c.CardNumber == cardNumber &&
             c.CVV == cvv &&
             c.ExpiryDate == expiryDate);
        }
    }
}
