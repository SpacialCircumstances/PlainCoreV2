using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A sprite, containing the texture, the color and the transformation.
    /// </summary>
    public class Sprite
    {
        public Sprite()
        {

        }

        public Sprite(ITexture tex)
        {
            Texture = tex ?? throw new ArgumentNullException(nameof(tex));
        }

        public Sprite(ITexture texture, Vector2 position, float rotation, Vector2 size) : this(texture)
        {
            Position = position;
            Rotation = rotation;
            Size = size;
        }

        public ITexture Texture { get; set; }

        public Vector2 Position { get; set; } = new Vector2();
        public float Rotation { get; set; }
        public Vector2 Size { get; set; } = new Vector2();
        public Vector2 Origin { get; set; } = new Vector2(0.5f);
        public Color4 Color { get; set; } = Color4.White;
    }
}
