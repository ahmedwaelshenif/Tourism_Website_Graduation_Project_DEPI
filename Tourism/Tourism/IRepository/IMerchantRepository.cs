using Microsoft.EntityFrameworkCore;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.IRepository
{
    public interface IMerchantRepository
    {
        public Merchant GetByEmail(string email);


        public void UpdateVerificationDocument(Merchant m, byte[] pdfBytes);


        public List<string> GetMessages(int id);

 

        public ServiceRequest GetServiceRequest(int productId);

        public List<ProductOrdersViewModel> GetOrders(int merchantId);

        public void PrepareDashboard(MerchantDashboardViewModel dashboardModel, int id);


    }
}
