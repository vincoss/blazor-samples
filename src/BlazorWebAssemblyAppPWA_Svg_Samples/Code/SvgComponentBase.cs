using Microsoft.AspNetCore.Components;
using System.Numerics;

namespace BlazorWebAssemblyAppPWA_Svg_Samples.Code
{
    public class SvgComponentBase : ComponentBase, IDisposable
    {
        [Parameter]
        public float Width { get; set; }

        [Parameter]
        public float Height { get; set; }

        [Parameter]
        public float X { get; set; }

        [Parameter]
        public float Y { get; set; }

        public bool PreserveAspect { get; set; } = true;

        [Parameter]
        public string? Style { get; set; }

        /// <summary>
        /// CaptureUnmatched Attributes.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string UniqueID { get; set; } = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "-").Replace("+", "-").Substring(0, 10);

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                WindowSizeSingleton.PropertyChanged -= OnWindowSizeSingleton;
            }

            _disposed = true;
        }

        #endregion

        [Inject]
        private IWindowSize WindowSizeSingleton { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            WindowSizeSingleton.PropertyChanged += OnWindowSizeSingleton; // TODO: can we move this into the Layout page or componentBase? Window resize and orientaion change

            _sizeDelta = new Vector2(Width, Height);
            _anchorPosition = new Vector2(X, Y);

            AdaptToDifferentAspectRatio();
        }

        private async void OnWindowSizeSingleton(object? sender, EventArgs e)
        {
            AdaptToDifferentAspectRatio();
            await InvokeAsync(StateHasChanged);
        }

        protected Vector2 _sizeDelta;
        protected Vector2 _anchorPosition;

        public void AdaptToDifferentAspectRatio()
        {
            float screenWidth = (float)WindowSizeSingleton.Width;
            float screenHeight = (float)WindowSizeSingleton.Height;
            float itemWidth = Width;
            float itemHeight = Height;

            float offsetX = X;
            float offsetY = Y;

            // NOTE: The width has issues on scale and position xy

            /*
                TODO: add property CenterXY, default false 
            */

            // var preserveAspect = WindowSizeSingleton.IsPortrait ? PreserveAspect : false;
            var preserveAspect = PreserveAspect;

            _sizeDelta = new Vector2(screenWidth * itemWidth / 100, ((preserveAspect) ? screenWidth * itemWidth : screenHeight * itemHeight) / 100);
            //_anchorPosition = new Vector2(screenWidth * offsetX / 100, screenHeight * offsetY / 100);
            //_anchorPosition = new Vector2(screenWidth * offsetX / 100, ((preserveAspect) ? screenWidth * offsetX : screenHeight * offsetY) / 100);
        }
    }
}
