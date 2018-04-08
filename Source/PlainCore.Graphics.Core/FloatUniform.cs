using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A shader uniform for setting a float.
    /// </summary>
    public class FloatUniform : IUniform
    {
        /// <summary>
        /// Creates a new uniform for setting a float.
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        public FloatUniform(string name)
        {
            this.name = name;
        }

        protected string name;
        public string Name => name;

        protected float value;

        /// <summary>
        /// The float value.
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
            }
        }

        public void Set(ShaderPipeline pipeline)
        {
            Gl.Uniform1f(pipeline.GetUniformLocation(name), 1, ref value);
        }
    }
}
