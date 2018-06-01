using System;
using System.Numerics;

namespace PlainCore.System
{
    /// <summary>
    /// A float rectangle.
    /// </summary>
    public struct FloatRectangle: IEquatable<FloatRectangle>
    {
        /// <summary>
        /// Create a float rectangle.
        /// </summary>
        /// <param name="position">The position of the rectangle</param>
        /// <param name="size">The size of the rectangle</param>
        public FloatRectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        /// <summary>
        /// Create a float rectangle.
        /// </summary>
        /// <param name="x">The x component of the rectangle</param>
        /// <param name="y">The y component of the rectangle</param>
        /// <param name="w">The width of the rectangle</param>
        /// <param name="h">The height of the rectangle</param>
        public FloatRectangle(float x, float y, float w, float h): this(new Vector2(x, y), new Vector2(w, h))
        {

        }

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public Vector2 End
        {
            get => Position + Size;
        }

        /// <summary>
        /// Checks if two rectangles are equal.
        /// </summary>
        /// <param name="other">The other rectangle</param>
        /// <returns>The equality of both rectangles</returns>
        public bool Equals(FloatRectangle other)
        {
            return Position == other.Position && Size == other.Size;
        }
    }
}
