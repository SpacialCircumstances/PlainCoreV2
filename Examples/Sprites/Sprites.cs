using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Sprites
{
    public class Sprites
    {
        SpriteBatch batch;
        Texture tex;
        Texture tex2;
        RenderWindow window;
        public void Run()
        {
            window = new RenderWindow();
            window.OnClosed += () =>
            {
                Console.WriteLine("Window closed");
            };

            Setup();

            while(window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.Green);

                Draw();

                window.Display();
            }

            batch.Dispose();
        }

        public void Setup()
        {
            batch = new SpriteBatch();
            tex = Texture.FromFile("Example.png");
            tex2 = Texture.FromFile("Screenshot.png");
        }

        public void Draw()
        {
            batch.Begin(window);
            batch.Draw(tex, 0f, 0f, 100f, 100f);
            batch.End();
        }
    }
}
