namespace Fundamentals_BlazorWebAssemblyApp.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<string>> GetCustomers();
    }
}
