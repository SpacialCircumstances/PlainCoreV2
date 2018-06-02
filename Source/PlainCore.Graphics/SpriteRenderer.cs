using OpenGL;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Renderer for rendering sprites.
    /// </summary>
    public class SpriteRenderer: IRenderPipelineSettings
    {
        private SpriteRenderItem[] renderItems;
        private int renderItemsIndex;
        private int renderItemsCount;

        /// <summary>
        /// Sets the current sprite items for rendering.
        /// </summary>
        /// <param name="renderItems">Sprite items</param>
        /// <param name="sortMode">Sort mode for sprites</param>
        /// <param name="index">Start index of items array</param>
        public void SetRenderItems(SpriteRenderItem[] renderItems, SpriteSortMode sortMode = SpriteSortMode.TextureLayer, int index = 0)
        {
            SetRenderItems(renderItems, sortMode, index, renderItems.Length - index);
        }

        /// <summary>
        /// Sets the current sprite items for rendering.
        /// </summary>
        /// <param name="renderItems">Sprite items</param>
        /// <param name="sortMode">Sort mode for sprites</param>
        /// <param name="index">Start index of items array</param>
        /// <param name="length">Length of sprite array part</param>
        public void SetRenderItems(SpriteRenderItem[] renderItems, SpriteSortMode sortMode, int index, int length)
        {
            if (renderItems == null) throw new ArgumentNullException(nameof(renderItems));
            if (index > renderItems.Length) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + length > renderItems.Length) throw new ArgumentException();

            this.renderItems = renderItems;
            this.renderItemsIndex = index;
            this.renderItemsCount = length;
            Array.Sort(this.renderItems, renderItemsIndex, renderItemsCount, GetComparer(sortMode));
        }

        /// <summary>
        /// Applies the batching and creates vertex data for the sprites.
        /// </summary>
        /// <param name="renderCallback">Function called with (vertex data, element count, texture) for each batch to draw</param>
        public void RenderToData(Action<VertexPositionColorTexture[], int, Texture> renderCallback)
        {
            int index = renderItemsIndex;
            int count = renderItemsCount;

            unsafe
            {
                Texture texture = null;
                int currentBatchCount = 0; //Number of SRIs in current batch
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

                            renderCallback.Invoke(vertexArray, vertexArray.Length + (vertexArray.Length / 2), texture);

                            currentBatchCount = 0;
                            batchStart = i;
                        }

                        texture = item.Texture;
                    }

                    currentBatchCount++;
                }

                //Flush
                VertexPositionColorTexture[] vertexArray2 = Flush(currentBatchCount, batchStart);

                renderCallback.Invoke(vertexArray2, vertexArray2.Length + (vertexArray2.Length / 2), texture);
            }
        }

        protected VertexPositionColorTexture[] Flush(int currentBatchCount, int batchStart)
        {
            VertexPositionColorTexture[] vertexArray = new VertexPositionColorTexture[currentBatchCount * 4];

            for (int j = 0; j < currentBatchCount; j++)
            {
                var currentItem = renderItems[j + batchStart];
                int vertexIndex = j * 4;
                vertexArray[vertexIndex] = currentItem.LT;
                vertexArray[vertexIndex + 1] = currentItem.RT;
                vertexArray[vertexIndex + 2] = currentItem.RD;
                vertexArray[vertexIndex + 3] = currentItem.LD;
            }

            return vertexArray;
        }

        /// <summary>
        /// Creates an indices array for rendering sprites.
        /// </summary>
        /// <param name="count">Amount of sprites to render</param>
        /// <returns>Indices array</returns>
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

        public void Dispose()
        {
            internalShader?.Dispose();
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

        private IComparer<SpriteRenderItem> GetComparer(SpriteSortMode mode)
        {
            switch (mode)
            {
                case SpriteSortMode.TextureLayer:
                    return new TextureLayerComparer();
                case SpriteSortMode.None:
                    return new NoOperationComparer();
                case SpriteSortMode.LayerFirst:
                    return new LayerFirstComparer();
                default:
                    throw new NotSupportedException();
            }
        }

        private class TextureLayerComparer : IComparer<SpriteRenderItem>
        {
            public int Compare(SpriteRenderItem x, SpriteRenderItem y)
            {
                int xKey = ((int)x.Texture.InternalTexture.Handle * 512) + x.Layer;
                int yKey = ((int)y.Texture.InternalTexture.Handle * 512) + y.Layer;

                return xKey - yKey;
            }
        }

        private class NoOperationComparer : IComparer<SpriteRenderItem>
        {
            public int Compare(SpriteRenderItem x, SpriteRenderItem y)
            {
                return 0;
            }
        }

        private class LayerFirstComparer : IComparer<SpriteRenderItem>
        {
            public int Compare(SpriteRenderItem x, SpriteRenderItem y)
            {
                int xKey = (x.Layer * 512) + (int)x.Texture.InternalTexture.Handle;
                int yKey = (y.Layer * 512) + (int)y.Texture.InternalTexture.Handle;

                return xKey - yKey;
            }
        }
    }
}
