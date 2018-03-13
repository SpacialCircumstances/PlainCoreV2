using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class ShaderPipeline: IBindable
    {
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
        }

        public void Bind()
        {
            Gl.UseProgram(handle);
        }

        public void Unbind()
        {
            //Empty, no need to unbind?
        }

        public void Dispose()
        {
            Gl.DeleteProgram(handle);
        }
    }
}
