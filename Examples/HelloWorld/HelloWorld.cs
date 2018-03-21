using OpenGL;
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
            pipeline = new ShaderPipeline(new ShaderResource(PlainCore.Graphics.Core.ShaderType.Vertex, _VertexSourceGL), new ShaderResource(PlainCore.Graphics.Core.ShaderType.Fragment, _FragmentSourceGL));
            buffer = new VertexArrayBuffer<VertexPositionColorTexture>(32, OpenGL.BufferUsage.StaticDraw);
            buffer.Vertices = _ArrayPosition;
            indexBuffer = new IndexBuffer<VertexPositionColorTexture>(OpenGL.BufferUsage.StaticDraw);
            indexBuffer.Indices = indexArray;
            vao = new VertexArrayObject<VertexPositionColorTexture>(buffer, pipeline,
                new VertexAttributeDescription("aPosition", 2, OpenGL.VertexAttribType.Float, false, 32, 0),
                new VertexAttributeDescription("Color", 4, VertexAttribType.Float, false, 32, 8),
                new VertexAttributeDescription("texCoords", 2, OpenGL.VertexAttribType.Float, true, 32, 24));
            texture = new DeviceTexture("tex", 100, 100, true);
            texture.Data = Image.Load("Example.png").SavePixelData();
            texture.Bind();
            texture.CopyData();
            buffer.Bind();
            buffer.CopyData();
            buffer.Unbind();
            indexBuffer.Bind();
            indexBuffer.CopyData();
            indexBuffer.Unbind();
            worldMatrix = new Matrix4fUniform("uMVP");
        }

        protected void Draw()
        {
            var projection = Matrix4x4.CreateOrthographic(2.0f, 2.0f, -1.0f, +1.0f);
            worldMatrix.Matrix = projection;
            pipeline.Bind();
            worldMatrix.Set(pipeline);
            buffer.Bind();
            indexBuffer.Bind();
            vao.Bind();
            texture.Set(pipeline);
            indexBuffer.DrawIndexed(buffer);
        }

        private readonly string[] _VertexSourceGL = {
            "#version 330\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec4 Color;\n",
            "in vec2 texCoords;\n",
            "out vec4 frColor;\n",
            "out vec2 textureCoords;\n",
            "void main() {\n",
            "	 gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "    textureCoords = texCoords;\n",
            "    frColor = Color;\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL = {
            "#version 330\n",
            "uniform sampler2D tex;\n",
            "in vec4 frColor;\n",
            "in vec2 textureCoords;\n",
            "out vec4 outColor;\n",
            "void main() {\n",
            "	outColor = texture(tex, textureCoords) * frColor;\n",
            "}\n"
        };

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
