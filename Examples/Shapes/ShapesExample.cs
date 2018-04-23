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
            triangle = new Triangle(new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector2(100f, 100f), Color4.Red),
                new VertexPositionColor(new Vector2(200f, 200f), Color4.Yellow),
                new VertexPositionColor(new Vector2(100f, 200f), Color4.Purple)
            });
        }

        public void Draw()
        {
            batch.Begin(window);
            
            foreach (var i in triangle.GetVertices())
            {
                batch.PushVertex(i.Position.X, i.Position.Y, i.Color);
            }
            batch.End();
        }
    }
}
