using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class ShaderPipeline: IBindable
    {
        private const int MAX_LOG = 1024;

        public ShaderPipeline(List<ShaderResource> shaders)
        {
            Handle = Gl.CreateProgram();
            Verify.VerifyResourceCreated(Handle);
            UploadShaders(shaders.ToArray());
        }

        public ShaderPipeline(params ShaderResource[] shaders)
        {
            Handle = Gl.CreateProgram();
            Verify.VerifyResourceCreated(Handle);
            UploadShaders(shaders);
        }

        public readonly uint Handle;

        protected void UploadShaders(ShaderResource[] shaders)
        {
            Verify.VerifyShaders(shaders);

            foreach(var shader in shaders)
            {
                Gl.AttachShader(Handle, shader.Handle);
            }

            Gl.LinkProgram(Handle);

            foreach(var shader in shaders)
            {
                Gl.DetachShader(Handle, shader.Handle);
            }

            Gl.GetProgram(Handle, ProgramProperty.LinkStatus, out int linked);

            if(linked == 0)
            {
                var log = ReadProgramLog(Handle);

                throw new InvalidOperationException($"Shader linking failed: {log}");
            }

        }

        public void Bind()
        {
            Gl.UseProgram(Handle);
        }

        public void Unbind()
        {
            Gl.UseProgram(0);
        }

        public void Dispose()
        {
            Gl.DeleteProgram(Handle);
        }

        public uint GetAttributeLocation(string name)
        {
            int pos = Gl.GetAttribLocation(Handle, name);
            Verify.VerifyAttribute(name, pos);

            return (uint)pos;
        }

        static string ReadProgramLog(uint id)
        {
            var infolog = new StringBuilder(MAX_LOG);
            Gl.GetProgramInfoLog(id, MAX_LOG, out int infologLength, infolog);
            return infolog.ToString();
        } 
    }
}
