namespace Taxation.API.Models
{
    public class DailyTaxRequest
    {
        public string Municipality { get; set; }
        public decimal Tax { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
