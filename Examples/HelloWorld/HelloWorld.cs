using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HelloWorld
{
    class HelloWorld
    {
        ShaderPipeline pipeline;
        VertexArrayObject<VertexPositionColor> vao;
        VertexArrayBuffer<VertexPositionColor> buffer;
        IndexBuffer<VertexPositionColor> indexBuffer;
        Matrix4fUniform worldMatrix;

        public void Run()
        {
            var window = new RenderWindow();

            Setup();

            while(window.IsOpen)
            {
                window.PollEvents();

                window.Clear(Color4.CornflowerBlue);

                Draw();

                window.Display();
            }
            
        }

        protected void Setup()
        {
            pipeline = new ShaderPipeline(new ShaderResource(ShaderType.Vertex, _VertexSourceGL), new ShaderResource(ShaderType.Fragment, _FragmentSourceGL));
            buffer = new VertexArrayBuffer<VertexPositionColor>(24, OpenGL.BufferUsage.StaticDraw);
            buffer.Vertices = _ArrayPosition;
            indexBuffer = new IndexBuffer<VertexPositionColor>(OpenGL.BufferUsage.StaticDraw);
            indexBuffer.Indices = indexArray;
            vao = new VertexArrayObject<VertexPositionColor>(buffer, pipeline, new VertexAttributeDescription("aPosition", 2, OpenGL.VertexAttribType.Float, false, 24, 0), new VertexAttributeDescription("vColor", 4, OpenGL.VertexAttribType.Float, false, 24, 8));
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
            indexBuffer.DrawIndexed(buffer);
        }

        private readonly string[] _VertexSourceGL = {
            "#version 330\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec4 vColor;\n",
            "out vec4 frColor;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "   frColor = vColor;\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL = {
            "#version 330\n",
            "in vec4 frColor;\n",
            "out vec4 outColor;\n",
            "void main() {\n",
            "	outColor = frColor;\n",
            "}\n"
        };

        private static readonly VertexPositionColor[] _ArrayPosition = new VertexPositionColor[] {
            new VertexPositionColor(new Vector2(0.0f, 0.0f), Color4.Lime),
            new VertexPositionColor(new Vector2(1.0f, 0.0f), Color4.Yellow),
            new VertexPositionColor(new Vector2(1.0f, 1.0f), Color4.Black),
            new VertexPositionColor(new Vector2(0.0f, 1.0f), Color4.Red),
        };

        private static readonly int[] indexArray = new int[]
        {
            0, 1, 2, 0, 2, 3
        };
    }
}
