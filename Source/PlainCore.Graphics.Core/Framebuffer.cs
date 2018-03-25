using OpenGL;
using System;
using System.Collections.Generic;
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

        public void Unbind()
        {
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
