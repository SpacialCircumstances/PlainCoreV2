using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    static class DefaultShader
    {
        public static ShaderResource FromType(Type type, ShaderType stage)
        {
            return FromName(type.Name, stage);
        }

        public static ShaderResource FromName(string name, ShaderType stage)
        {
            switch (name)
            {
                case "VertexPosition":
                    return new ShaderResource(stage, null);
                default:
                    return null;
                    break;
            }
        }

        public const string POSITION_NAME = "Position";
        public const string COLOR_NAME = "Color";
        public const string TEXTURE_COORDINATES_NAME = "TextureCoordinates";
        public const string MVP_UNIFORM_NAME = "uMVP";
        public const string DEFFAULT_TEXTURE_UNIFORM_NAME = "tex";
        private const string FRAGMENT_COLOR_NAME = "frColor";
        private const string FRAGMENT_TEXTURE_COORDINATES_NAME = "texCoords";
        private const string OUT_COLOR_NAME = "outColor";

        private const string GLSL_VERSION = "#version 330";
        private static readonly string V_IN_POSITION = $"in vec2 {POSITION_NAME};";
        private static readonly string V_IN_POSITION3 = $"in vec3 {POSITION_NAME};";
        private static readonly string V_IN_COLOR = $"in vec4 {COLOR_NAME};";
        private static readonly string V_IN_TEXCOORDS = $"in vec2 {TEXTURE_COORDINATES_NAME};";
        private static readonly string V_OUT_COLOR = $"out vec4 {FRAGMENT_COLOR_NAME};";
        private static readonly string V_OUT_TEXCOORDS = $"out vec2 {TEXTURE_COORDINATES_NAME};";
        private static readonly string F_IN_COLOR = $"in vec4 {FRAGMENT_COLOR_NAME};";
        private static readonly string F_IN_TEXCOORDS = $"in vec2 {TEXTURE_COORDINATES_NAME};";
        private static readonly string F_OUT_COLOR = $"out vec4 {OUT_COLOR_NAME};";
        private static readonly string U_MVP = $"uniform mat4 {MVP_UNIFORM_NAME};";
        private static readonly string U_DEFAULT_TEXTURE = $"uniform sampler2D {DEFFAULT_TEXTURE_UNIFORM_NAME};";
        private static readonly string MAIN_FUNCTION = "void main() {";
        private static readonly string CLOSE_BRACE = "}";
        private static readonly string V_ASSIGN_POSITION = $"gl_Position = {MVP_UNIFORM_NAME} * vec4({POSITION_NAME}, 0.0, 1.0);";
        private static readonly string V_ASSIGN_POSITION3 = $"gl_Position = {MVP_UNIFORM_NAME} * vec4({POSITION_NAME}, 1.0);";
        private static readonly string V_ASSIGN_COLOR = $"{FRAGMENT_COLOR_NAME} = {COLOR_NAME};";
        private static readonly string V_ASSIGN_TEXCOORDS = $"{FRAGMENT_TEXTURE_COORDINATES_NAME} = {TEXTURE_COORDINATES_NAME};";
        private static readonly string F_ASSIGN_COLOR = $"{OUT_COLOR_NAME} = {FRAGMENT_COLOR_NAME};";
        private static readonly string F_TEXTURE_GET = $"texture({DEFFAULT_TEXTURE_UNIFORM_NAME}, {FRAGMENT_TEXTURE_COORDINATES_NAME})";
        private static readonly string F_ASSIGN_TEXTURE = $"{OUT_COLOR_NAME} = {F_TEXTURE_GET};";
        private static readonly string F_ASSIGN_TEXTURE_COLOR = $"{OUT_COLOR_NAME} = {F_TEXTURE_GET} * {FRAGMENT_COLOR_NAME};";
    }
}
