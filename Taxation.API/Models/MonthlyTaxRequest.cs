namespace Taxation.API.Models
{
    public class MonthlyTaxRequest
    {
        public int MunicipalityId { get; set; }
        public float Tax { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
