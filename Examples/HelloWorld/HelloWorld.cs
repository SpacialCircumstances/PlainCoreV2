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
            vao = new VertexArrayObject<VertexPositionColor>(buffer, pipeline, new VertexAttributeDescription("aPosition", 2, OpenGL.VertexAttribType.Float, false, 24, 0), new VertexAttributeDescription("vColor", 4, OpenGL.VertexAttribType.Float, false, 24, 8));
            buffer.Bind();
            buffer.CopyData();
            buffer.Unbind();
            worldMatrix = new Matrix4fUniform("uMVP");
        }

        protected void Draw()
        {
            Matrix4x4 projection = Matrix4x4.CreateOrthographic(2.0f, 2.0f, -1.0f, +1.0f);
            worldMatrix.Matrix = projection;
            pipeline.Bind();
            worldMatrix.Set(pipeline);
            buffer.Bind();
            vao.Bind();
            buffer.DrawDirect();
        }

        private readonly string[] _VertexSourceGL = {
            "#version 150 compatibility\n",
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
            "#version 150 compatibility\n",
            "in vec4 frColor;\n",
            "void main() {\n",
            "	gl_FragColor = frColor;\n",
            "}\n"
        };

        private static readonly VertexPositionColor[] _ArrayPosition = new VertexPositionColor[] {
            new VertexPositionColor(new Vector2(0.0f, 0.0f), new Color4(1f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector2(1.0f, 0.0f), new Color4(1f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector2(1.0f, 1.0f), new Color4(1f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector2(0.0f, 0.0f), new Color4(1f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector2(0.0f, 1.0f), new Color4(1f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector2(1.0f, 1.0f), new Color4(1f, 1f, 0f, 1f)),
        };
    }
}
