using GLFWDotNet;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Window
{
    public class Window: IWindow
    {
        public Window(uint width, uint height, string title, bool resizable, ContextSettings settings)
        {
            this.width = width;
            this.height = height;
            this.resizable = resizable;
            this.title = title;
            Open(settings);
        }

        protected uint width;
        protected uint height;
        protected bool resizable;
        protected bool visible = true;
        protected string title;

        public IntPtr Handle = IntPtr.Zero;

        private bool Exists()
        {
            return GLFW.WindowShouldClose(Handle) == 0;
        }

        public bool IsOpen => Exists();

        public string Title
        {
            get => title;
            set
            {
                title = value;
                GLFW.SetWindowTitle(Handle, title);
            }
        }

        public uint Height => height;

        public uint Width => width;

        public bool IsResizable => resizable;

        public bool IsVisible => visible;

        public void Close()
        {
            GLFW.DestroyWindow(Handle);
        }

        public void Display()
        {
            GLFW.SwapBuffers(Handle);
        }

        public Vector2 GetPosition()
        {
            GLFW.GetWindowPos(Handle, out int x, out int y);
            return new Vector2(x, y);
        }

        public void PollEvents()
        {
            GLFW.PollEvents();
        }

        protected virtual void Open(ContextSettings settings)
        {
            if (GLFW.Init() == 0) throw new NotSupportedException("GLFW init failed");

            //Set hints
            GLFW.WindowHint(GLFW.CLIENT_API, GLFW.OPENGL_API);
            GLFW.WindowHint(GLFW.OPENGL_PROFILE, GLFW.OPENGL_CORE_PROFILE);
            GLFW.WindowHint(GLFW.CONTEXT_VERSION_MAJOR, 3);
            GLFW.WindowHint(GLFW.CONTEXT_VERSION_MINOR, 3);
            GLFW.WindowHint(GLFW.RESIZABLE, resizable ? 1 : 0);

            Handle = GLFW.CreateWindow((int)width, (int)height, title, IntPtr.Zero, IntPtr.Zero);
            if(Handle == IntPtr.Zero)
            {
                GLFW.Terminate();
                throw new NotSupportedException("Window creation failed");
            }

            GLFW.MakeContextCurrent(Handle);
            //TODO: Callback handling?
        }

        public void Dispose()
        {
            GLFW.Terminate();
        }
    }
}
