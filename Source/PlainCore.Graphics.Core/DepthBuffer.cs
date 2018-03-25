using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class DepthBuffer : IBindable
    {
        public DepthBuffer(int width, int height)
        {
            Handle = Gl.GenRenderbuffer();
            Verify.VerifyResourceCreated(Handle);
            Bind();
            Gl.RenderbufferStorage(RenderbufferTarget.Renderbuffer, InternalFormat.DepthComponent, width, height);
            Unbind();
        }

        public readonly uint Handle;

        public void Bind()
        {
            Gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
        }

        public void Dispose()
        {
            Gl.DeleteRenderbuffers(Handle);
        }

        public void Unbind()
        {
            Gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }
    }
}
