using OpenGL;
using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using SixLabors.ImageSharp;
using System.Numerics;
using System.Text;

namespace HelloWorld
{
    class HelloTexture
    {
        ShaderPipeline pipeline;
        VertexArrayObject<VertexPositionTexture> vao;
        VertexArrayBuffer<VertexPositionTexture> buffer;
        IndexBuffer<VertexPositionTexture> indexBuffer;
        Matrix4fUniform worldMatrix;
        uint texture;

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
            buffer = new VertexArrayBuffer<VertexPositionTexture>(16, OpenGL.BufferUsage.StaticDraw);
            buffer.Vertices = _ArrayPosition;
            indexBuffer = new IndexBuffer<VertexPositionTexture>(OpenGL.BufferUsage.StaticDraw);
            indexBuffer.Indices = indexArray;
            vao = new VertexArrayObject<VertexPositionTexture>(buffer, pipeline,
                new VertexAttributeDescription("aPosition", 2, OpenGL.VertexAttribType.Float, false, 16, 0),
                new VertexAttributeDescription("texCoords", 2, OpenGL.VertexAttribType.Float, true, 16, 8));
            texture = Gl.GenTexture();
            var imageData = Image.Load("Example.png").SavePixelData();
            Gl.BindTexture(TextureTarget.Texture2d, texture);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, 100, 100, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageData);
            Gl.GenerateMipmap(TextureTarget.Texture2d);
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
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, texture);
            Gl.Uniform1(pipeline.GetUniformLocation("tex"), 0);
            indexBuffer.DrawIndexed(buffer);
        }

        private readonly string[] _VertexSourceGL = {
            "#version 330\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec2 texCoords;\n",
            "out vec2 textureCoords;\n",
            "void main() {\n",
            "	 gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "    textureCoords = texCoords;\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL = {
            "#version 330\n",
            "uniform sampler2D tex;\n",
            "in vec2 textureCoords;\n",
            "out vec4 outColor;\n",
            "void main() {\n",
            "	outColor = texture(tex, textureCoords);\n",
            "}\n"
        };

        private static readonly VertexPositionTexture[] _ArrayPosition = new VertexPositionTexture[] {
            new VertexPositionTexture(new Vector2(0.0f, 0.0f), new Vector2(0f, 1f)),
            new VertexPositionTexture(new Vector2(1.0f, 0.0f), new Vector2(1f, 1f)),
            new VertexPositionTexture(new Vector2(1.0f, 1.0f), new Vector2(1f, 0f)),
            new VertexPositionTexture(new Vector2(0.0f, 1.0f), new Vector2(0f, 0f))
        };

        private static readonly int[] indexArray = new int[]
        {
            0, 1, 2, 0, 2, 3
        };
    }
}
