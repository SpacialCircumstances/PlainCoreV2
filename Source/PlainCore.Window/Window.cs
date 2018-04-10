using GLFWDotNet;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Window
{
    /// <summary>
    /// A window implementation using GLFW
    /// </summary>
    public class Window: IWindow
    {
        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="width">The desired width</param>
        /// <param name="height">The desired height</param>
        /// <param name="title">The desired title</param>
        /// <param name="resizable">Should the user be able to change the size</param>
        /// <param name="settings">ContextSettings for the OpenGL context</param>
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
            GLFW.SetWindowShouldClose(Handle, GLFW.TRUE);
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

            GLFW.SetErrorCallback(new GLFW.ErrorFun(Error));

            //Set hints
            GLFW.WindowHint(GLFW.CLIENT_API, GLFW.OPENGL_API);
            GLFW.WindowHint(GLFW.OPENGL_PROFILE, GLFW.OPENGL_CORE_PROFILE);
            GLFW.WindowHint(GLFW.OPENGL_FORWARD_COMPAT, GLFW.TRUE);
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

            GLFW.SetWindowCloseCallback(Handle, new GLFW.WindowCloseFun(ptr => OnClosed()));

            GLFW.SetWindowFocusCallback(Handle, new GLFW.WindowFocusFun((ptr, i) => OnFocusChanged(i)));

            GLFW.SetWindowPosCallback(Handle, new GLFW.WindowPosFun((ptr, x, y) => OnPositionChanged(x, y)));

            GLFW.SetWindowSizeCallback(Handle, new GLFW.WindowSizeFun((ptr, w, h) => OnSizeChanged(w, h)));

            OnSizeChanged += (w, h) =>
            {
                this.width = (uint)w;
                this.height = (uint)h;
            };

            GLFW.SetCharCallback(Handle, new GLFW.CharFun((ptr, c) => OnTextEntered((char)c)));

            GLFW.SetCursorEnterCallback(Handle, new GLFW.CursorEnterFun((ptr, t) => OnCursorEntered(t)));

            GLFW.SetCursorPosCallback(Handle, new GLFW.CursorPosFun((ptr, x, y) => OnMouseMoved(x, y)));

            GLFW.SetFramebufferSizeCallback(Handle, new GLFW.FramebufferSizeFun((ptr, x, y) => OnFramebufferResized(x, y)));

            GLFW.SetJoystickCallback(new GLFW.JoystickFun((i, j) => OnJoystickEventReceived(i, j)));

            GLFW.SetKeyCallback(Handle, new GLFW.KeyFun((ptr, i, j, k, l) => OnKeyEventReceived(i, j, k, l)));

            GLFW.SetMonitorCallback(new GLFW.MonitorFun((ptr, i) => OnMonitorEventReceived(ptr, i)));

            GLFW.SetMouseButtonCallback(Handle, new GLFW.MouseButtonFun((ptr, i, j, k) => OnMouseButtonEventReceived(i, j, k)));

            GLFW.SetScrollCallback(Handle, new GLFW.ScrollFun((ptr, x, y) => OnScrolled(x, y)));
        }

        public void Dispose()
        {
            GLFW.Terminate();
        }

        protected void Error(int i, string error)
        {
            throw new Exception($"GLFW error {i}: {error}");
        }

        public Action OnClosed;
        public Action<int> OnFocusChanged;
        public Action<int, int> OnPositionChanged;
        public Action<int, int> OnSizeChanged;
        public Action<char> OnTextEntered;
        public Action<int> OnCursorEntered;
        public Action<double, double> OnMouseMoved;
        public Action<int, int> OnFramebufferResized;
        public Action<int, int> OnJoystickEventReceived;
        public Action<int, int, int, int> OnKeyEventReceived;
        public Action<IntPtr, int> OnMonitorEventReceived;
        public Action<int, int, int> OnMouseButtonEventReceived;
        public Action<double, double> OnScrolled;
    }
}
