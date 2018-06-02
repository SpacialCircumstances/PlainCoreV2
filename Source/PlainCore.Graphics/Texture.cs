using System;
using PlainCore.Graphics.Core;
using PlainCore.System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A texture that can be drawn.
    /// </summary>
    public class Texture : ITexture, IUniform, IDisposable
    {
        /// <summary>
        /// Load the texture from an image file.
        /// </summary>
        /// <param name="filename">Filename of the image</param>
        /// <param name="repeated">Texture is tileable</param>
        /// <returns>The texture</returns>
        public static Texture FromFile(string filename, bool repeated = false)
        {
            var img = Image.Load(filename);
            return FromImage(img, repeated);
        }

        /// <summary>
        /// Create the texture from an image in memory.
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="repeated">Texture is tileable</param>
        /// <returns>The texture</returns>
        public static Texture FromImage(Image<Rgba32> image, bool repeated = false)
        {
            int h = image.Height;
            int w = image.Width;
            var data = image.SavePixelData();
            return new Texture(w, h, data, repeated);
        }

        /// <summary>
        /// Wraps an OpenGL texture.
        /// </summary>
        /// <param name="texture">The OpenGL texture</param>
        /// <returns>The texture</returns>
        public static Texture FromDeviceTexture(DeviceTexture texture)
        {
            return new Texture
            {
                deviceTexture = texture
            };
        }

        /// <summary>
        /// Creates a texture from a byte array.
        /// </summary>
        /// <param name="data">The byte data</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="repeated">Texture is tileable</param>
        /// <returns>The texture</returns>
        public static Texture FromMemory(byte[] data, int width, int height, bool repeated = false)
        {
            return new Texture(width, height, data, repeated);
        }

        protected Texture(int width, int height, byte[] data, bool repeated = false)
        {
            deviceTexture = new DeviceTexture(DefaultShader.DEFFAULT_TEXTURE_UNIFORM_NAME, width, height, true, repeated);
            deviceTexture.Bind();
            deviceTexture.CopyData(data);
        }

        protected Texture()
        {

        }

        protected DeviceTexture deviceTexture;

        protected FloatRectangle rectangle = new FloatRectangle(0, 0, 1, 1);

        public FloatRectangle Rectangle => rectangle;
        public int Width => deviceTexture.Width;
        public int Height => deviceTexture.Height;
        public DeviceTexture InternalTexture => deviceTexture;

        Texture ITexture.Texture => this;

        public string Name => deviceTexture.Name;

        public void Set(ShaderPipeline pipeline)
        {
            deviceTexture.Bind();
            deviceTexture.Set(pipeline);
            deviceTexture.Unbind();
        }

        public void Dispose()
        {
            deviceTexture.Dispose();
        }
    }
}
