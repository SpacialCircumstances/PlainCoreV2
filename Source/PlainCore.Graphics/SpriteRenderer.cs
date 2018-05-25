using OpenGL;
using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    public class SpriteRenderer: IRenderPipelineSettings
    {
        private SpriteRenderItem[] renderItems;
        private int renderItemsIndex;
        private int renderItemsCount;

        public void SetRenderItems(SpriteRenderItem[] renderItems, int index = 0)
        {
            SetRenderItems(renderItems, index, renderItems.Length - index);
        }

        public void SetRenderItems(SpriteRenderItem[] renderItems, int index, int length)
        {
            this.renderItems = renderItems;
            this.renderItemsIndex = index;
            this.renderItemsCount = length;
            Array.Sort(this.renderItems, renderItemsIndex, renderItemsCount);
        }

        public void Render(Action<VertexPositionColorTexture[], int[], Texture> renderCallback)
        {

        }

        public uint VertexSize => VertexPositionColorTexture.Size;

        public PrimitiveType Primitive => PrimitiveType.Triangles;

        private ShaderPipeline internalShader;

        public ShaderPipeline Shader
        {
            get
            {
                return internalShader ?? (internalShader = new ShaderPipeline(
                        DefaultShader.FromType(typeof(VertexPositionColorTexture), Core.ShaderType.Vertex),
                        DefaultShader.FromType(typeof(VertexPositionColorTexture), Core.ShaderType.Fragment)));
            }
        }

        private VertexAttributeDescription[] internalVertexAttributes;

        public VertexAttributeDescription[] VertexAttributes
        {
            get
            {
                return internalVertexAttributes ?? (internalVertexAttributes = DefaultVertexDefinition.FromType(typeof(VertexPositionColorTexture)));
            }
        }
    }
}
