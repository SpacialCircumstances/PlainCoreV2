using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;
using PlainCore.System;
using SixLabors.ImageSharp;

namespace PlainCore.Graphics
{
    public class Texture : ITexture
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

        public static Texture FromMemory(byte[] data, int width, int height, bool repeated = false)
        {
            return new Texture(width, height, data, repeated);
        }

        protected Texture(int width, int height, byte[] data, bool repeated = false)
        {
            deviceTexture = new DeviceTexture("tex", width, height, true, repeated);
            deviceTexture.Bind();
            deviceTexture.CopyData(data);
        }

        protected DeviceTexture deviceTexture;

        protected FloatRectangle rectangle = new FloatRectangle(0, 0, 1, 1);

        public FloatRectangle Rectangle => rectangle;

        Texture ITexture.Texture => this;

        public void Use(ShaderPipeline shader)
        {
            deviceTexture.Set(shader);
        }
    }
}
