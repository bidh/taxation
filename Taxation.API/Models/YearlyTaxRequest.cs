namespace Taxation.API.Models
{
    public class YearlyTaxRequest
    {
        public int MunicipalityId { get; set; }
        public float Tax { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
