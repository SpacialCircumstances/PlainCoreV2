using System.IO;
using System.Reflection;

namespace PlainCore.System
{
    /// <summary>
    /// Class for global settings
    /// </summary>
    public static class PlainCoreSettings
    {
        /// <summary>
        /// Configures the root path for searching for the native GLFW libraries
        /// </summary>
        public static string GlfwSearchPath { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
