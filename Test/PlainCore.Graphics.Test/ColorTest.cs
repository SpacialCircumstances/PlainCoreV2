using PlainCore.Graphics.Core;
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
            Assert.Equal(col1.R, col2.R, 2);
            Assert.Equal(col1.G, col2.G, 2);
            Assert.Equal(col1.B, col2.B, 2);
            Assert.Equal(col1.A, col2.A, 2);
        }
    }
}
