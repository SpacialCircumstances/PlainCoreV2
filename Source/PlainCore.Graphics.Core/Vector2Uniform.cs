using OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A shader uniform for setting a Vector2.
    /// </summary>
    public class Vector2Uniform : IUniform
    {
        /// <summary>
        /// Creates a new uniform for setting a Vector2.
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        public Vector2Uniform(string name)
        {
            this.name = name;
        }

        protected readonly string name;
        public string Name => name;

        protected Vector2 vector;

        /// <summary>
        /// Value of the vector.
        /// </summary>
        public Vector2 Vector
        {
            get => vector;
            set
            {
                vector = value;
            }
        }

        public void Set(ShaderPipeline pipeline)
        {
            if (pipeline == null) throw new ArgumentNullException(nameof(pipeline));
            Gl.Uniform2f(pipeline.GetUniformLocation(name), 1, ref vector);
        }
    }
}
