using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Sprites
{
    public class Sprites
    {
        SpriteBatch batch;
        Texture tex;
        Texture tex2;
        Matrix4fUniform worldMatrix;
        float rot;
        public void Run()
        {
            var window = new RenderWindow();

            Setup();

            while(window.IsOpen)
            {
                rot++;

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
            tex2 = Texture.FromFile("Screenshot.png");
            worldMatrix = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
            worldMatrix.Matrix = Matrix4x4.CreateOrthographic(2, 2, -1, 1);
        }

        public void Draw()
        {
            batch.Begin(worldMatrix);
            batch.Draw(tex, Color4.White, 0, 0, 1, 1, 0.1f, 0, 0, 1, 1);
            batch.End();
        }
    }
}
