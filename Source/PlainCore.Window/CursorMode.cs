namespace PlainCore.Window
{
    /// <summary>
    /// Defines how the cursor should be handled when over the application window.
    /// </summary>
    public enum CursorMode
    {
        /// <summary>
        /// Show normal cursor.
        /// </summary>
        Normal,

        /// <summary>
        /// Locks the cursor to the window and hides it.
        /// </summary>
        Disabled,

        /// <summary>
        /// Hides the cursor when hovering over the window, but does not lock it.
        /// </summary>
        Hidden
    }
}
