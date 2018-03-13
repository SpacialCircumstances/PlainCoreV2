using PlainCore.Graphics.Core;
using System;

namespace HelloWorld
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var window = new OpenGLWindow(800, 600, "PlainCore Hello World!", false);

            while(window.IsOpen)
            {
                window.PollEvents();

                window.Display();
            }

            window.Dispose();
        }
    }
}
