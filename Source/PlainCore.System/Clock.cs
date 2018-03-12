using System;
using System.Diagnostics;

namespace PlainCore.System
{
    public class Clock
    {
        public Clock()
        {

        }

        private Stopwatch stopwatch;

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public TimeSpan Restart()
        {
            var elapsed = stopwatch.Elapsed;
            stopwatch.Restart();
            return elapsed;
        }

        public TimeSpan Elapsed
        {
            get => stopwatch.Elapsed;
        }
    }
}
