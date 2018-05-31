﻿using OpenGL;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A shader uniform for setting a Vector4.
    /// </summary>
    public class Vector4Uniform : IUniform
    {
        /// <summary>
        /// Creates a new uniform for setting a Vector4.
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        public Vector4Uniform(string name)
        {
            this.name = name;
        }

        protected readonly string name;
        public string Name => name;

        protected Vector4 vector;

        /// <summary>
        /// Value of the vector.
        /// </summary>
        public Vector4 Vector
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
            Gl.Uniform4f(pipeline.GetUniformLocation(name), 1, ref vector);
        }
    }
}
