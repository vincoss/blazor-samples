using Microsoft.JSInterop;

namespace BlazorWebAssemblyAppPWA_Svg_Samples.Code
{
	public class HelperJsInterop : IAsyncDisposable
	{
		private static Func<Task>? _staticOnLogAsync;
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
		public static async Task OnBrowserResizeee()
		{
			await _staticOnLogAsync?.Invoke();
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

		public event Func<Task> OnResize = () => { return Task.CompletedTask; };
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
}
