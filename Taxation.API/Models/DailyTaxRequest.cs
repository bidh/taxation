namespace Taxation.API.Models
{
    public class DailyTaxRequest
    {
        public int MunicipalityId { get; set; }
        public float Tax { get; set; }
        public string Date { get; set; }
    }
}
