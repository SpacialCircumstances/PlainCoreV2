using PlainCore.Graphics.Text;
using SixLabors.ImageSharp;

namespace Fonts
{
    public class FontExample
    {
        public void Run()
        {
            var description = new FontGenerator().GenerateFont("OpenSans-Regular.ttf", 40);
            description.Bitmap.Save("Font.png");
        }
    }
}
