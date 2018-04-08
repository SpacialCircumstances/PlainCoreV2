namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Defines a shader uniform that has a name and can be set.
    /// </summary>
    /// <remarks>
    /// Can be shared between multiple shader pipeline, but must have the same name everywhere.
    /// </remarks>
    public interface IUniform
    {
        /// <summary>
        /// The uniforms name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Set the uniform in a shader pipeline.
        /// </summary>
        /// <param name="pipeline">The pipeline</param>
        void Set(ShaderPipeline pipeline);
    }
}
