using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public static class DefaultVertexDefinition
    {
        public static VertexAttributeDescription[] FromType(Type type)
        {
            return FromName(type.Name);
        }

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

        public static VertexAttributeDescription GetPositionAttribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.POSITION_NAME, 2, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }

        public static VertexAttributeDescription GetPosition3Attribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.POSITION_NAME, 3, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }

        public static VertexAttributeDescription GetColorAttribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.COLOR_NAME, 4, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }

        public static VertexAttributeDescription GetTextureCoordinatesAttribute(int vertexSize, int offset)
        {
            return new VertexAttributeDescription(DefaultShader.TEXTURE_COORDINATES_NAME, 2, OpenGL.VertexAttribType.Float, false, vertexSize, offset);
        }
    }
}
