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

        public void RenderToData(Action<VertexPositionColorTexture[], Texture> renderCallback)
        {
            int index = renderItemsIndex;
            int count = renderItemsCount;

            unsafe
            {
                Texture texture = null;
                int currentBatchCount = 0;
                int batchStart = 0;

                for (int i = index; i < count; i++)
                {
                    var item = renderItems[i];

                    if (texture != item.Texture)
                    {
                        if (currentBatchCount != 0)
                        {
                            //Flush
                            VertexPositionColorTexture[] vertexArray = Flush(currentBatchCount, batchStart);

                            renderCallback.Invoke(vertexArray, texture);

                            currentBatchCount = 0;
                            batchStart = i;
                        }

                        texture = item.Texture;
                    }

                    currentBatchCount++;
                }

                //Flush
                VertexPositionColorTexture[] vertexArray2 = Flush(currentBatchCount, batchStart);

                renderCallback.Invoke(vertexArray2, texture);
            }
        }

        protected VertexPositionColorTexture[] Flush(int currentBatchCount, int batchStart)
        {
            VertexPositionColorTexture[] vertexArray = new VertexPositionColorTexture[currentBatchCount * 4];

            for (int j = batchStart; j < currentBatchCount; j++)
            {
                var currentItem = renderItems[j];
                int vertexIndex = j * 4;
                vertexArray[vertexIndex] = currentItem.LT;
                vertexArray[vertexIndex + 1] = currentItem.RT;
                vertexArray[vertexIndex + 2] = currentItem.RD;
                vertexArray[vertexIndex + 3] = currentItem.LD;
            }

            return vertexArray;
        }

        public static int[] GetIndices(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException(nameof(count));
            }

            int[] indices = new int[count * 6];

            for (int i = 0; i < count * 4; i += 4)
            {
                int offset = i;
                int index = i + (i / 2);
                indices[index] = offset + 0;
                indices[index + 1] = offset + 1;
                indices[index + 2] = offset + 2;
                indices[index + 3] = offset + 0;
                indices[index + 4] = offset + 2;
                indices[index + 5] = offset + 3;
            }

            return indices;
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
