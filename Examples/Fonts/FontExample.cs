using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.Graphics.Text;
using SixLabors.ImageSharp;
using System;

namespace Fonts
{
    public class FontExample
    {
        SpriteBatch batch;
        RenderWindow window;
        Font font;
        public void Run()
        {
            window = new RenderWindow();
            window.OnClosed += () =>
            {
                Console.WriteLine("Window closed");
            };

            Setup();

            while (window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.Black);

                Draw();

                window.Display();
            }

            batch.Dispose();
        }

        public void Setup()
        {
            batch = new SpriteBatch();
            var description = new FontGenerator().GenerateFont("OpenSans-Regular.ttf", 40);
            font = Font.FromDescription(description);
        }

        public void Draw()
        {
            batch.Begin(window);
            font.Draw(batch, "ASDFGHJKL", 100f, 100f);
            batch.End();
        }
    }
}
