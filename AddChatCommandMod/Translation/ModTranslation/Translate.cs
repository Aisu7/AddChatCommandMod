using System.Collections.Generic;

namespace AddChatCommandMod;

public static class ModTranslation
{
    public static readonly Dictionary<string,string> Translations = new()
    {
        {"Command.Disabled","無効化されています。"},
        {"Guide.Title","AddChatCommandMod ガイド"}
    };
    public static string GetString(string key)
    {
        if (Translations.TryGetValue(key,out string? value))
        {
            return value;
        }
        else
        {
            Main.Log.LogWarning($"[{key}]の翻訳が未設定");
            return key;
        }
    }
}
