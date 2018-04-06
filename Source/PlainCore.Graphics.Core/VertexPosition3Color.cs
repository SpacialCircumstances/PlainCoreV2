using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Vertex containing a 3D position and a color
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition3Color
    {
        public Vector3 Position;
        public Color4 Color;

        public const uint Size = Color4.Size + 12;

        /// <summary>
        /// Create a vertex with a 3D position and a color
        /// </summary>
        /// <param name="position">Position component</param>
        /// <param name="color">Color component</param>
        public VertexPosition3Color(Vector3 position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
