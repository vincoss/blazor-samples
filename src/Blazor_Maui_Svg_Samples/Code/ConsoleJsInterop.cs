using Microsoft.JSInterop;

namespace Blazor_Maui_Svg_Samples.Code
{
	public class ConsoleJsInterop
	{
		private readonly IJSRuntime _jsRuntime;

		public ConsoleJsInterop(IJSRuntime jsRuntime)
		{
			this._jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(JSRuntime));
		}

		public async Task WriteLine(string message)
		{
			if (string.IsNullOrWhiteSpace(message) == false)
			{
				await this._jsRuntime.InvokeVoidAsync("console.log", message);
			}
		}
	}
}
