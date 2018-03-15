namespace PlainCore.Graphics.Core
{
    public interface IUniform
    {
        string Name { get; }
        void Set(ShaderPipeline pipeline);
    }
}
