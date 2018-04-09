using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Class containing default shader implementations.
    /// </summary>
    public static class DefaultShader
    {
        /// <summary>
        /// Get a shader from the type of the vertex used for rendering.
        /// </summary>
        /// <param name="type">Type of the vertex</param>
        /// <param name="stage">Stage of the shader</param>
        /// <returns></returns>
        public static ShaderResource FromType(Type type, ShaderType stage)
        {
            return FromName(type.Name, stage);
        }

        /// <summary>
        /// Get a shader from the name of the vertex type used for rendering.
        /// </summary>
        /// <param name="name">Name of the vertex type</param>
        /// <param name="stage">Stage of the shader</param>
        /// <returns></returns>
        public static ShaderResource FromName(string name, ShaderType stage)
        {
            switch (name)
            {
                case "VertexPositionTexture":
                    if(stage == ShaderType.Fragment)
                    {
                        return new ShaderResource(stage, POSITION_TEXTURE_FRAGMENT);
                    }
                    else if(stage == ShaderType.Vertex)
                    {
                        return new ShaderResource(stage, POSITION_TEXTURE_VERTEX);
                    }
                    break;

                case "VertexPositionColor":
                    if(stage == ShaderType.Fragment)
                    {
                        return new ShaderResource(stage, POSITION_COLOR_FRAGMENT);
                    }
                    else if(stage == ShaderType.Vertex)
                    {
                        return new ShaderResource(stage, POSITION_COLOR_VERTEX);
                    }
                    break;

                case "VertexPositionColorTexture":
                    if(stage == ShaderType.Fragment)
                    {
                        return new ShaderResource(stage, POSITION_COLOR_TEXTURE_FRAGMENT);
                    }
                    else if (stage == ShaderType.Vertex)
                    {
                        return new ShaderResource(stage, POSITION_COLOR_TEXTURE_VERTEX);
                    }
                    break;

                case "VertexPosition3Texture":
                    if (stage == ShaderType.Fragment)
                    {
                        return new ShaderResource(stage, POSITION_TEXTURE_FRAGMENT);
                    }
                    else if (stage == ShaderType.Vertex)
                    {
                        return new ShaderResource(stage, POSITION3_TEXTURE_VERTEX);
                    }
                    break;

                case "VertexPosition3Color":
                    if (stage == ShaderType.Fragment)
                    {
                        return new ShaderResource(stage, POSITION_COLOR_FRAGMENT);
                    }
                    else if (stage == ShaderType.Vertex)
                    {
                        return new ShaderResource(stage, POSITION3_COLOR_VERTEX);
                    }
                    break;

                case "VertexPosition3ColorTexture":
                    if (stage == ShaderType.Fragment)
                    {
                        return new ShaderResource(stage, POSITION_COLOR_TEXTURE_FRAGMENT);
                    }
                    else if (stage == ShaderType.Vertex)
                    {
                        return new ShaderResource(stage, POSITION3_COLOR_TEXTURE_VERTEX);
                    }
                    break;
            }

            return null;
        }

        /// <summary>
        /// Name of default position attribute in the vertex shader.
        /// </summary>
        public const string POSITION_NAME = "Position";

        /// <summary>
        /// Name of default color attribute in the vertex shader.
        /// </summary>
        public const string COLOR_NAME = "Color";

        /// <summary>
        /// Name of default texture coordinates attribute in the vertex shader.
        /// </summary>
        public const string TEXTURE_COORDINATES_NAME = "TextureCoordinates";

        /// <summary>
        /// Name of the default world matrix uniform.
        /// </summary>
        public const string MVP_UNIFORM_NAME = "uMVP";

        /// <summary>
        /// Name of the default 2D texture uniform.
        /// </summary>
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
        private static readonly string V_OUT_TEXCOORDS = $"out vec2 {FRAGMENT_TEXTURE_COORDINATES_NAME};";
        private static readonly string F_IN_COLOR = $"in vec4 {FRAGMENT_COLOR_NAME};";
        private static readonly string F_IN_TEXCOORDS = $"in vec2 {FRAGMENT_TEXTURE_COORDINATES_NAME};";
        private static readonly string F_OUT_COLOR = $"out vec4 {OUT_COLOR_NAME};";
        private static readonly string U_MVP = $"uniform mat4 {MVP_UNIFORM_NAME};";
        private static readonly string U_DEFAULT_TEXTURE = $"uniform sampler2D {DEFFAULT_TEXTURE_UNIFORM_NAME};";
        private const string MAIN_FUNCTION = "void main() {";
        private const string CLOSE_BRACE = "}";
        private static readonly string V_ASSIGN_POSITION = $"gl_Position = {MVP_UNIFORM_NAME} * vec4({POSITION_NAME}, 0.0, 1.0);";
        private static readonly string V_ASSIGN_POSITION3 = $"gl_Position = {MVP_UNIFORM_NAME} * vec4({POSITION_NAME}, 1.0);";
        private static readonly string V_ASSIGN_COLOR = $"{FRAGMENT_COLOR_NAME} = {COLOR_NAME};";
        private static readonly string V_ASSIGN_TEXCOORDS = $"{FRAGMENT_TEXTURE_COORDINATES_NAME} = {TEXTURE_COORDINATES_NAME};";
        private static readonly string F_ASSIGN_COLOR = $"{OUT_COLOR_NAME} = {FRAGMENT_COLOR_NAME};";
        private static readonly string F_TEXTURE_GET = $"texture({DEFFAULT_TEXTURE_UNIFORM_NAME}, {FRAGMENT_TEXTURE_COORDINATES_NAME})";
        private static readonly string F_ASSIGN_TEXTURE = $"{OUT_COLOR_NAME} = {F_TEXTURE_GET};";
        private static readonly string F_ASSIGN_TEXTURE_COLOR = $"{OUT_COLOR_NAME} = {F_TEXTURE_GET} * {FRAGMENT_COLOR_NAME};";

        private static readonly string[] POSITION_TEXTURE_VERTEX = $@"
{GLSL_VERSION}
{V_IN_POSITION}
{V_IN_TEXCOORDS}
{U_MVP}
{V_OUT_TEXCOORDS}
{MAIN_FUNCTION}
{V_ASSIGN_POSITION}
{V_ASSIGN_TEXCOORDS}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION_TEXTURE_FRAGMENT = $@"
{GLSL_VERSION}
{F_IN_TEXCOORDS}
{U_DEFAULT_TEXTURE}
{F_OUT_COLOR}
{MAIN_FUNCTION}
{F_ASSIGN_TEXTURE}
{CLOSE_BRACE}
".Split('\n');

        private static readonly string[] POSITION3_TEXTURE_VERTEX = $@"
{GLSL_VERSION}
{V_IN_POSITION3}
{V_IN_TEXCOORDS}
{U_MVP}
{V_OUT_TEXCOORDS}
{MAIN_FUNCTION}
{V_ASSIGN_POSITION3}
{V_ASSIGN_TEXCOORDS}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION_COLOR_VERTEX = $@"
 {GLSL_VERSION}
{V_IN_POSITION}
{V_IN_COLOR}
{U_MVP}
{V_OUT_COLOR}
{MAIN_FUNCTION}
{V_ASSIGN_COLOR}
{V_ASSIGN_POSITION}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION_COLOR_FRAGMENT = $@"
 {GLSL_VERSION}
{F_IN_COLOR}
{F_OUT_COLOR}
{MAIN_FUNCTION}
{F_ASSIGN_COLOR}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION3_COLOR_VERTEX = $@"
 {GLSL_VERSION}
{V_IN_POSITION3}
{V_IN_COLOR}
{U_MVP}
{V_OUT_COLOR}
{MAIN_FUNCTION}
{V_ASSIGN_COLOR}
{V_ASSIGN_POSITION3}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION_COLOR_TEXTURE_VERTEX = $@"
 {GLSL_VERSION}
{V_IN_POSITION}
{V_IN_COLOR}
{V_IN_TEXCOORDS}
{U_MVP}
{V_OUT_COLOR}
{V_OUT_TEXCOORDS}
{MAIN_FUNCTION}
{V_ASSIGN_COLOR}
{V_ASSIGN_TEXCOORDS}
{V_ASSIGN_POSITION}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION_COLOR_TEXTURE_FRAGMENT = $@"
{GLSL_VERSION}
{F_IN_COLOR}
{F_IN_TEXCOORDS}
{U_DEFAULT_TEXTURE}
{F_OUT_COLOR}
{MAIN_FUNCTION}
{F_ASSIGN_TEXTURE_COLOR}
{CLOSE_BRACE}
            ".Split('\n');

        private static readonly string[] POSITION3_COLOR_TEXTURE_VERTEX = $@"
 {GLSL_VERSION}
{V_IN_POSITION3}
{V_IN_COLOR}
{V_IN_TEXCOORDS}
{U_MVP}
{V_OUT_COLOR}
{V_OUT_TEXCOORDS}
{MAIN_FUNCTION}
{V_ASSIGN_COLOR}
{V_ASSIGN_TEXCOORDS}
{V_ASSIGN_POSITION3}
{CLOSE_BRACE}
            ".Split('\n');
    }
}