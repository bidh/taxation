using Taxation.DAL.Services;

namespace Taxation.API.Managers
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxService _taxService;
        public TaxManager(ITaxService taxService)
        {
            _taxService = taxService;
        }
        public async Task<decimal> GetTax(string municipality, DateTimeOffset date)
        {
            return await _taxService.GetTax(municipality, date);
        }
    }
}
