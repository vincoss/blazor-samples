using Microsoft.JSInterop;

namespace Blazor_Samples.Services
{
    public class ServiceA
    {
       public ServiceA()
        {
        }
    }

    public class ServiceB
    {
        public ServiceB() 
        { 
        }
    }

    public class ServiceC
    {
        public ServiceC()
        {
        }
    }

    public class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new
        TaskFactory(CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            return AsyncHelper._taskFactory
              .StartNew<Task<TResult>>(func)
              .Unwrap<TResult>()
              .GetAwaiter()
              .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            AsyncHelper._taskFactory
              .StartNew<Task>(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        public static void RunSync(Func<ValueTask> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            AsyncHelper._taskFactory
              .StartNew<ValueTask>(func).RunSynchronously();
              //.GetAwaiter()
              //.GetResult();
        }
    }
}
