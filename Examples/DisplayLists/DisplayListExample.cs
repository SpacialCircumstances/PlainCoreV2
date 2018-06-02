using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.Graphics.Shapes;
using PlainCore.Graphics.Text;
using PlainCore.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace DisplayLists
{
    public class DisplayListExample
    {
        public void Run()
        {
            PlainCoreSettings.GlfwSearchPath = Path.GetFullPath("../../../../../Native/");
            var window = new RenderWindow();

            var rs = new TextureResourceSet(window);
            var t = Texture.FromFile("Example.png");
            var renderer = new SpriteRenderer();
            var dl = DynamicDisplayList<VertexPositionColorTexture>.Create(renderer);
            var sprites = new List<SpriteRenderItem>();
            var description = FontGenerator.GenerateFont("OpenSans-Regular.ttf", 40);
            var font = new Font(description);
            sprites.Add(SpriteDrawer.Draw(t, Color4.White, 0f, 0f));
            sprites.Add(SpriteDrawer.Draw(t, Color4.White, 100f, 100f));
            sprites.Add(SpriteDrawer.Draw(t, Color4.White, 200f, 200f));
            sprites.Add(SpriteDrawer.Draw(t, Color4.White, 400f, 400f));
            var glyphs = font.DrawString("ASDF", 500f, 400f, 1f);
            sprites.AddRange(glyphs);
            var indices = SpriteRenderer.GetIndices(sprites.Count);
            renderer.SetRenderItems(sprites.ToArray());

            while (window.IsOpen)
            {
                window.Clear(Color4.Black);

                window.PollEvents();

                renderer.RenderToData((vertices, count, tex) =>
                {
                    dl.SetVertices(vertices);
                    dl.SetIndices(indices, count);
                    rs.Texture = tex;
                    dl.Draw(rs, count);
                });

                window.Display();
            }

            dl.Dispose();
            font.Dispose();
            renderer.Dispose();
            window.Dispose();
        }
    }
}
