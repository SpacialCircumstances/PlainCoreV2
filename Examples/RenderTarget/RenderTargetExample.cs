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
        SpriteRenderer spriteRenderer;
        float rotation;
        Framebuffer defaultFramebuffer;
        RenderWindow window;
        DynamicDisplayList<VertexPositionColorTexture> spriteDisplayList;
        TextureResourceSet resourceSet;

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

            vab.Dispose();
            vao.Dispose();
            pipeline.Dispose();
            renderTexture.Dispose();
            spriteRenderer.Dispose();
            spriteDisplayList.Dispose();
            window.Dispose();
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

            spriteRenderer = new SpriteRenderer();
            defaultFramebuffer = Framebuffer.GetDefault();
            spriteDisplayList = new DynamicDisplayList<VertexPositionColorTexture>(VertexPositionColorTexture.Size);
            resourceSet = new TextureResourceSet(window);
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
                var sprite = SpriteBatcher.Draw(renderTexture, 0f, 0f);
                spriteRenderer.SetRenderItems(new SpriteRenderItem[]{ sprite });
                spriteRenderer.RenderToData((data, tex) =>
                {
                    spriteDisplayList.SetVertices(data);
                    spriteDisplayList.SetIndices(SpriteRenderer.GetIndices(1));
                    resourceSet.Texture = tex;
                });
            }

            defaultFramebuffer.Bind();
            normal.Set();

            rotation += 0.001f;
            spriteDisplayList.Draw(resourceSet);
        }

        private readonly VertexPositionColor[] vertices = new VertexPositionColor[]
        {
            new VertexPositionColor(new Vector2(200f, 200f), Color4.Blue),
            new VertexPositionColor(new Vector2(0f, 0f), Color4.Blue),
            new VertexPositionColor(new Vector2(400f, 0f), Color4.Blue)
        };
    }
}
