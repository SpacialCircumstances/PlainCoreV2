namespace PlainCore.Window
{
    /// <summary>
    /// Defines settings for the OpenGL context.
    /// </summary>
    public struct ContextSettings
    {
        /// <summary>
        /// Create context settings
        /// </summary>
        /// <param name="majorGLVersion">Major OpenGL version</param>
        /// <param name="minorGLVersion">Minor OpenGL version</param>
        /// <param name="coreProfile">Use OpenGL core profile</param>
        public ContextSettings(int majorGLVersion, int minorGLVersion, bool coreProfile)
        {
            MajorGLVersion = majorGLVersion;
            MinorGLVersion = minorGLVersion;
            CoreProfile = coreProfile;
        }

        /// <summary>
        /// Major OpenGL version
        /// </summary>
        public int MajorGLVersion;

        /// <summary>
        /// Minor OpenGL version
        /// </summary>
        public int MinorGLVersion;

        /// <summary>
        /// Uses OpenGL core profile
        /// </summary>
        public bool CoreProfile;
    }
}
