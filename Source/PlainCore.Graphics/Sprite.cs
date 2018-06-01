﻿using PlainCore.Graphics.Core;
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
            this.position = position;
            this.rotation = rotation;
            this.size = size;
        }

        public ITexture Texture { get; set; }

        protected Vector2 position = new Vector2();
        protected float rotation;
        protected Vector2 size = new Vector2();
        protected Vector2 origin = new Vector2(0.5f, 0.5f);
        protected Color4 color = Color4.White;

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public Vector2 Size { get => size; set => size = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Color4 Color { get => color; set => color = value; }
    }
}
