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
