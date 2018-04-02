using OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class Vector2Uniform : IUniform
    {
        public Vector2Uniform(string name)
        {
            this.name = name;
        }

        protected readonly string name;
        public string Name => name;

        protected Vector2 vector;
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
            Gl.Uniform2f(pipeline.GetUniformLocation(name), 1, ref vector);
        }
    }
}
