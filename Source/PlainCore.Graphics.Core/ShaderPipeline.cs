using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A shader pipeline containing different shader stages.
    /// </summary>
    public class ShaderPipeline: IBindable
    {
        private const int MAX_LOG = 1024;

        /// <summary>
        /// Create a shader pipeline.
        /// </summary>
        /// <param name="shaders">The shaders</param>
        public ShaderPipeline(List<ShaderResource> shaders)
        {
            if (shaders == null) throw new ArgumentNullException(nameof(shaders));
            Handle = Gl.CreateProgram();
            Verify.VerifyResourceCreated(Handle);
            UploadShaders(shaders.ToArray());
        }

        /// <summary>
        /// Create a shader pipeline.
        /// </summary>
        /// <param name="shaders">The shaders</param>
        public ShaderPipeline(params ShaderResource[] shaders)
        {
            if (shaders == null) throw new ArgumentNullException(nameof(shaders));
            Handle = Gl.CreateProgram();
            Verify.VerifyResourceCreated(Handle);
            UploadShaders(shaders);
        }

        public readonly uint Handle;
        protected Dictionary<string, int> UniformLocations = new Dictionary<string, int>();

        protected void UploadShaders(ShaderResource[] shaders)
        {
            Verify.VerifyShaders(shaders);

            foreach(var shader in shaders)
            {
                Gl.AttachShader(Handle, shader.Handle);
            }

            Gl.LinkProgram(Handle);

            foreach(var shader in shaders)
            {
                Gl.DetachShader(Handle, shader.Handle);
            }

            Gl.GetProgram(Handle, ProgramProperty.LinkStatus, out int linked);

            if(linked == 0)
            {
                var log = ReadProgramLog(Handle);

                throw new InvalidOperationException($"Shader linking failed: {log}");
            }
        }

        public void Bind()
        {
            Gl.UseProgram(Handle);
        }

        public void Unbind()
        {
            Gl.UseProgram(0);
        }

        public void Dispose()
        {
            Gl.DeleteProgram(Handle);
        }

        /// <summary>
        /// Get the location of the attribute by the name.
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <returns>Position of the attribute. -1 if not found.</returns>
        public uint GetAttributeLocation(string name)
        {
            int pos = Gl.GetAttribLocation(Handle, name);
            Verify.VerifyAttribute(name, pos);

            return (uint)pos;
        }

        /// <summary>
        /// Get the location of the uniform by the name.
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        /// <returns>Position of the uniform. -1 if not found.</returns>
        public int GetUniformLocation(string name)
        {
            if (UniformLocations.ContainsKey(name))
            {
                return UniformLocations[name];
            }
            else
            {
                int location = Gl.GetUniformLocation(Handle, name);
                Verify.VerifyUniform(name, location);
                UniformLocations.Add(name, location);
                return location;
            }
        }

        protected static string ReadProgramLog(uint id)
        {
            var infolog = new StringBuilder(MAX_LOG);
            Gl.GetProgramInfoLog(id, MAX_LOG, out int infologLength, infolog);
            return infolog.ToString();
        }
    }
}
