using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.System
{
    public class FloatRectangle: IEquatable<FloatRectangle>
    {
        public FloatRectangle(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        public FloatRectangle()
        {

        }

        public FloatRectangle(float x, float y, float w, float h): this(new Vector2(x, y), new Vector2(w, h))
        {

        }

        protected Vector2 position;
        protected Vector2 size;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public Vector2 Size
        {
            get => size;
            set => size = value;
        }

        public Vector2 End
        {
            get => Position + Size;
        }

        public bool Equals(FloatRectangle other)
        {
            return position == other.position && size == other.size;
        }
    }
}
