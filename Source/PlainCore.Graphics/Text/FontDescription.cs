using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.IO;

namespace PlainCore.Graphics.Text
{
    public class FontDescription
    {
        public static FontDescription FromDescriptionFile(string filename)
        {
            string content = File.ReadAllText(filename);
            var fontDescription = JsonConvert.DeserializeObject<FontDescription>(content);
            fontDescription.LoadImage();
            return fontDescription;
        }

        public FontDescription(Image<Rgba32> image, uint fontSize, Dictionary<char, GlyphLayout> glyphs)
        {
            Bitmap = image;
            FontSize = fontSize;
            this.glyphs = glyphs;
        }

        public FontDescription(string imageFile, uint fontSize, Dictionary<char, GlyphLayout> glyphs)
        {
            ImageFile = imageFile;
            LoadImage();
            FontSize = fontSize;
            this.glyphs = glyphs;
        }

        public string ImageFile { get; private set; }

        [JsonIgnore]
        public Image<Rgba32> Bitmap { get; private set; }

        public uint FontSize { get; private set; }

        protected Dictionary<char, GlyphLayout> glyphs;

        public GlyphLayout GetGlyph(char c)
        {
            return glyphs[c];
        }

        public void Save(string fileName, string imageFileName)
        {
            ImageFile = imageFileName;
            Bitmap.Save(imageFileName);
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(fileName, json);
        }

        protected void LoadImage()
        {
            Bitmap = Image.Load(ImageFile);
        }
    }
}
