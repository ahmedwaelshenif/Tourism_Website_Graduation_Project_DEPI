using Tourism.Models;

namespace Tourism.IRepository
{
    public interface ICreditCardRepository
    {
        public CreditCard GetCreditCard(string cardNumber, string cvv, string expiryDate);
    }
}
