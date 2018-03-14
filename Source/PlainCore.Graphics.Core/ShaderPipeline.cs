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
            handle = Gl.CreateProgram();
            Verify.VerifyResourceCreated(handle);
            UploadShaders(shaders.ToArray());
        }

        public ShaderPipeline(params ShaderResource[] shaders)
        {
            handle = Gl.CreateProgram();
            Verify.VerifyResourceCreated(handle);
            UploadShaders(shaders);
        }

        protected uint handle;

        protected void UploadShaders(ShaderResource[] shaders)
        {
            Verify.VerifyShaders(shaders);

            foreach(var shader in shaders)
            {
                Gl.AttachShader(handle, shader.Handle);
            }

            Gl.LinkProgram(handle);

            foreach(var shader in shaders)
            {
                Gl.DetachShader(handle, shader.Handle);
            }

            Gl.GetProgram(handle, ProgramProperty.LinkStatus, out int linked);

            if(linked == 0)
            {
                var log = ReadProgramLog(handle);

                throw new InvalidOperationException($"Shader linking failed: {log}");
            }

        }

        public void Bind()
        {
            Gl.UseProgram(handle);
        }

        public void Unbind()
        {
            Gl.UseProgram(0);
        }

        public void Dispose()
        {
            Gl.DeleteProgram(handle);
        }

        static string ReadProgramLog(uint id)
        {
            var infolog = new StringBuilder(MAX_LOG);
            Gl.GetProgramInfoLog(id, MAX_LOG, out int infologLength, infolog);
            return infolog.ToString();
        } 
    }
}
