using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.System;
using System;

namespace HelloWorld
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var window = new RenderWindow();
            var clock = new Clock();
            clock.Start();
            while(window.IsOpen)
            {
                window.PollEvents();
                if (clock.Elapsed.TotalSeconds > 1) clock.Restart();
                window.Clear(new Color4((float)clock.Elapsed.TotalSeconds, 0f, 0f, 0f));

                window.Display();
            }

            window.Dispose();
        }
    }
}
