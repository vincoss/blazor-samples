using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using SystemTimer = System.Timers.Timer;


namespace BlazorWebAssemblyApp_GameCanvas_Samples.Code
{
    // TODO: if not used remove
    public class GameLoopTimer : IGameLoop
    {
        private bool _disposed;
        private int _fpsTicNum = 0;
        private int _framesRendered = 0;
        private long _previous = DateTime.Now.Ticks;
        private SystemTimer _timer = new SystemTimer();
        
        public GameLoopTimer()
        {
            _timer.Interval = 16;
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void TimerElapsed(object? source, ElapsedEventArgs e)
        {
            var current = DateTime.Now.Ticks;
            var elapsedSeconds = (current - _previous) / TimeSpan.TicksPerSecond;

            // Fps per second
            if (elapsedSeconds >= 1)
            {
                _fpsTicNum = _framesRendered;
                _framesRendered = 0;
                _previous = current;
            }

            Render(_fpsTicNum);
            _framesRendered++;
        }

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
                if (_timer != null)
                {
                    _timer.Elapsed -= TimerElapsed;
                    _timer.Dispose();
                    _timer = null;
                }
            }

            _disposed = true;
        }

        #endregion

        public Action<int> Render { get; set; } = (fps) => { };
    }
}
