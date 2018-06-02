namespace PlainCore.Graphics
{
    /// <summary>
    /// Modes for sorting sprites in the sprite renderer
    /// </summary>
    public enum SpriteSortMode
    {
        /// <summary>
        /// Default. Creates batches by texture and sorts by layer inside the batches.
        /// </summary>
        TextureLayer,

        /// <summary>
        /// Performs no sort, sprites are rendered in the order they were passed. Batches are created when texture changes.
        /// </summary>
        None,

        /// <summary>
        /// Sort depth-first. May create many more batches.
        /// </summary>
        LayerFirst
    }
}
