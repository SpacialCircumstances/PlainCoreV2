using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.Graphics.Text;
using PlainCore.System;
using System;
using System.IO;

namespace HelloWorld
{
    public class HelloWorldExample
    {
        private RenderWindow window;
        private Font font;
        private SpriteRenderer renderer;
        private IChangeableDisplayList<VertexPositionColorTexture> displayList;
        private TextureResourceSet resourceSet;
        private Random random;

        public void Run()
        {
            PlainCoreSettings.GlfwSearchPath = Path.GetFullPath("../../../../../Native/");

            window = new RenderWindow();

            Setup();

            while(window.IsOpen)
            {
                window.PollEvents();

                Draw();
            };
        }

        private void Setup()
        {
            random = new Random();
            font = Font.GenerateFromFont("OpenSans-Regular.ttf", 50);
            var textSprites = font.DrawString("HelloWorld", 0, 0);
            renderer = new SpriteRenderer();
            renderer.SetRenderItems(textSprites);
            resourceSet = new TextureResourceSet(window);
            displayList = DynamicDisplayList<VertexPositionColorTexture>.Create(renderer);
            renderer.RenderToData((vertices, count, tex) =>
            {
                displayList.SetIndices(SpriteRenderer.GetIndices(count));
                displayList.SetVertices(vertices);
                resourceSet.Texture = tex;
            });
        }

        private void Draw()
        {
            window.Clear(GetRandomColor());

            displayList.Draw(resourceSet);

            window.Display();
        }

        private Color4 GetRandomColor()
        {
            var r = (byte)random.Next(256);
            var g = (byte)random.Next(256);
            var b = (byte)random.Next(256);

            return new Color4(r, g, b);
        }
    }
}
