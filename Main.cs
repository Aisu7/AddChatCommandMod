using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using BepInEx.Logging;

namespace AddChatCommandMod;

[BepInPlugin(GUID, ModName, Version)]
class Main : BasePlugin
{
    const string GUID = "com.aisu7.acm";
    const string ModName = "AddChatCommandMod";
    const string Version = "1.0.0";
    
    public override void Load()
    {
        foreach (string name in GetType().Assembly.GetManifestResourceNames())
        {
            if (name.EndsWith("ModTranslations.csv"))
            {
                using Stream? stream = GetType().Assembly.GetManifestResourceStream(name);

                if (stream != null)
                    ModTranslations.GetFilePath(stream);
                else
                    Log.LogError("ModTranslations.csvを取得できませんでした");

                break;
            }
        }

        string dynamicName = this.GetType().Name;
        var harmony = new Harmony(GUID);
        Logger.Log = base.Log;
 
        harmony.PatchAll();

        int patchedCount = harmony.GetPatchedMethods().Count();
        //Main.ACMLog($"[Main] Harmony適用済みメソッド数: {patchedCount}");
        foreach (var m in harmony.GetPatchedMethods())
        {
            Log.LogInfo($"{dynamicName},パッチ済み: {m.DeclaringType?.Name}.{m.Name}");
        }

        Log.LogInfo("AddChatCommanderMod Loaded!");
    }
}

[HarmonyPatch(typeof(ModManager),nameof(ModManager.LateUpdate))]
public class Stamp
{
    public static void Postfix(ModManager __instance)
    {
        __instance.ShowModStamp();
    }

}