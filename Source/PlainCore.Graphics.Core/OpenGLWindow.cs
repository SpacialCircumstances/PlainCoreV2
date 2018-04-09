using OpenGL;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Window using a modern OpenGL context.
    /// </summary>
    public class OpenGLWindow : Window.Window
    {
        public OpenGLWindow(uint width, uint height, string title, bool resizable) : base(width, height, title, resizable, new Window.ContextSettings(3, 3, true))
        {
        }

        static OpenGLWindow() {
            Gl.Initialize();
        }
    }
}
