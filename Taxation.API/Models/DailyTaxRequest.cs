namespace Taxation.API.Models
{
    public class DailyTaxRequest
    {
        /// <summary>
        /// Id of the municipality
        /// </summary>
        public int MunicipalityId { get; set; }

        /// <summary>
        /// Tax rate
        /// </summary>
        public float Tax { get; set; }

        /// <summary>
        /// Tax rate date
        /// </summary>
        public string Date { get; set; }
    }
}
