using OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Represents a framebuffer with attachments.
    /// </summary>
    public class Framebuffer: IBindable
    {
        /// <summary>
        /// Gets the default framebuffer.
        /// </summary>
        /// <returns>The default framebuffer</returns>
        public static Framebuffer GetDefault()
        {
            return new Framebuffer(0, true);
        }

        protected Framebuffer(uint handle, bool isDefault = false)
        {
            this.isDefault = isDefault;
            Handle = handle;
        }

        /// <summary>
        /// Creates a new framebuffer.
        /// </summary>
        public Framebuffer()
        {
            Handle = Gl.GenFramebuffer();
            Verify.VerifyResourceCreated(Handle);
        }

        /// <summary>
        /// OpenGL handle.
        /// </summary>
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

        /// <summary>
        /// Attaches a texture as color attachment 0. Texture must be created and allocated. Texture must be bound.
        /// </summary>
        /// <param name="texture">The texture to attach.</param>
        public void AttachTexture(DeviceTexture texture)
        {
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            texture.Bind();
            Gl.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, texture.Handle, 0);
            texture.Unbind();
        }

        /// <summary>
        /// Attaches a depth buffer. Depth buffer must be bound.
        /// </summary>
        /// <param name="depthBuffer"></param>
        public void AttachDepthBuffer(DepthBuffer depthBuffer)
        {
            if (depthBuffer == null) throw new ArgumentNullException(nameof(depthBuffer));
            depthBuffer.Bind();
            Gl.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthBuffer.Handle);
            depthBuffer.Unbind();
        }

        public void Unbind()
        {
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        /// <summary>
        /// Use this framebuffers color target for rendering.
        /// </summary>
        public void Use()
        {
            Gl.DrawBuffers(Gl.COLOR_ATTACHMENT0);
        }

        /// <summary>
        /// Check the validity of the framebuffer. Should be called after initialization. Framebuffer must be bound.
        /// </summary>
        public void Check()
        {
            if (Gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferStatus.FramebufferComplete)
            {
                throw new InvalidOperationException("Framebuffer not complete");
            }
        }

        /// <summary>
        /// Read data from the framebuffers color target in RGBA format. Framebuffer must be bound.
        /// </summary>
        /// <param name="width">Width of the data</param>
        /// <param name="height">Height of the data</param>
        /// <returns></returns>
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

        /// <summary>
        /// Read data from framebuffer to an image. Framebuffer must be bound.
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns></returns>
        public Image<Rgba32> ReadToImage(int width, int height)
        {
            var bytes = Read(width, height);
            return Image.LoadPixelData<Rgba32>(bytes, width, height);
        }

        /// <summary>
        /// Clears the framebuffer for rendering. Framebuffer must be bound.
        /// </summary>
        /// <param name="c"></param>
        public void Clear(Color4 c)
        {
            Gl.ClearColor(c.R, c.G, c.B, c.A);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
