using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class DeviceTexture : IDeviceBuffer<byte>, IUniform
    {
        public DeviceTexture(string name, int width, int height, bool genMipmaps = true, bool smooth = false, bool repeated = false)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.genMipmaps = genMipmaps;
            this.repeated = repeated;
            this.smooth = smooth;
            Handle = Gl.GenTexture();
            Verify.VerifyResourceCreated(Handle);
        }

        protected readonly string name;
        protected readonly int width;
        protected readonly int height;
        protected readonly bool genMipmaps;
        protected readonly bool repeated;
        protected readonly bool smooth;

        public readonly uint Handle;
        public string Name => name;
        public int Width => width;
        public int Height => height;

        public void Bind()
        {
            Gl.BindTexture(TextureTarget.Texture2d, Handle);
        }

        public void CopyData(byte[] data)
        {
            SetParameters();

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            if(genMipmaps)
            {
                Gl.GenerateMipmap(TextureTarget.Texture2d);
            }
        }

        protected void SetParameters()
        {
            if (repeated)
            {
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, Gl.REPEAT);
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, Gl.REPEAT);
            }
            else
            {
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, Gl.CLAMP_TO_EDGE);
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, Gl.CLAMP_TO_EDGE);
            }

            if (smooth)
            {
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, Gl.LINEAR);
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, Gl.LINEAR);
            }
            else
            {
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, Gl.NEAREST);
                Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, Gl.NEAREST);
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

        public void CopyRawData(byte[] data)
        {
            CopyData(data);
        }

        public void CopyRawData(IntPtr pointer, uint size)
        {
            SetParameters();

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pointer);
            if (genMipmaps)
            {
                Gl.GenerateMipmap(TextureTarget.Texture2d);
            }
        }

        public void ReplaceData(byte[] data, IntPtr offset)
        {
            throw new NotSupportedException();
        }

        public void ReplaceData(IntPtr data, uint size, IntPtr offset)
        {
            throw new NotSupportedException();
        }
    }
}
