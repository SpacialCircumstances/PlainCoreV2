using OpenGL;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class Vector4Uniform : IUniform
    {
        public Vector4Uniform(string name)
        {
            this.name = name;
        }

        protected readonly string name;
        public string Name => name;

        protected Vector4 vector;
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
            Gl.Uniform4f(pipeline.GetUniformLocation(name), 1, ref vector);
        }
    }
}
