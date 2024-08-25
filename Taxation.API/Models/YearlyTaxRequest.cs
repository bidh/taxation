namespace Taxation.API.Models
{
    public class YearlyTaxRequest
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
        /// Tax rate period start date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Tax rate period end date
        /// </summary>
        public string EndDate { get; set; }
    }
}
