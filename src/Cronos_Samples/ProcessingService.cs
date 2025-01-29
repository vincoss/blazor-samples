using System;


namespace Cronos_Samples
{
    public interface IProcessingService
    {
        Task RunAsync();
    }

    public class ProcessingService : IProcessingService
    {
        public Task RunAsync()
        {
            Console.WriteLine($"{nameof(ProcessingService)}: {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}
