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

            var dl = new StreamDisplayList<VertexPositionColorTexture>(VertexPositionColorTexture.Size);
            var rs = new TextureResourceSet(window);
            var t = Texture.FromFile("Example.png");
            var renderer = new SpriteRenderer();
            var sprites = new List<SpriteRenderItem>();
            var indices = SpriteRenderer.GetIndices(1);
            sprites.Add(SpriteBatcher.Draw(t, Color4.White, 100f, 100f, 200f, 200f, 0f));
            renderer.SetRenderItems(sprites.ToArray());

            while (window.IsOpen)
            {
                window.Clear(Color4.Red);

                window.PollEvents();

                renderer.RenderToData((vertices, tex) =>
                {
                    dl.SetVertices(vertices);
                    dl.SetIndices(indices);
                    rs.Texture = tex;
                    dl.Draw(rs);
                });

                window.Display();
            }

            window.Dispose();
        }

        private static readonly VertexPositionColorTexture[] vertexArray = new VertexPositionColorTexture[] {
            new VertexPositionColorTexture(new Vector2(0.0f, 0.0f), Color4.Blue, new Vector2(0f, 1f)), //LT
            new VertexPositionColorTexture(new Vector2(400.0f, 0.0f), Color4.Blue, new Vector2(1f, 1f)), //RT
            new VertexPositionColorTexture(new Vector2(400.0f, 400.0f), Color4.Blue, new Vector2(1f, 0f)), //RD
            new VertexPositionColorTexture(new Vector2(0.0f, 400.0f), Color4.Blue, new Vector2(0f, 0f)) //LD
        };

        private static readonly int[] indexArray = new int[]
        {
            0, 1, 2, 0, 2, 3
        };
    }
}
