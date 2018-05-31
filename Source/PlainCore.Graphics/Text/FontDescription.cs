using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.IO;

namespace PlainCore.Graphics.Text
{
    /// <summary>
    /// Contains the data of a generated bitmap font.
    /// </summary>
    public class FontDescription
    {
        /// <summary>
        /// Load the description from a file with a serialized FontDescription and load the bitmap.
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>The deserialized font description</returns>
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

        /// <summary>
        /// Get the glyph data for rendering the character.
        /// </summary>
        /// <param name="c">The character</param>
        /// <returns>Glyph data</returns>
        public GlyphLayout GetGlyph(char c)
        {
            return glyphs[c];
        }

        /// <summary>
        /// Save the font description and the bitmap to a file.
        /// </summary>
        /// <param name="fileName">Filename for the serialized font description</param>
        /// <param name="imageFileName">Filename for the bitmap</param>
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
