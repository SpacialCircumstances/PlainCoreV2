using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Class containing default vertex attribute descriptions.
    /// </summary>
    /// <seealso cref="DefaultShader"/>
    public static class DefaultVertexDefinition
    {
        /// <summary>
        /// Gets the attribute descriptions from the vertex type.
        /// </summary>
        /// <param name="type">Type of the vertex</param>
        /// <returns>An array of attribute descriptions</returns>
        public static VertexAttributeDescription[] FromType(Type type)
        {
            return FromName(type.Name);
        }

        /// <summary>
        /// Gets the attribute descriptions from the name of the vertex type.
        /// </summary>
        /// <param name="name">Name of the vertex type</param>
        /// <returns>An array of attribute descriptions</returns>
        public static VertexAttributeDescription[] FromName(string name)
        {
            switch(name)
            {
                case "VertexPositionColor":
                    return new VertexAttributeDescription[] {
                        GetPositionAttribute((int)VertexPositionColor.Size, 0),
                        GetColorAttribute((int)VertexPositionColor.Size, 8)
                    };

                case "VertexPosition3Color":
                    return new VertexAttributeDescription[] {
                        GetPosition3Attribute((int)VertexPosition3Color.Size, 0),
                        GetColorAttribute((int)VertexPosition3Color.Size, 12)
                    };

                case "VertexPositionTexture":
                    return new VertexAttributeDescription[] {
                        GetPositionAttribute((int)VertexPositionTexture.Size, 0),
                        GetTextureCoordinatesAttribute((int)VertexPositionTexture.Size, 8)
                    };

                case "VertexPosition3Texture":
                    return new VertexAttributeDescription[] {
                        GetPositionAttribute((int)VertexPosition3Texture.Size, 0),
                        GetTextureCoordinatesAttribute((int)VertexPosition3Texture.Size, 12)
                    };

                case "VertexPositionColorTexture":
                    return new VertexAttributeDescription[]
                    {
                        GetPositionAttribute((int)VertexPositionColorTexture.Size, 0),
                        GetColorAttribute((int)VertexPositionColorTexture.Size, 8),
                        GetTextureCoordinatesAttribute((int)VertexPositionColorTexture.Size, 24)
                    };

                case "VertexPosition3ColorTexture":
                    return new VertexAttributeDescription[]
                    {
                        GetPosition3Attribute((int)VertexPosition3ColorTexture.Size, 0),
                        GetColorAttribute((int)VertexPosition3ColorTexture.Size, 12),
                        GetTextureCoordinatesAttribute((int)VertexPosition3ColorTexture.Size, 28)
                    };
            }

            return null;
        }

        /// <summary>
        /// Gets an attribute for a 2D position.
        /// </summary>
        /// <param name="vertexSize">Size of a vertex instance in memory</param>
        /// <param name="offset">Offset from memory begin for a single vertex instance</param>
        /// <returns>The description for the attribute</returns>
        public static VertexAttributeDescription GetPositionAttribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.POSITION_NAME, 2, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }

        /// <summary>
        /// Gets an attribute for a 3D position.
        /// </summary>
        /// <param name="vertexSize">Size of a vertex instance in memory</param>
        /// <param name="offset">Offset from memory begin for a single vertex instance</param>
        /// <returns>The description for the attribute</returns>
        public static VertexAttributeDescription GetPosition3Attribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.POSITION_NAME, 3, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }

        /// <summary>
        /// Gets an attribute for a color.
        /// </summary>
        /// <param name="vertexSize">Size of a vertex instance in memory</param>
        /// <param name="offset">Offset from memory begin for a single vertex instance</param>
        /// <returns>The description for the attribute</returns>
        public static VertexAttributeDescription GetColorAttribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.COLOR_NAME, 4, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }

        /// <summary>
        /// Gets an attribute for 2D texture coordinates.
        /// </summary>
        /// <param name="vertexSize">Size of a vertex instance in memory</param>
        /// <param name="offset">Offset from memory begin for a single vertex instance</param>
        /// <returns>The description for the attribute</returns>
        public static VertexAttributeDescription GetTextureCoordinatesAttribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.TEXTURE_COORDINATES_NAME, 2, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }
    }
}
