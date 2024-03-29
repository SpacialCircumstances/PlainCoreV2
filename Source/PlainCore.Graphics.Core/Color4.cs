﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A color. Internal a Vector4.
    /// </summary>
    public struct Color4 : IEquatable<Color4>
    {
        public Color4(float r, float g, float b, float a = 1f)
        {
            vector = new Vector4(r, g, b, a);
        }

        public Color4(byte r, byte g, byte b, byte a = 255)
        {
            vector = new Vector4(r / 256f, g / 256f, b / 256f, a / 256f);
        }

        public Color4(Vector4 vector)
        {
            this.vector = vector;
        }

        private Vector4 vector;

        public float R => vector.X;
        public float G => vector.Y;
        public float B => vector.Z;
        public float A => vector.W;

        public const int Size = 16;

        public bool Equals(Color4 other)
        {
            return other.vector.Equals(vector);
        }

        public static readonly Color4 White = new Color4(1f, 1f, 1f);
        public static readonly Color4 Transparent = new Color4(1f, 1f, 1f, 0f);
        public static readonly Color4 Black = new Color4(0f, 0f, 0f);
        public static readonly Color4 Red = new Color4(1f, 0f, 0f);
        public static readonly Color4 Lime = new Color4(0f, 1f, 0f);
        public static readonly Color4 Blue = new Color4(0f, 0f, 1f);
        public static readonly Color4 Yellow = new Color4(1f, 1f, 0f);
        public static readonly Color4 Cyan = new Color4(0f, 1f, 1f);
        public static readonly Color4 Magenta = new Color4(1f, 0f, 1f);
        public static readonly Color4 Gray = new Color4(0.5f, 0.5f, 0.5f);
        public static readonly Color4 Olive = new Color4(0.5f, 0.5f, 0f);
        public static readonly Color4 Maroon = new Color4(0.5f, 0f, 0f);
        public static readonly Color4 Green = new Color4(0f, 0.5f, 0f);
        public static readonly Color4 Purple = new Color4(0.5f, 0f, 0.5f);
        public static readonly Color4 Teal = new Color4(0f, 0.5f, 0.5f);
        public static readonly Color4 Navy = new Color4(0f, 0f, 0.5f);
        public static readonly Color4 CornflowerBlue = new Color4(0.3921f, 0.5843f, 0.9294f);

        public static Color4 operator *(Color4 color, float scalar)
        {
            return new Color4(color.R * scalar, color.G * scalar, color.B * scalar, color.A * scalar);
        }

        public static Color4 operator /(Color4 color, float scalar)
        {
            return new Color4(color.R / scalar, color.G / scalar, color.B / scalar, color.A / scalar);
        }
    }
}
