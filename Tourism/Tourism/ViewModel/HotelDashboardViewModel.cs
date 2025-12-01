using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class HotelDashboardViewModel
    {
        public int HotelId { get; set; }
        public string? Name { get; set; } = "";

       
        public bool IsVerified { get; set; }
        public string VerificationStatusText { get; set; } = "قيد المراجعة";

     
        public int TotalRooms { get; set; }

     
        public List<int>? TotalBookingsLastMonth { get; set; } = new();

        
        public List<double>? MonthlyRevenue { get; set; } = new();

        
        public List<double>? AnnualRevenue { get; set; } = new();

     
        public List<string> Messages { get; set; } = new();


        public bool HasPendingRequest { get; set; }=false;
    }


}
