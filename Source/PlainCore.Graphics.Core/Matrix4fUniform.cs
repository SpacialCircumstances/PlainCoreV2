using OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class Matrix4fUniform : IUniform
    {
        public Matrix4fUniform(string name)
        {
            this.name = name;
        }

        protected readonly string name;
        public string Name => name;
        protected Matrix4x4 matrix = Matrix4x4.Identity;

        public Matrix4x4 Matrix
        {
            get => matrix;
            set { matrix = value; }
        }

        public void Set(ShaderPipeline pipeline)
        {
            Gl.ProgramUniformMatrix4f(pipeline.Handle, pipeline.GetUniformLocation(name), 1, false, ref matrix);
        }
    }
}
