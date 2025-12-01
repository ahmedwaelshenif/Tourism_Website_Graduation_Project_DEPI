using System.ComponentModel.DataAnnotations;
using Tourism.Models.Relations;
using Tourism.Models;

namespace Tourism.ViewModel
{
    public class MerchantDashboardViewModel
    {

        public string? name { get; set; } = "";
        public List<int>? units { get; set; } = new();
        public List<double>? total { get; set; } = new();
        public List<double>? annualEarnings { get; set; } = new();
        public List<string> msg { get; set; } = new();

        public int rate { get; set; }
        public List<List<Review>>? reviews=new();


    }
}
