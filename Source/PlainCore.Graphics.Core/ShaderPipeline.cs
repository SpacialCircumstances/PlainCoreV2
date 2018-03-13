using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class ShaderPipeline
    {
        public ShaderPipeline(List<ShaderResource> shaders)
        {
            handle = Gl.CreateProgram();

            UploadShaders(shaders.ToArray());
        }

        public ShaderPipeline(params ShaderResource[] shaders)
        {
            handle = Gl.CreateProgram();

            UploadShaders(shaders);
        }

        protected uint handle;

        protected void UploadShaders(ShaderResource[] shaders)
        {
            if (!VerifyShaders(shaders)) throw new NotSupportedException("Shader resources invalid");

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

        protected bool VerifyShaders(ShaderResource[] shaders)
        {
            //TODO: Check if stages present
            return true;
        }
    }
}
