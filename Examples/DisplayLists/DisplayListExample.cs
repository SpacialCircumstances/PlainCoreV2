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
            var sprite = new Sprite(t, new Vector2(100, 100), 0f, new Vector2(200f, 200f));
            var renderer = new SpriteRenderer();
            var sprites = new List<SpriteRenderItem>();
            var indices = SpriteRenderer.GetIndices(1);
            sprites.Add(SpriteBatcher.Draw(sprite));
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
            new VertexPositionColorTexture(new Vector2(0.0f, 0.0f), Color4.Blue, new Vector2(0f, 1f)),
            new VertexPositionColorTexture(new Vector2(400.0f, 0.0f), Color4.Blue, new Vector2(1f, 1f)),
            new VertexPositionColorTexture(new Vector2(400.0f, 400.0f), Color4.Blue, new Vector2(1f, 0f)),
            new VertexPositionColorTexture(new Vector2(0.0f, 400.0f), Color4.Blue, new Vector2(0f, 0f))
        };

        private static readonly int[] indexArray = new int[]
        {
            0, 1, 2, 0, 2, 3
        };
    }
}
