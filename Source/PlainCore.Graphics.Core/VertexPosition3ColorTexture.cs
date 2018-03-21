﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition3ColorTexture
    {
        public Vector3 Position;
        public Color4 Color;
        public Vector2 TextureCoordinates;

        public const uint Size = Color4.Size + 20;

        public VertexPosition3ColorTexture(Vector3 position, Color4 color, Vector2 textureCoordinates)
        {
            Position = position;
            Color = color;
            TextureCoordinates = textureCoordinates;
        }
    }
}
