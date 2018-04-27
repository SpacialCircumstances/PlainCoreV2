using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.Graphics.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Shapes
{
    public class ShapesExample
    {
        ShapeBatch batch;
        RenderWindow window;
        Triangle triangle;
        Rectangle rect;
        Circle circle;
        Circle circle2;
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

                window.Clear(Color4.Green);

                Draw();

                window.Display();
            }

            batch.Dispose();
        }

        public void Setup()
        {
            batch = new ShapeBatch();
            triangle = new Triangle(
                new VertexPositionColor(new Vector2(100f, 100f), Color4.Red),
                new VertexPositionColor(new Vector2(200f, 200f), Color4.Yellow),
                new VertexPositionColor(new Vector2(100f, 200f), Color4.Purple)
            );
            rect = new Rectangle(new Vector2(300f, 200f), new Vector2(200f, 200f), Color4.CornflowerBlue);
            circle = new Circle(64, new Vector2(500f, 500f), 50f, Color4.Black);
            circle2 = new Circle(16, new Vector2(100f, 400f), 100f, Color4.Blue);
        }

        public void Draw()
        {
            batch.Begin(window);
            batch.Draw(rect);
            batch.Draw(triangle);
            batch.Draw(circle);
            batch.Draw(circle2);
            batch.End();
        }
    }
}
