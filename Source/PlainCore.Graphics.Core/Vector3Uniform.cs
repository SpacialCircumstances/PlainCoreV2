using OpenGL;
using System;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A shader uniform for setting a Vector3.
    /// </summary>
    public class Vector3Uniform : IUniform
    {
        /// <summary>
        /// Creates a new uniform for setting a Vector3.
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        public Vector3Uniform(string name)
        {
            this.name = name;
        }

        protected readonly string name;
        public string Name => name;

        protected Vector3 vector;

        /// <summary>
        /// Value of the vector.
        /// </summary>
        public Vector3 Vector
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
            Gl.Uniform3f(pipeline.GetUniformLocation(name), 1, ref vector);
        }
    }
}
