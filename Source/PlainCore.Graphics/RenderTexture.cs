using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using PlainCore.Graphics.Core;
using PlainCore.System;

namespace PlainCore.Graphics
{
    public class RenderTexture : ITexture, IRenderTarget
    {
        public RenderTexture(int width, int height)
        {
            view = new View(new Viewport(0, height, width, 0), new Vector2(0, 0), new Vector2(width, height));
            depthBuffer = new DepthBuffer(width, height);
            framebuffer = new Framebuffer();
            deviceTexture = new DeviceTexture(DefaultShader.DEFFAULT_TEXTURE_UNIFORM_NAME, width, height, false);
            deviceTexture.Bind();
            framebuffer.Bind();
            deviceTexture.CopyRawData(IntPtr.Zero, 0);
            framebuffer.AttachDepthBuffer(depthBuffer);
            framebuffer.AttachTexture(deviceTexture);
            deviceTexture.Unbind();
            framebuffer.Use();
            framebuffer.Check();
            texture = Texture.FromDeviceTexture(deviceTexture);
            framebuffer.Unbind();
        }

        protected DepthBuffer depthBuffer;
        protected View view;
        protected Framebuffer framebuffer;
        protected DeviceTexture deviceTexture;
        protected Texture texture;
        public Texture Texture => texture;

        public FloatRectangle Rectangle => texture.Rectangle;

        public Framebuffer Framebuffer => framebuffer;

        public Viewport Viewport => view.Viewport;

        public Matrix4x4 WorldMatrix => view.WorldMatrix;

        public void Use()
        {
            framebuffer.Bind();
            Viewport.Set();
        }

        public void Clear(Color4 color)
        {
            framebuffer.Clear(color);
        }
    }
}
