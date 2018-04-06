using OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class Framebuffer: IBindable
    {
        public static Framebuffer GetDefault()
        {
            return new Framebuffer(0, true);
        }

        protected Framebuffer(uint handle, bool isDefault = false)
        {
            this.isDefault = isDefault;
            Handle = handle;
        }

        public Framebuffer()
        {
            Handle = Gl.GenFramebuffer();
            Verify.VerifyResourceCreated(Handle);
        }

        public readonly uint Handle;
        protected readonly bool isDefault;

        public void Bind()
        {
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public void Dispose()
        {
            //Can not dispose default framebuffer because it is owned by the context
            if (!isDefault)
            {
                Gl.DeleteFramebuffers(Handle);
            }
        }

        public void AttachTexture(DeviceTexture texture)
        {
            texture.Bind();
            Gl.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, texture.Handle, 0);
            texture.Unbind();
        }

        public void AttachDepthBuffer(DepthBuffer depthBuffer)
        {
            depthBuffer.Bind();
            Gl.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthBuffer.Handle);
            depthBuffer.Unbind();
        }

        public void Unbind()
        {
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Use()
        {
            Gl.DrawBuffers(Gl.COLOR_ATTACHMENT0);
        }

        public void Check()
        {
            if (Gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferStatus.FramebufferComplete)
            {
                throw new InvalidOperationException("Framebuffer not complete");
            }
        }

        public byte[] Read(int width, int height)
        {
            var size = width * height * 4;
            IntPtr data = Marshal.AllocHGlobal(size);
            var outData = new byte[size];
            Gl.ReadPixels(0, 0, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            Marshal.Copy(data, outData, 0, size);
            Marshal.FreeHGlobal(data);

            return outData;
        }

        public void Clear(Color4 c)
        {
            Gl.ClearColor(c.R, c.G, c.B, c.A);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
