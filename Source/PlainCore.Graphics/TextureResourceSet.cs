﻿using System;
using System.Collections.Generic;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A resource set with a single texture.
    /// </summary>
    public class TextureResourceSet: DefaultResourceSet
    {
        public TextureResourceSet(IRenderTarget renderTarget) : base(renderTarget)
        {
        }

        /// <summary>
        /// Allows setting/getting the assigned texture.
        /// </summary>
        public Texture Texture { get; set; }

        public override IEnumerable<IUniform> GetUniforms()
        {
            if (Texture == null) throw new ArgumentNullException(nameof(Texture));

            yield return Texture;

            foreach(var uniform in base.GetUniforms())
            {
                yield return uniform;
            }
        }
    }
}
