using System;
using System.Numerics;

namespace PlainCore.Window
{
    public interface IWindow: IDisposable
    {
        bool IsOpen { get; }
        string Title { get; set; }
        uint Height { get; }
        uint Width { get; }
        bool IsResizable { get; }
        bool IsVisible { get; }
        void Close();
        void PollEvents();
        void Display();
        Vector2 GetPosition();
    }
}
