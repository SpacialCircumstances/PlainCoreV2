using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Text
{
    public class LoadableBitmapFont
    {

        public static LoadableBitmapFont LoadFromFile(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<LoadableBitmapFont>(json);
        }

        public string ImageFilename { get; }
        public Dictionary<char, GlyphLayout> Glyphs { get; }
        public uint FontSize { get; }

        public FontDescription CreateDescription()
        {
            return new FontDescription(Image.Load(ImageFilename), Glyphs, FontSize);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
