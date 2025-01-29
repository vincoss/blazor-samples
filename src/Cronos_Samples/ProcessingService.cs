using Cronos;
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

            /*
                Read here con expression from some source, config or database... 
            */

            var cronExpression = "*/1 * * * *";
            Cro(cronExpression);

            return Task.CompletedTask;
        }

        private DateTime _lastRun = DateTime.MinValue;

        private void Cro(string targetExpression)
        {
            var utcNow = DateTime.UtcNow;
            var flag = CronExpression.TryParse(targetExpression, out CronExpression actualExpression);

            if (actualExpression != null)
            {
                var nextOccurrence = actualExpression.GetNextOccurrence(utcNow);

                if (nextOccurrence != null)
                {
                    Console.WriteLine($"Scheduled expression '{targetExpression}', with next occurrence is '{nextOccurrence}', utc now: '{utcNow}'.");

                    if (IsSameMinute(_lastRun, nextOccurrence.Value))
                    {
                        Console.WriteLine("The scheduled expression this minute.");
                        return;
                    }

                    if (IsSameMinute(utcNow, nextOccurrence.Value))
                    {
                        Console.WriteLine($"Running scheduled expression: '{nextOccurrence}' at utc now: '{utcNow}'.");

                        // Run processing schedule

                        _lastRun = utcNow;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Invalid cron expression: {targetExpression}");
            }
        }

        private static bool IsSameMinute(DateTime dateTime1, DateTime dateTime2)
        {
            return (Math.Abs((dateTime1 - dateTime2).TotalMinutes) < 1);
        }
    }
}
