using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorWebAssemblyAppPWA_Svg_Samples
{
    public class BrowserResizeService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BrowserResizeService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./browser-resize.js").AsTask());
        }

        public async Task Initialize()
        {
            StaticOnLogAsync = this.OnResize;
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("registerResizeCallback");
        }

        private static Func<Task> StaticOnLogAsync;

        public event Func<Task> OnResize = () => {  return Task.CompletedTask; };

        [JSInvokable("OnBrowserResizeHandler")]
        public static async Task OnBrowserResizeee()
        {
            await StaticOnLogAsync?.Invoke();           
        }

        public async Task<int> GetInnerHeight()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("getInnerHeight");
        }

        public async Task<int> GetInnerWidth()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("getInnerWidth");
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
