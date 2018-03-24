using PlainCore.Graphics.Core;
using System.Numerics;
using Xunit;

namespace PlainCore.Graphics.Test
{
    public class ColorTest
    {
        [Fact]
        public void CheckCreation()
        {
            var col1 = new Color4(255, 255, 255);
            var col2 = new Color4(1f, 1f, 1f);
            var col3 = new Color4(new Vector4(1f, 1f, 1f, 1f));
            Assert.Equal(col1.R, col2.R, 2);
            Assert.Equal(col1.G, col2.G, 2);
            Assert.Equal(col1.B, col2.B, 2);
            Assert.Equal(col1.A, col2.A, 2);
            Assert.Equal(col2, col3);
        }

        [Fact]
        public void CheckOperators()
        {
            var col1 = new Color4(1f, 1f, 1f);
            var col2 = col1 / 2f;
            var col3 = new Color4(0.25f, 0.25f, 0.25f);
            var col4 = col3 * 2f;
            Assert.Equal(0.5f, col2.R);
            Assert.Equal(0.5f, col2.G);
            Assert.Equal(0.5f, col2.B);
            Assert.Equal(0.5f, col2.A);
            Assert.Equal(0.5f, col4.R);
            Assert.Equal(0.5f, col4.G);
            Assert.Equal(0.5f, col4.B);
            Assert.Equal(2f, col4.A);
        }
    }
}
