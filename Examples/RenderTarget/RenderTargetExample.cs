using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using SixLabors.ImageSharp;
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

        public void Run()
        {
            var window = new RenderWindow();

            Setup();

            while (window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.Black);

                Draw();

                window.Display();

                throw new NotSupportedException();
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
            pipeline.Unbind();
            vao.Unbind();
            vab.Unbind();
            worldMatrix = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
            worldMatrix.Matrix = Matrix4x4.Identity;
        }

        private void Draw()
        {
            renderTexture.Use();
            vab.Bind();
            vao.Bind();
            pipeline.Bind();
            worldMatrix.Set(pipeline);
            vab.DrawDirect(3);
            var imageData = renderTexture.Buffer.Read(800, 600);
            var image = Image.LoadPixelData<Rgba32>(imageData, 800, 600);
            image.Save("Screenshot.png");
        }

        private readonly VertexPositionColor[] vertices = new VertexPositionColor[]
        {
            new VertexPositionColor(new Vector2(0.5f, 0.5f), Color4.Blue),
            new VertexPositionColor(new Vector2(-0.5f, 0.5f), Color4.Blue),
            new VertexPositionColor(new Vector2(-0.5f, -0.5f), Color4.Blue)
        };
    }
}
