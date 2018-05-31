using System.IO;
using System.Reflection;

namespace PlainCore.System
{
    public static class PlainCoreSettings
    {
        public static string GlfwSearchPath { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
