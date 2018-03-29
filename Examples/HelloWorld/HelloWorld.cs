using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using SixLabors.ImageSharp;
using System.Numerics;
using System.Text;

namespace HelloWorld
{
    class HelloWorld
    {
        ShaderPipeline pipeline;
        VertexArrayObject<VertexPositionColorTexture> vao;
        VertexArrayBuffer<VertexPositionColorTexture> buffer;
        IndexBuffer<VertexPositionColorTexture> indexBuffer;
        Matrix4fUniform worldMatrix;
        DeviceTexture texture;
        Framebuffer defaultFramebuffer;

        int counter;

        public void Run()
        {
            var window = new RenderWindow();

            Setup();

            while (window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.CornflowerBlue);

                Draw();

                window.Display();
            }

        }

        protected void Setup()
        {
            var vs = DefaultShader.FromType(typeof(VertexPositionColorTexture), PlainCore.Graphics.Core.ShaderType.Vertex);
            var fs = DefaultShader.FromType(typeof(VertexPositionColorTexture), PlainCore.Graphics.Core.ShaderType.Fragment);
            pipeline = new ShaderPipeline(vs,  fs);
            buffer = new VertexArrayBuffer<VertexPositionColorTexture>(32, OpenGL.BufferUsage.StaticDraw);
            indexBuffer = new IndexBuffer<VertexPositionColorTexture>(OpenGL.BufferUsage.StaticDraw);
            vao = new VertexArrayObject<VertexPositionColorTexture>(buffer, pipeline,
                DefaultVertexDefinition.FromType(typeof(VertexPositionColorTexture)));
            texture = new DeviceTexture(DefaultShader.DEFFAULT_TEXTURE_UNIFORM_NAME, 100, 100, true);
            defaultFramebuffer = Framebuffer.GetDefault();
            var imageData = Image.Load("Example.png").SavePixelData();
            texture.Bind();
            texture.CopyData(imageData);
            buffer.Bind();
            buffer.CopyData(_ArrayPosition);
            buffer.Unbind();
            indexBuffer.Bind();
            indexBuffer.CopyData(indexArray);
            indexBuffer.Unbind();
            worldMatrix = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
        }

        protected void Draw()
        {
            counter++;
            var projection = Matrix4x4.CreateOrthographic(2.0f, 2.0f, -1.0f, +1.0f);
            worldMatrix.Matrix = projection;
            pipeline.Bind();
            worldMatrix.Set(pipeline);
            buffer.Bind();
            indexBuffer.Bind();
            vao.Bind();
            texture.Set(pipeline);
            indexBuffer.DrawIndexed(buffer, 6);
            if(counter == 100)
            {
                SaveScreenshot();
            }
        }

        private void SaveScreenshot()
        {
            var imageData = defaultFramebuffer.Read(800, 600);
            var image = Image.LoadPixelData<Rgba32>(imageData, 800, 600);
            image.Save("Screenshot.png");
        }

        private static readonly VertexPositionColorTexture[] _ArrayPosition = new VertexPositionColorTexture[] {
            new VertexPositionColorTexture(new Vector2(0.0f, 0.0f), Color4.Blue, new Vector2(0f, 1f)),
            new VertexPositionColorTexture(new Vector2(1.0f, 0.0f), Color4.Blue, new Vector2(1f, 1f)),
            new VertexPositionColorTexture(new Vector2(1.0f, 1.0f), Color4.Blue, new Vector2(1f, 0f)),
            new VertexPositionColorTexture(new Vector2(0.0f, 1.0f), Color4.Blue, new Vector2(0f, 0f))
        };

        private static readonly int[] indexArray = new int[]
        {
            0, 1, 2, 0, 2, 3
        };
    }
}
