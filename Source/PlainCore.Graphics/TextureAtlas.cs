using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Keeps track of texture regions. Can be loaded.
    /// </summary>
    /// <remarks>Useful for tilemaps etc.</remarks>
    public class TextureAtlas
    {
        public static TextureAtlas LoadFromFile(string atlasFile)
        {
            string json = File.ReadAllText(atlasFile);
            var atlas = JsonConvert.DeserializeObject<TextureAtlasData>(json);
            return new TextureAtlas(atlas);
        }

        private readonly TextureAtlasData atlasData;
        private readonly Texture texture;

        public TextureAtlas(TextureAtlasData data)
        {
            atlasData = data;
            texture = Texture.FromFile(data.ImageName);
        }

        public TextureAtlas(TextureAtlasData data, Image<Rgba32> replacement): this(data, Texture.FromImage(replacement))
        {
        }

        public TextureAtlas(TextureAtlasData data, Texture replacementTexture)
        {
            atlasData = data;
            texture = replacementTexture;
        }

        public TextureRegion GetRegion(string name)
        {
            return new TextureRegion(texture, atlasData.GetRegion(name));
        }

        public TextureRegion this[string name]
        {
            get => GetRegion(name);
        }
    }
}
