using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.Graphics.Shapes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DisplayLists
{
    public class DisplayListExample
    {
        public void Run()
        {
            var window = new RenderWindow();

            var renderer = new ShapeRenderer();
            var dl = new StreamDisplayList<VertexPositionColor>(VertexPositionColor.Size);
            var sq1 = new Rectangle(new Vector2(100f, 100f), new Vector2(200f, 200f), Color4.White);
            var c1 = new Circle(12, new Vector2(500, 500), 60f, Color4.Red);
            var rs = new DefaultResourceSet(window);

            while (window.IsOpen)
            {
                window.Clear(Color4.Black);

                window.PollEvents();
                renderer.Begin();
                renderer.Render(c1);
                renderer.Render(sq1);
                renderer.End(dl);
                dl.Draw(rs);

                window.Display();
            }

            window.Dispose();
        }

        private static readonly VertexPositionColor[] vertexArray = new VertexPositionColor[] {
            new VertexPositionColor(new Vector2(0.0f, 0.0f), Color4.Blue),
            new VertexPositionColor(new Vector2(400.0f, 0.0f), Color4.Red),
            new VertexPositionColor(new Vector2(400.0f, 400.0f), Color4.Green),
            new VertexPositionColor(new Vector2(0.0f, 400.0f), Color4.Yellow)
        };

        private static readonly int[] indexArray = new int[]
        {
            0, 1, 2, 0, 2, 3
        };
    }
}
