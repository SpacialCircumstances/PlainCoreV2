using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RenderTarget
{
    public class RenderTargetExample
    {
        VertexArrayBuffer<VertexPositionColor> vab;
        VertexArrayObject<VertexPositionColor> vao;
        ShaderPipeline pipeline;
        Matrix4fUniform worldMatrix;
        RenderTexture renderTexture;
        bool renderTargetDrawn = false;
        Viewport normal = new Viewport(0, 600, 800, 0);
        SpriteBatch batch;
        float rotation;
        Framebuffer defaultFramebuffer;
        RenderWindow window;

        public void Run()
        {
            window = new RenderWindow();

            Setup();

            while (window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.Red);

                Draw();

                window.Display();
            }
        }

        private void Setup()
        {
            pipeline = new ShaderPipeline(DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Vertex), DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Fragment));
            vab = new VertexArrayBuffer<VertexPositionColor>(VertexPositionColor.Size, OpenGL.BufferUsage.StaticDraw);
            vao = new VertexArrayObject<VertexPositionColor>(vab, pipeline, DefaultVertexDefinition.FromType(typeof(VertexPositionColor)));
            renderTexture = new RenderTexture(400, 400);
           
            vab.Bind();
            vao.Bind();
            pipeline.Bind();
            vab.CopyData(vertices);
            worldMatrix = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
            pipeline.Unbind();
            vao.Unbind();
            vab.Unbind();

            batch = new SpriteBatch();
            defaultFramebuffer = Framebuffer.GetDefault();
        }

        private void Draw()
        {
            if (!renderTargetDrawn)
            {
                renderTexture.Use();
                renderTexture.Clear(Color4.Gray);
                vab.Bind();
                vao.Bind();
                pipeline.Bind();
                worldMatrix.Matrix = renderTexture.WorldMatrix;
                worldMatrix.Set(pipeline);
                vab.DrawDirect(3);
                var imageData = renderTexture.Framebuffer.Read(800, 600);
                var image = Image.LoadPixelData<Rgba32>(imageData, 800, 600);
                image.Save("Screenshot.png");
                renderTargetDrawn = true;
            }
            defaultFramebuffer.Bind();
            normal.Set();

            rotation += 0.001f;
            batch.Begin(window);
            batch.Draw(renderTexture, Color4.White, 300f, 500f, 100f, 200f, rotation);
            batch.End();
        }

        private readonly VertexPositionColor[] vertices = new VertexPositionColor[]
        {
            new VertexPositionColor(new Vector2(200f, 200f), Color4.Blue),
            new VertexPositionColor(new Vector2(0f, 0f), Color4.Blue),
            new VertexPositionColor(new Vector2(400f, 0f), Color4.Blue)
        };
    }
}
