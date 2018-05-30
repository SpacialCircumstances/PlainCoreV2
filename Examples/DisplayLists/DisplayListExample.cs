using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.Graphics.Shapes;
using PlainCore.Graphics.Text;
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

            var dl = new DynamicDisplayList<VertexPositionColorTexture>(VertexPositionColorTexture.Size);
            var rs = new TextureResourceSet(window);
            var t = Texture.FromFile("Example.png");
            var renderer = new SpriteRenderer();
            var sprites = new List<SpriteRenderItem>();
            var description = FontGenerator.GenerateFont("OpenSans-Regular.ttf", 40);
            var font = new Font(description);
            sprites.Add(SpriteBatcher.Draw(t, Color4.White, 0f, 0f));
            sprites.Add(SpriteBatcher.Draw(t, Color4.White, 100f, 100f));
            sprites.Add(SpriteBatcher.Draw(t, Color4.White, 200f, 200f));
            sprites.Add(SpriteBatcher.Draw(t, Color4.White, 400f, 400f));
            var glyphs = font.DrawString("ASDF", 500f, 400f, 1f);
            sprites.AddRange(glyphs);
            var indices = SpriteRenderer.GetIndices(sprites.Count);
            renderer.SetRenderItems(sprites.ToArray());

            while (window.IsOpen)
            {
                window.Clear(Color4.Black);

                window.PollEvents();

                renderer.RenderToData((vertices, tex) =>
                {
                    dl.SetVertices(vertices);
                    dl.SetIndices(indices, vertices.Length + (vertices.Length / 2));
                    rs.Texture = tex;
                    dl.Draw(rs);
                });

                window.Display();
            }

            dl.Dispose();
            font.Dispose();
            renderer.Dispose();
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
