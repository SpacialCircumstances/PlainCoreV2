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
    }
}
