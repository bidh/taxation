namespace Taxation.API.Models
{
    public class MonthlyTaxRequest
    {
        public int Municipality { get; set; }
        public float Tax { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
