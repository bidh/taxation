namespace Taxation.DAL.Services
{
    public interface ITaxService
    {
        public Task<decimal> GetTax(string municipality, DateTimeOffset date);
    }
}
