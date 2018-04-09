using OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A single shader object, for example a vertex shader.
    /// </summary>
    public class ShaderResource: IDisposable
    {
        private const int MAX_LOG = 1024;

        /// <summary>
        /// Load the shader code from a file.
        /// </summary>
        /// <param name="type">Type of the shader</param>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        public static ShaderResource FromFile(ShaderType type, string filename)
        {
            return new ShaderResource(type, File.ReadAllLines(filename));
        }

        /// <summary>
        /// Create a new shader.
        /// </summary>
        /// <param name="type">Type of the shader</param>
        /// <param name="code">GLSL code</param>
        public ShaderResource(ShaderType type, string[] code)
        {
            Type = type;

            handle = Gl.CreateShader(ToShaderType(type));
            Verify.VerifyResourceCreated(handle);

            Gl.ShaderSource(handle, code);
            Gl.CompileShader(handle);

            Gl.GetShader(handle, ShaderParameterName.CompileStatus, out int compiled);
            if (compiled != 0)
                return;

            var log = ReadShaderLog(handle);

            throw new InvalidOperationException($"Shader compilation failed: {log}");
        }

        protected readonly uint handle;

        public readonly ShaderType Type;

        public uint Handle => handle;

        private static OpenGL.ShaderType ToShaderType(ShaderType type)
        {
            switch(type)
            {
                case ShaderType.Fragment:
                    return OpenGL.ShaderType.FragmentShader;
                case ShaderType.Vertex:
                    return OpenGL.ShaderType.VertexShader;
                case ShaderType.Geometry:
                    return OpenGL.ShaderType.GeometryShader;
                case ShaderType.TesselationControl:
                    return OpenGL.ShaderType.TessControlShader;
                case ShaderType.TesselationEval:
                    return OpenGL.ShaderType.TessEvaluationShader;
            }

            return 0;
        }

        public void Dispose()
        {
            Gl.DeleteShader(handle);
        }

        protected static string ReadShaderLog(uint shader)
        {
            var infolog = new StringBuilder(MAX_LOG);
            Gl.GetShaderInfoLog(shader, MAX_LOG, out int infologLength, infolog);
            return infolog.ToString();
        }
    }
}
