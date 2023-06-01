using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Xml.Linq;

namespace BlazorWebAssemblyAppPWA_Svg_Samples.Code
{
	public class HelperJsInterop : IAsyncDisposable
	{
		private static Func<int, int, Task>? _staticOnLogAsync;
		private readonly Lazy<Task<IJSObjectReference>> moduleTask;

		public HelperJsInterop(IJSRuntime jsRuntime)
		{
			moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./browser-resize.js").AsTask());
		}

		public async Task Initialize()
		{
			_staticOnLogAsync = this.OnResize;
			var module = await moduleTask.Value;
			await module.InvokeVoidAsync("registerResizeCallback");
		}

		[JSInvokable("OnBrowserResizeHandler")]
		public static async Task OnBrowserResizeee(int width, int height)
		{
			await _staticOnLogAsync?.Invoke(width, height);
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

		public async Task<BoundingClientRect> GetElementDOMRect(object element)
		{
			if (element == null) throw new ArgumentNullException(nameof(element));
			
			var args = new object[] { element };
			var module = await moduleTask.Value;
			return await module.InvokeAsync<BoundingClientRect>("getElementDOMRect", args);
		}

		public async ValueTask DisposeAsync()
		{
			if (moduleTask.IsValueCreated)
			{
				var module = await moduleTask.Value;
				await module.DisposeAsync();
			}
		}

		public event Func<int, int, Task> OnResize = (w,h) => { return Task.CompletedTask; };
	}

	public class BoundingClientRect
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public double Top { get; set; }
		public double Right { get; set; }
		public double Bottom { get; set; }
		public double Left { get; set; }
	}

	public class JSRuntimeService : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>>? moduleTask;
        private DotNetObjectReference<JSRuntimeService>? _dotNetHelper;
		private readonly IJSRuntime _jSRuntime;

        public JSRuntimeService(IJSRuntime jsRuntime)
        {
			_jSRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            _dotNetHelper = DotNetObjectReference.Create(this);
		}

		public async Task InitializeAync()
		{
            await _jSRuntime.InvokeVoidAsync("WindowHelpers.setDotNetHelper", _dotNetHelper);
		}

		[JSInvokable("OnBrowserResizeHandlerInstance")]
		public Task OnBrowserResize(int width, int height)
		{
			var handler = _windowSizeChanged;
            if (handler != null)
            {
                handler(this, new WindowSizeChangedEventArgs(width, height));
            }
            return Task.CompletedTask;
        }

		private EventHandler<WindowSizeChangedEventArgs>? _windowSizeChanged;

		/// <summary>
		/// An event that fires when the window is resized.
		/// </summary>
		public event EventHandler<WindowSizeChangedEventArgs> WindowResize
		{
			add { _windowSizeChanged += value; }
			remove { _windowSizeChanged -= value; }
		}

		public async ValueTask DisposeAsync()
		{
			if (moduleTask != null && moduleTask.IsValueCreated)
			{
				var module = await moduleTask.Value;
				await module.DisposeAsync();
			}
		}
	}

    public class WindowSizeChangedEventArgs : EventArgs
    {
        public WindowSizeChangedEventArgs(int width, int height)
        {
            Width = width;
            Height = height;
        }

		public int Width { get; }
        public int Height { get; }
    }

}
