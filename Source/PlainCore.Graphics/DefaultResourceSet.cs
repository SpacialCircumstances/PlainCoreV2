using System;
using System.Collections.Generic;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public class DefaultResourceSet : IResourceSet
    {
        public DefaultResourceSet(IRenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        public IRenderTarget RenderTarget { get; }
        public List<IUniform> Uniforms { get; set; } = new List<IUniform>();
        private readonly Matrix4fUniform worldMatrixUniform = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);

        public virtual IEnumerable<IUniform> GetUniforms()
        {
            if (RenderTarget == null) throw new ArgumentNullException("RenderTarget");
            worldMatrixUniform.Matrix = RenderTarget.WorldMatrix;

            yield return worldMatrixUniform;

            foreach (var uniform in Uniforms)
            {
                yield return uniform;
            }
        }
    }
}
