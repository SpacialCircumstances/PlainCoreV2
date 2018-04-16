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
       
        public void SetCursorMode(CursorMode mode)
        {
            if (mode == CursorMode.Normal)
            {
                GLFW.SetInputMode(Handle, GLFW.CURSOR, GLFW.CURSOR_NORMAL);
            }
            else if (mode == CursorMode.Hidden)
            {
                GLFW.SetInputMode(Handle, GLFW.CURSOR, GLFW.CURSOR_HIDDEN);
            }
            else if (mode == CursorMode.Disabled)
            {
                GLFW.SetInputMode(Handle, GLFW.CURSOR, GLFW.CURSOR_DISABLED);
            }
        }

        public string Clipboard
        {
            get
            {
                return GLFW.GetClipboardString(Handle);
            }
            set
            {
                GLFW.SetClipboardString(Handle, value);
            }
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

#pragma warning disable RCS1163 // Unused parameter.
            GLFW.SetWindowCloseCallback(Handle, new GLFW.WindowCloseFun(ptr => OnClosed?.Invoke()));

            GLFW.SetWindowFocusCallback(Handle, new GLFW.WindowFocusFun((ptr, i) => OnFocusChanged?.Invoke((Focus)i)));

            GLFW.SetWindowPosCallback(Handle, new GLFW.WindowPosFun((ptr, x, y) => OnPositionChanged?.Invoke(x, y)));

            GLFW.SetWindowSizeCallback(Handle, new GLFW.WindowSizeFun((ptr, w, h) => OnSizeChanged?.Invoke(w, h)));

            OnSizeChanged += (w, h) =>
            {
                this.width = (uint)w;
                this.height = (uint)h;
            };

            GLFW.SetCharCallback(Handle, new GLFW.CharFun((ptr, c) => OnTextEntered?.Invoke((char)c)));

            GLFW.SetCursorEnterCallback(Handle, new GLFW.CursorEnterFun((ptr, t) => OnCursorOnWindowChanged?.Invoke(t == 1)));

            GLFW.SetCursorPosCallback(Handle, new GLFW.CursorPosFun((ptr, x, y) => OnMouseMoved?.Invoke(x, y)));

            GLFW.SetFramebufferSizeCallback(Handle, new GLFW.FramebufferSizeFun((ptr, x, y) => OnFramebufferResized?.Invoke(x, y)));

            GLFW.SetJoystickCallback(new GLFW.JoystickFun((i, j) => OnJoystickEventReceived?.Invoke(i, j)));

            GLFW.SetKeyCallback(Handle, new GLFW.KeyFun((ptr, i, j, k, l) => OnKeyEventReceived?.Invoke((Key)i, j, (KeyAction)k, (Mod)l)));

            GLFW.SetMonitorCallback(new GLFW.MonitorFun((ptr, i) => OnMonitorEventReceived?.Invoke(ptr, i)));

            GLFW.SetMouseButtonCallback(Handle, new GLFW.MouseButtonFun((ptr, i, j, k) => OnMouseButtonEventReceived?.Invoke((MouseButton)i, (MouseButtonAction)j, (Mod)k)));

            GLFW.SetScrollCallback(Handle, new GLFW.ScrollFun((ptr, x, y) => OnScrolled?.Invoke(x, y)));

#pragma warning restore RCS1163 // Unused parameter.
        }

        public void Dispose()
        {
            GLFW.Terminate();
        }

        protected void Error(int i, string error)
        {
            throw new Exception($"GLFW error {i}: {error}");
        }

        /// <summary>
        /// Called when the window closes.
        /// </summary>
        public Action OnClosed;

        /// <summary>
        /// Called when the window is now in focus or out of focus.
        /// </summary>
        public Action<Focus> OnFocusChanged;

        /// <summary>
        /// Called with (x, y) when the position of the window on the screen changes.
        /// </summary>
        public Action<int, int> OnPositionChanged;

        /// <summary>
        /// Called with (w, h) when the size of the window changes.
        /// </summary>
        public Action<int, int> OnSizeChanged;

        /// <summary>
        /// Called when text input is received.
        /// </summary>
        public Action<char> OnTextEntered;

        /// <summary>
        /// Called when cursor enters (true) or leaves window.
        /// </summary>
        public Action<bool> OnCursorOnWindowChanged;

        /// <summary>
        /// Called when the mouse moves.
        /// </summary>
        public Action<double, double> OnMouseMoved;

        /// <summary>
        /// Called when the framebuffer object is resized.
        /// </summary>
        /// <remarks>This does not mean that the window was resized.</remarks>
        public Action<int, int> OnFramebufferResized;

        /// <summary>
        /// Called when a joystick connects or disconnects.
        /// </summary>
        public Action<int, int> OnJoystickEventReceived;

        /// <summary>
        /// Called with (key, scancode, action, mods) when a key event occurs.
        /// </summary>
        public Action<Key, int, KeyAction, Mod> OnKeyEventReceived;

        /// <summary>
        /// Called when a monitor was connected or disconnected.
        /// </summary>
        public Action<IntPtr, int> OnMonitorEventReceived;

        /// <summary>
        /// Called with (button, action, mods) when a mouse button is pressed/released.
        /// </summary>
        public Action<MouseButton, MouseButtonAction, Mod> OnMouseButtonEventReceived;

        /// <summary>
        /// Called with (x, y) when the scroll wheel is used.
        /// </summary>
        public Action<double, double> OnScrolled;
    }
}
