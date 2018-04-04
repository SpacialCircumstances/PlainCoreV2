using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;
using PlainCore.System;

namespace PlainCore.Graphics
{
    public class RenderTexture : ITexture
    {
        public RenderTexture(int width, int height)
        {
            viewport = new Viewport(0, width, 0, height);
            depthBuffer = new DepthBuffer(width, height);
            framebuffer = new Framebuffer();
            deviceTexture = new DeviceTexture(DefaultShader.DEFFAULT_TEXTURE_UNIFORM_NAME, width, height, false);
            deviceTexture.Bind();
            framebuffer.Bind();
            deviceTexture.CopyRawData(IntPtr.Zero, 0);
            framebuffer.AttachDepthBuffer(depthBuffer);
            framebuffer.AttachTexture(deviceTexture);
            deviceTexture.Unbind();
            framebuffer.Check();
            texture = Texture.FromDeviceTexture(deviceTexture);
        }

        protected DepthBuffer depthBuffer;
        protected Viewport viewport;
        protected Framebuffer framebuffer;
        protected DeviceTexture deviceTexture;
        protected Texture texture;
        public Texture Texture => texture;

        public FloatRectangle Rectangle => texture.Rectangle;
    }
}
