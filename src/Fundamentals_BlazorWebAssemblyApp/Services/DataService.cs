using Fundamentals_BlazorWebAssemblyApp.Interfaces;

namespace Fundamentals_BlazorWebAssemblyApp.Services
{
    public class DataService : IDataService
    {
        public DataService(HttpClient http)
        {
        }

        public async Task<IEnumerable<string>> GetCustomers()
        {
           var data = new[]
            {
                "Mike",
                "Peter"
            };

            return data;
        }
    }
}
