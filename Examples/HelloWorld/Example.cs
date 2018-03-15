using OpenGL;
using PlainCore.Graphics;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HelloWorld
{
    public class Example
    {
        public void Run()
        {
            var window = new RenderWindow();

            RenderControl_CreateGL320();

            while (window.IsOpen)
            {
                window.PollEvents();
                window.Clear(Color4.CornflowerBlue);

                _Angle = (_Angle + 0.1f) % 45.0f;
                RenderControl_RenderGL320();

                window.Display();
            }

            window.Dispose();
        }
        #region Common Shading

        // Note: abstractions for drawing using programmable pipeline.

        /// <summary>
        /// Shader object abstraction.
        /// </summary>
        private class Object : IDisposable
        {
            public Object(OpenGL.ShaderType shaderType, string[] source)
            {
                if (source == null)
                    throw new ArgumentNullException(nameof(source));

                // Create
                ShaderName = Gl.CreateShader(shaderType);
                // Submit source code
                Gl.ShaderSource(ShaderName, source);
                // Compile
                Gl.CompileShader(ShaderName);
                // Check compilation status
                int compiled;

                Gl.GetShader(ShaderName, ShaderParameterName.CompileStatus, out compiled);
                if (compiled != 0)
                    return;

                // Throw exception on compilation errors
                const int logMaxLength = 1024;

                StringBuilder infolog = new StringBuilder(logMaxLength);
                int infologLength;

                Gl.GetShaderInfoLog(ShaderName, logMaxLength, out infologLength, infolog);

                throw new InvalidOperationException($"unable to compile shader: {infolog}");
            }

            public readonly uint ShaderName;

            public void Dispose()
            {
                Gl.DeleteShader(ShaderName);
            }
        }

        /// <summary>
        /// Program abstraction.
        /// </summary>
        private class Program : IDisposable
        {
            public Program(string[] vertexSource, string[] fragmentSource)
            {
                // Create vertex and frament shaders
                // Note: they can be disposed after linking to program; resources are freed when deleting the program
                using (Object vObject = new Object(OpenGL.ShaderType.VertexShader, vertexSource))
                using (Object fObject = new Object(OpenGL.ShaderType.FragmentShader, fragmentSource))
                {
                    // Create program
                    ProgramName = Gl.CreateProgram();
                    // Attach shaders
                    Gl.AttachShader(ProgramName, vObject.ShaderName);
                    Gl.AttachShader(ProgramName, fObject.ShaderName);
                    // Link program
                    Gl.LinkProgram(ProgramName);

                    // Check linkage status
                    int linked;

                    Gl.GetProgram(ProgramName, ProgramProperty.LinkStatus, out linked);

                    if (linked == 0)
                    {
                        const int logMaxLength = 1024;

                        StringBuilder infolog = new StringBuilder(logMaxLength);
                        int infologLength;

                        Gl.GetProgramInfoLog(ProgramName, 1024, out infologLength, infolog);

                        throw new InvalidOperationException($"unable to link program: {infolog}");
                    }

                    // Get uniform locations
                    if ((LocationMVP = Gl.GetUniformLocation(ProgramName, "uMVP")) < 0)
                        throw new InvalidOperationException("no uniform uMVP");

                    // Get attributes locations
                    if ((LocationPosition = Gl.GetAttribLocation(ProgramName, "aPosition")) < 0)
                        throw new InvalidOperationException("no attribute aPosition");
                    if ((LocationColor = Gl.GetAttribLocation(ProgramName, "aColor")) < 0)
                        throw new InvalidOperationException("no attribute aColor");
                }
            }

            public readonly uint ProgramName;

            public readonly int LocationMVP;

            public readonly int LocationPosition;

            public readonly int LocationColor;

            public void Dispose()
            {
                Gl.DeleteProgram(ProgramName);
            }
        }

        /// <summary>
        /// Buffer abstraction.
        /// </summary>
        private class Buffer : IDisposable
        {
            public Buffer(float[] buffer)
            {
                if (buffer == null)
                    throw new ArgumentNullException(nameof(buffer));

                // Generate a buffer name: buffer does not exists yet
                BufferName = Gl.GenBuffer();
                // First bind create the buffer, determining its type
                Gl.BindBuffer(BufferTarget.ArrayBuffer, BufferName);
                // Set buffer information, 'buffer' is pinned automatically
                Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(4 * buffer.Length), buffer, BufferUsage.StaticDraw);
            }

            public readonly uint BufferName;

            public void Dispose()
            {
                Gl.DeleteBuffers(BufferName);
            }
        }

        /// <summary>
        /// Vertex array abstraction.
        /// </summary>
        private class VertexArray : IDisposable
        {
            public VertexArray(Program program, float[] positions, float[] colors)
            {
                if (program == null)
                    throw new ArgumentNullException(nameof(program));

                // Allocate buffers referenced by this vertex array
                _BufferPosition = new Buffer(positions);
                _BufferColor = new Buffer(colors);

                // Generate VAO name
                ArrayName = Gl.GenVertexArray();
                // First bind create the VAO
                Gl.BindVertexArray(ArrayName);

                // Set position attribute

                // Select the buffer object
                Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);
                // Format the vertex information: 2 floats from the current buffer
                Gl.VertexAttribPointer((uint)program.LocationPosition, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
                // Enable attribute
                Gl.EnableVertexAttribArray((uint)program.LocationPosition);

                // As above, but for color attribute
                Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferColor.BufferName);
                Gl.VertexAttribPointer((uint)program.LocationColor, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
                Gl.EnableVertexAttribArray((uint)program.LocationColor);
            }

            public readonly uint ArrayName;

            private readonly Buffer _BufferPosition;

            private readonly Buffer _BufferColor;

            public void Dispose()
            {
                Gl.DeleteVertexArrays(ArrayName);

                _BufferPosition.Dispose();
                _BufferColor.Dispose();
            }
        }

        /// <summary>
        /// The program used for drawing the triangle.
        /// </summary>
        private Program _Program;

        /// <summary>
        /// The vertex arrays used for drawing the triangle.
        /// </summary>
        private VertexArray _VertexArray;

        #endregion

        #region Shaders

        private void RenderControl_CreateGL320()
        {
            _Program = new Program(_VertexSourceGL, _FragmentSourceGL);
            _VertexArray = new VertexArray(_Program, _ArrayPosition, _ArrayColor);
        }

        private void RenderControl_RenderGL320()
        {
            // Compute the model-view-projection on CPU
            Matrix4x4 projection = Matrix4x4.CreateOrthographic(-1.0f, +1.0f, -1.0f, +1.0f);
            Matrix4x4 modelview = Matrix4x4.CreateTranslation(0.5f, 0.5f, 0.0f) * Matrix4x4.CreateRotationZ(_Angle);
            var combined = projection * modelview;
            // Select the program for drawing
            Gl.UseProgram(_Program.ProgramName);
            // Set uniform state           )
            Gl.UniformMatrix4f(_Program.LocationMVP, 1, false, ref combined);
            // Use the vertex array
            Gl.BindVertexArray(_VertexArray.ArrayName);
            // Draw triangle
            // Note: vertex attributes are streamed from GPU memory
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        private readonly string[] _VertexSourceGL = {
            "#version 150 compatibility\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec3 aColor;\n",
            "out vec3 vColor;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "	vColor = aColor;\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL = {
            "#version 150 compatibility\n",
            "in vec3 vColor;\n",
            "void main() {\n",
            "	gl_FragColor = vec4(vColor, 1.0);\n",
            "}\n"
        };

        private static readonly float[] _ArrayPosition = new float[] {
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f
        };

        /// <summary>
        /// Vertex color array.
        /// </summary>
        private static readonly float[] _ArrayColor = new float[] {
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 1.0f
        };

        private float _Angle;
    }
}
#endregion