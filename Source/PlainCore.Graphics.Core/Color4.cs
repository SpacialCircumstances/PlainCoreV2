using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public struct Color4 : IEquatable<Color4>
    {
        public Color4(float r, float g, float b, float a)
        {
            vector = new Vector4(r, g, b, a);
        }

        private Vector4 vector;

        public float R => vector.X;
        public float G => vector.Y;
        public float B => vector.Z;
        public float A => vector.W;

        public const int Size = 16;

        public bool Equals(Color4 other)
        {
            return other.vector == vector;
        }

        public static Color4 White = new Color4(1f, 1f, 1f, 0f);
        public static Color4 Transparent = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Black = new Color4(0f, 0f, 0f, 0f);
        public static Color4 Red = new Color4(1f, 0f, 0f, 0f);
        public static Color4 Lime = new Color4(0f, 1f, 0f, 0f);
        public static Color4 Blue = new Color4(0f, 0f, 1f, 0f);
        public static Color4 Yellow = new Color4(1f, 1f, 0f, 0f);
        public static Color4 Cyan = new Color4(0f, 1f, 1f, 0f);
        public static Color4 Magenta = new Color4(1f, 0f, 1f, 0f);
    }
}
