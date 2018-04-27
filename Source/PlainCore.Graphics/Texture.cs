using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;
using PlainCore.System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PlainCore.Graphics
{
    public class Texture : ITexture, IUniform
    {
        public static Texture FromFile(string filename, bool repeated = false)
        {
            var img = Image.Load(filename);
            return FromImage(img, repeated);
        }

        public static Texture FromImage(Image<Rgba32> image, bool repeated = false)
        {
            int h = image.Height;
            int w = image.Width;
            var data = image.SavePixelData();
            return new Texture(w, h, data, repeated);
        }

        public static Texture FromDeviceTexture(DeviceTexture texture)
        {
            return new Texture
            {
                deviceTexture = texture
            };
        }

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

        Texture ITexture.Texture => this;

        public string Name => deviceTexture.Name;

        public void Set(ShaderPipeline pipeline)
        {
            deviceTexture.Bind();
            deviceTexture.Set(pipeline);
        }
    }
}
