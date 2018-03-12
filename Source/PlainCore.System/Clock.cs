using System;
using System.Diagnostics;

namespace PlainCore.System
{
    public class Clock
    {
        private Stopwatch stopwatch = new Stopwatch();

        public void Start()
        {
#if DEBUG
            if (stopwatch.IsRunning) throw new NotSupportedException("Clock already running");
#endif
            stopwatch.Start();
        }

        public void Stop()
        {
#if DEBUG
            if (!stopwatch.IsRunning) throw new NotSupportedException("Clock not running");
#endif
            stopwatch.Stop();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public TimeSpan Restart()
        {
#if DEBUG
            if (!stopwatch.IsRunning) throw new NotSupportedException("Clock not running");
#endif
            var elapsed = stopwatch.Elapsed;
            stopwatch.Restart();
            return elapsed;
        }

        public TimeSpan Elapsed => stopwatch.Elapsed;

        public bool IsRunning => stopwatch.IsRunning;
    }
}
