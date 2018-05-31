using System;
using System.Numerics;

namespace PlainCore.Window
{
    /// <summary>
    /// A basic interface to define a window independent from a windowing library.
    /// </summary>
    public interface IWindow: IDisposable
    {
        /// <summary>
        /// Checks if the window is open.
        /// </summary>
        /// <remarks>
        /// The render loop should exit when this is false.
        /// </remarks>
        bool IsOpen { get; }

        /// <summary>
        /// Allows setting/getting the title of the window.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Height of the window.
        /// </summary>
        uint Height { get; }

        /// <summary>
        /// Width of the window.
        /// </summary>
        uint Width { get; }

        /// <summary>
        /// Checks if the window can be resized by the user.
        /// </summary>
        bool IsResizable { get; }

        /// <summary>
        /// Checks if the window is displayed on the screen.
        /// </summary>
        /// <remarks>
        /// Not to be confused with IsOpen as it is not coupled to the OpenGL context.</remarks>
        bool IsVisible { get; }

        /// <summary>
        /// Manually close the window, killing the OpenGL context.
        /// </summary>
        void Close();

        /// <summary>
        /// Check for events and handle them.
        /// </summary>
        /// <remarks>
        /// You should probably call this each frame.</remarks>
        void PollEvents();

        /// <summary>
        /// Swaps the backbuffer with the frontbuffer, displaying the rendered graphics.
        /// </summary>
        /// <remarks>
        /// You should probably call this at the end of each frame.</remarks>
        void Display();

        /// <summary>
        /// Gets the global position in relation to the screen.
        /// </summary>
        /// <returns>The position of the window</returns>
        Vector2 Position { get; }
    }
}
