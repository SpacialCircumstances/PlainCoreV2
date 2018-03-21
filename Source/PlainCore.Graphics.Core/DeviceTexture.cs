using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class DeviceTexture : IBindable, IUniform
    {
        public DeviceTexture(string name, int width, int height, bool genMipmaps = true)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.genMipmaps = genMipmaps;
            Handle = Gl.GenTexture();
            Verify.VerifyResourceCreated(Handle);
        }

        protected readonly string name;
        protected readonly int width;
        protected readonly int height;
        protected readonly bool genMipmaps;

        public readonly uint Handle;
        public string Name => name;

        public void Bind()
        {
            Gl.BindTexture(TextureTarget.Texture2d, Handle);
        }

        public void CopyData(byte[] data)
        {
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            if(genMipmaps)
            {
                Gl.GenerateMipmap(TextureTarget.Texture2d);
            }
        }

        public void Dispose()
        {
            Gl.DeleteTextures(Handle);
        }

        public void Set(ShaderPipeline pipeline)
        {
            Gl.ActiveTexture(TextureUnit.Texture0);
            Bind();
            Gl.Uniform1(pipeline.GetUniformLocation(name), 0);
            Unbind();
        }

        public void Unbind()
        {
            //Somehow its not a good idea to unbind a texture...
        }
    }
}
