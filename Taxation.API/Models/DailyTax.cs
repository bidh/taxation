namespace Taxation.API.Models
{
    public class DailyTax
    {
        public string Municipality { get; set; }
        public decimal Tax { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
