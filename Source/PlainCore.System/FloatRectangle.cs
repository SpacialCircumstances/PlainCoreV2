using System;
using System.Numerics;

namespace PlainCore.System
{
    /// <summary>
    /// A float rectangle.
    /// </summary>
    public class FloatRectangle: IEquatable<FloatRectangle>
    {
        /// <summary>
        /// Create a float rectangle.
        /// </summary>
        /// <param name="position">The position of the rectangle</param>
        /// <param name="size">The size of the rectangle</param>
        public FloatRectangle(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        /// <summary>
        /// Create a rectangle with the default Position & Size.
        /// </summary>
        public FloatRectangle()
        {

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

        protected Vector2 position;
        protected Vector2 size;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public Vector2 Size
        {
            get => size;
            set => size = value;
        }

        /// <summary>
        /// Gets the End of the rectangle. Is equal to Position + Size.
        /// </summary>
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
            return position == other.position && size == other.size;
        }
    }
}
