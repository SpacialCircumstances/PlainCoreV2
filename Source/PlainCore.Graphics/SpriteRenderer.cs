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
            SetRenderItems(renderItems, index, renderItems.Length);
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

        public ShaderPipeline Shader => null;

        public VertexAttributeDescription[] VertexAttributes => null;
    }
}
