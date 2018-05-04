using PlainCore.Graphics;
using PlainCore.Graphics.Core;
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

            var dl = new StreamDisplayList<VertexPositionColor>(VertexPositionColor.Size);
            dl.SetIndices(indexArray);
            dl.SetVertices(vertexArray);
            var rs = new DefaultResourceSet(window);

            while (window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.Black);
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
