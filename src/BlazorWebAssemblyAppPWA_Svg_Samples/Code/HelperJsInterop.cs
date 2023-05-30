using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

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

	//public class JSRuntimeService : IAsyncDisposable
	//{
	//	private readonly Lazy<Task<IJSObjectReference>> moduleTask;

	//	public JSRuntimeService(IJSRuntime jsRuntime)
	//	{
	//		moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./browser-resize.js").AsTask());
	//	}

	//	public async Task InitializeAync()
	//	{
	//		var module = await moduleTask.Value;
	//		await module.InvokeVoidAsync("registerResizeCallback");
	//		_isInitialized = true;
	//	}

 //       [JSInvokable("OnBrowserResizeHandler")]
 //       public static async Task OnBrowserResizeee(int width, int height)
 //       {
	//	//	return Task.CompletedTask;
 //       }

 //       private EventHandler<EventArgs>? _windowSizeChanged;

	//	/// <summary>
	//	/// An event that fires when the window is resized.
	//	/// </summary>
	//	public event EventHandler<EventArgs> WindowResize
	//	{
	//		add {
	//			AssertInitialized();
	//			_windowSizeChanged += value;
	//		}
	//		remove {
	//			AssertInitialized();
	//			_windowSizeChanged -= value;
	//		}
	//	}
	//	private bool _isInitialized;
	//	private void AssertInitialized()
	//	{
	//		if (!_isInitialized)
	//		{
	//			EnsureInitialized();
	//		}

	//		if (!_isInitialized)
	//		{
	//			throw new InvalidOperationException($"'{GetType().Name}' has not been initialized.");
	//		}
	//	}

	//	/// <summary>
	//	/// Allows derived classes to lazily self-initialize. Implementations that support lazy-initialization should override
	//	/// this method and call <see cref="Initialize(string, string)" />.
	//	/// </summary>
	//	protected virtual void EnsureInitialized()
	//	{
	//	}

	//	public async ValueTask DisposeAsync()
	//	{
	//		if (moduleTask.IsValueCreated)
	//		{
	//			var module = await moduleTask.Value;
	//			await module.DisposeAsync();
	//		}
	//	}
	//}

}
