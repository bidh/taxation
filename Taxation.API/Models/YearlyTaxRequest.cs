namespace Taxation.API.Models
{
    public class YearlyTaxRequest
    {
        public int Municipality { get; set; }
        public decimal Tax { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
