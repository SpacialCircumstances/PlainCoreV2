using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlainCore.Graphics.Test
{
    public class DefaultShaderTest
    {
        [Fact]
        public void TestCompile()
        {
            var window = new OpenGLWindow(200, 200, "Test", false);

            new ShaderPipeline(
                DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Vertex),
                DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Fragment));

            new ShaderPipeline(
                DefaultShader.FromType(typeof(VertexPosition3Color), ShaderType.Vertex),
                DefaultShader.FromType(typeof(VertexPosition3Color), ShaderType.Fragment));

            new ShaderPipeline(
                DefaultShader.FromType(typeof(VertexPositionTexture), ShaderType.Vertex),
                DefaultShader.FromType(typeof(VertexPositionTexture), ShaderType.Fragment));

            new ShaderPipeline(
                DefaultShader.FromType(typeof(VertexPosition3Texture), ShaderType.Vertex),
                DefaultShader.FromType(typeof(VertexPosition3Texture), ShaderType.Fragment));

            new ShaderPipeline(
                DefaultShader.FromType(typeof(VertexPositionColorTexture), ShaderType.Vertex),
                DefaultShader.FromType(typeof(VertexPositionColorTexture), ShaderType.Fragment));

            new ShaderPipeline(
                DefaultShader.FromType(typeof(VertexPosition3ColorTexture), ShaderType.Vertex),
                DefaultShader.FromType(typeof(VertexPosition3ColorTexture), ShaderType.Fragment));

            window.Close();
            window.Dispose();
        }
    }
}
