using OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class ShaderResource: IDisposable
    {
        private const int MAX_LOG = 1024;

        public static ShaderResource FromFile(ShaderType type, string filename)
        {
            return new ShaderResource(type, File.ReadAllLines(filename));
        }

        public ShaderResource(ShaderType type, string[] code)
        {
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
