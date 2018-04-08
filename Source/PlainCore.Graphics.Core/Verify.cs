using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Internal class for verifying correctness.
    /// </summary>
    internal static class Verify
    {
        /// <summary>
        /// Verifies that the resource was created by checking for a correct handle
        /// </summary>
        /// <param name="handle">Resource handle</param>
        public static void VerifyResourceCreated(uint handle)
        {
            if (handle <= 0)
            {
                var err = Gl.GetError();
                throw new InvalidOperationException($"OpenGL resource creation failed: {err}");
            }
        }

        /// <summary>
        /// Verifies that the shaders create a valid pipeline
        /// </summary>
        /// <param name="shaders">Shaders</param>
        public static void VerifyShaders(ShaderResource[] shaders)
        {
            bool hasVertex = false;
            bool hasFragment = false;

            foreach (var shader in shaders)
            {
                if (shader.Type == ShaderType.Fragment)
                {
                    hasFragment = true;
                }
                else if (shader.Type == ShaderType.Vertex)
                {
                    hasVertex = true;
                }
            }

            if (!hasVertex || !hasFragment)
            {
                throw new InvalidOperationException("Shader pipeline not valid: Does not contain vertex shader or fragment shader");
            }
        }

        /// <summary>
        /// Verifies that a shader attribute was found in a shader
        /// </summary>
        /// <param name="attribute">The attributes name</param>
        /// <param name="position">The position in the shader</param>
        public static void VerifyAttribute(string attribute, int position)
        {
            if (position == -1)
            {
                var error = Gl.GetError();
                throw new InvalidOperationException($"Attribute {attribute} not found in shader pipeline: {error}");
            }
        }

        /// <summary>
        /// Verifies that a shader uniform was found in a shader
        /// </summary>
        /// <param name="name">The uniforms name</param>
        /// <param name="position">The position in the shader</param>
        public static void VerifyUniform(string name, int position)
        {
            if (position == -1)
            {
                var error = Gl.GetError();
                throw new InvalidOperationException($"Uniform {name} not found in shader pipeline: {error}");
            }
        }
    }
}
