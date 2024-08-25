namespace Taxation.API.Managers
{
    public interface ITaxManager
    {
        public Task<decimal> GetTax(string municipality, DateTimeOffset date);
    }
}
