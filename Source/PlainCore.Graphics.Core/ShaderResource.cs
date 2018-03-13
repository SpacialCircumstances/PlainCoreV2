using OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class ShaderResource
    {
        public static ShaderResource FromFile(ShaderType type, string filename)
        {
            return new ShaderResource(type, File.ReadAllLines(filename));
        }

        public ShaderResource(ShaderType type, string[] code)
        {
            handle = Gl.CreateShader(ToShaderType(type));

            Gl.ShaderSource(handle, code);
            Gl.CompileShader(handle);
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
            }

            return 0;
        }
    }
}
