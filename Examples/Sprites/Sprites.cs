using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprites
{
    public class Sprites
    {
        SpriteBatch batch;
        Texture tex;
        Matrix4fUniform worldMatrix;
        public void Run()
        {
            var window = new RenderWindow();

            Setup();

            while(window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.Green);

                Draw();

                window.Display();
            }
        }

        public void Setup()
        {
            batch = new SpriteBatch();
            tex = Texture.FromFile("Example.png");
            worldMatrix = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
        }

        public void Draw()
        {
            batch.Begin(worldMatrix);
            batch.Draw(tex, Color4.White, 0f, 0, 1, 1);
            batch.End();
        }
    }
}
