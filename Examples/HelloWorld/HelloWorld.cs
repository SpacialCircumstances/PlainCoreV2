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
        VertexArrayObject<Vector2> vao;
        VertexArrayBuffer<Vector2> buffer;
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
            buffer = new VertexArrayBuffer<Vector2>(8, OpenGL.BufferUsage.StaticDraw);
            buffer.Vertices = _ArrayPosition;
            vao = new VertexArrayObject<Vector2>(buffer, pipeline, new VertexAttributeDescription("aPosition", 2, OpenGL.VertexAttribType.Float, false, 0, 0));
            buffer.Bind();
            buffer.CopyData();
            buffer.Unbind();
            worldMatrix = new Matrix4fUniform("uMVP");
        }

        protected void Draw()
        {
            Matrix4x4 projection = Matrix4x4.CreateOrthographic(-1.0f, +1.0f, -1.0f, +1.0f);
            worldMatrix.Matrix = projection;
            pipeline.Bind();
            worldMatrix.Set(pipeline);
            buffer.Bind();
            vao.Bind();
            buffer.Draw();
        }

        private readonly string[] _VertexSourceGL = {
            "#version 150 compatibility\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL = {
            "#version 150 compatibility\n",
            "void main() {\n",
            "	gl_FragColor = vec4(1.0, 0.0, 1.0, 1.0);\n",
            "}\n"
        };

        private static readonly Vector2[] _ArrayPosition = new Vector2[] {
            new Vector2(0.0f, 0.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(1.0f, 1.0f)
        };
    }
}
