using OpenGL;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class Vector3Uniform : IUniform
    {
        public Vector3Uniform(string name)
        {

        }

        protected string name;
        public string Name => name;

        protected Vector3 vector;
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
            Gl.Uniform3f(pipeline.GetUniformLocation(name), 1, ref vector);
        }
    }
}
