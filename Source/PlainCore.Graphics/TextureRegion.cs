﻿using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.System;

namespace PlainCore.Graphics
{
    public class TextureRegion : ITexture
    {
        public TextureRegion(Texture texture, FloatRectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }

        protected readonly Texture texture;
        protected readonly FloatRectangle rectangle;

        public Texture Texture => texture;

        public FloatRectangle Rectangle => rectangle;
    }
}
