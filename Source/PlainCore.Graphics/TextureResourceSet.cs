using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public class TextureResourceSet: DefaultResourceSet
    {
        public TextureResourceSet(IRenderTarget renderTarget) : base(renderTarget)
        {
        }

        public Texture Texture { get; set; }

        public override IEnumerable<IUniform> GetUniforms()
        {
            yield return Texture;

            foreach(var uniform in base.GetUniforms())
            {
                yield return uniform;
            }
        }
    }
}
