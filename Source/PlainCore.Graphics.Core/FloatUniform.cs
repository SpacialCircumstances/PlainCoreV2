using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class FloatUniform : IUniform
    {
        public FloatUniform(string name)
        {
            this.name = name;
        }

        protected string name;
        public string Name => name;

        protected float value;
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
