using PlainCore.System;
using System;
using System.Collections.Generic;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Contains the data of a texture atlas.
    /// </summary>
    public class TextureAtlasData
    {
        private readonly Dictionary<string, FloatRectangle> regions;

        public TextureAtlasData(string imageName, Dictionary<string, FloatRectangle> regions)
        {
            ImageName = imageName;
            this.regions = regions ?? throw new ArgumentNullException(nameof(regions));
        }

        public FloatRectangle GetRegion(string name)
        {
            var found = regions.TryGetValue(name, out var region);
            if (!found)
            {
                throw new ArgumentException($"Region {name} not found in texture atlas");
            }

            return region;
        }

        public string ImageName { get; }
    }
}
