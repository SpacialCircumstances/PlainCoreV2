using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using PlainCore.System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace RenderTarget
{
    public class RenderTargetExample
    {
        private VertexArrayBuffer<VertexPositionColor> vab;
        private VertexArrayObject<VertexPositionColor> vao;
        private ShaderPipeline pipeline;
        private Matrix4fUniform worldMatrix;
        private RenderTexture renderTexture;
        private bool renderTargetDrawn;
        private readonly Viewport normal = new Viewport(0, 600, 800, 0);
        private SpriteRenderer spriteRenderer;
        private float rotation;
        private Framebuffer defaultFramebuffer;
        private RenderWindow window;
        private DynamicDisplayList<VertexPositionColorTexture> spriteDisplayList;
        private TextureResourceSet resourceSet;

        public void Run()
        {
            PlainCoreSettings.GlfwSearchPath = Path.GetFullPath("../../../../../Native/");

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
                var sprite = SpriteDrawer.Draw(renderTexture, 0f, 0f);
                spriteRenderer.SetRenderItems(new SpriteRenderItem[]{ sprite });
                spriteRenderer.RenderToData((data, count, tex) =>
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
