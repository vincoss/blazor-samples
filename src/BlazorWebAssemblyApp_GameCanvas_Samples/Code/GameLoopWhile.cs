using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlazorWebAssemblyApp_GameCanvas_Samples.Code
{
	// TODO: if not used remove
	public class GameLoopWhile : IGameLoop
    {
        private bool _disposed;
        private bool _isRunning = true;

        public GameLoopWhile()
        {
            var t = new Thread(Loop);
            t.IsBackground = true;
            t.Start();
        }

        #region Private methods

        private void Loop()
        {
            int desiredFps = 60; // TODO: find optimal value for this
            int fpsTicNum = 0;
            int _framesRendered = 0;
            long delayTicks = (1000 / desiredFps) * TimeSpan.TicksPerMillisecond;
            var previousTicks = DateTime.Now.Ticks; // TODO: see other loop wheter this is right or use,  Environment.TickCount64
			var seconds = previousTicks;

			while (_isRunning)
            {
                var current = DateTime.Now.Ticks;
                var elapsedTicks = current - previousTicks;
                var elapsedSeconds = (current - seconds) / TimeSpan.TicksPerSecond;
                previousTicks = current;

                // Fps seconds
                if (elapsedSeconds >= 1)
                {
                    fpsTicNum = _framesRendered;
                    _framesRendered = 0;
                    seconds = current;
                }

                Render(fpsTicNum);

                _framesRendered++;
                var delay = delayTicks - elapsedTicks;
                var delayMilliseconds = (int)(delay / TimeSpan.TicksPerMillisecond);

                if (delayMilliseconds > 0)
                {
                    Thread.Sleep(delayMilliseconds);
                }
            }
        }

        #endregion

        #region IDisposable

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
                _isRunning = false;
            }

            _disposed = true;
        }

        #endregion

        public Action<int> Render { get; set; } = (fps) => { };
    }
}
