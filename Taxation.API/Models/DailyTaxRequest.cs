﻿namespace Taxation.API.Models
{
    public class DailyTaxRequest
    {
        public int MunicipalityId { get; set; }
        public decimal Tax { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
