using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.IO;
using System.Collections.Generic;

namespace AddChatCommandMod;

[BepInPlugin(GUID, ModName, Version)]
public class Main : BasePlugin
{
    const string GUID = "com.aisu7.acm";
    const string ModName = "AddChatCommandMod";
    const string Version = "0.1.0";
    
    public override void Load()
    {
        ResourceManager.SetUpResources();
        Registry.RegisterCommands();

        string dynamicName = this.GetType().Name;
        var harmony = new Harmony(GUID);
 
        harmony.PatchAll();

        int patchedCount = harmony.GetPatchedMethods().Count();
        Logger.Log.LogInfo($"[Main] Harmony適用済みメソッド数: {patchedCount}");
        foreach (var m in harmony.GetPatchedMethods())
        {
            Logger.Log.LogInfo($"{dynamicName},パッチ済み: {m.DeclaringType?.Name}.{m.Name}");
        }

        Logger.Log.LogInfo("AddChatCommanderMod Loaded!");

    }
}


public static class ResourceManager
{
    static readonly Dictionary<string, byte[]> SavedFiles = new();
    public static void SetUpResources()
    {   
        // 全ファイル名文nameへの代入が完了するまで
        foreach (string? name in typeof(Main).Assembly.GetManifestResourceNames())
        {
            Logger.AddChatCommandModLog(Logger.Log.LogInfo,$"Resource: {name}");
            if (name.Contains(".Resorces"))
            {
                using (Stream? stream = typeof(Main).Assembly.GetManifestResourceStream(name))
                {
                    if (stream == null)
                    {
                        continue;
                    }
                    int index = name.IndexOf(".Resorces");
                    string fileName = index.Substring(index,name.Length);
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        SavedFiles.Add(fileName, memoryStream.ToArray());
                    }   
                }
            }
        }
        foreach (string? name in NeedFileNames)
        {
            if (SavedFiles.TryGetValue(name,out byte[]? fileData))
                Logger.Log.LogInfo($"必要なフォルダ、{name}が取得できました。");
            else
                Logger.Log.LogInfo($"必要なフォルダ、{name}が取得できませんでした。");  
        }
            
    }

    public static byte[]? GetBytes(string name)
    {
        if (SavedFiles.TryGetValue(name, out byte[]? fileData))
            return fileData;
        else
        {
            Logger.Log.LogInfo($"必要なフォルダ、{name}が取得できませんでした。");
            return null;
        }
    }
}




[HarmonyPatch(typeof(ModManager),nameof(ModManager.LateUpdate))]
public class ShowModStamp
{
    public static void Postfix(ModManager __instance)
    {
        __instance.ShowModStamp();
    }
}
