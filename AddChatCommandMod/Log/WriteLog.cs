using BepInEx.Logging;
using System.Runtime.CompilerServices;

namespace AddChatCommandMod;

public static class Logger
{
    public static ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("AddChatCommandMod");

    public static void AddChatCommandModLog(Action<string> action, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
    {
        string className = Path.GetFileNameWithoutExtension(file);
        action($"[{className}.{member}:{line}] {message}");
    }


}