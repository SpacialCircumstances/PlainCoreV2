using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using System;

namespace HelloWorld
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var window = new RenderWindow();

            while(window.IsOpen)
            {
                window.PollEvents();
                window.Clear(1f, 0f, 0f);

                window.Display();
            }

            window.Dispose();
        }
    }
}
