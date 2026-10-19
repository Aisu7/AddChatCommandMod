using BepInEx;
using BepInEx.Core;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace AddChatCommandMod;

[BepInPlugin(GUID, ModName, Version)]
class Main : BasePlugin
{
    const string GUID = "com.aisu7.acm";
    const string ModName = "AddChatCommandMod";
    const string Version = "1.0.0";

    
    public override void Load()
    {
        string dynamicName = this.GetType().Name;
        var harmony = new Harmony(GUID);
        
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

class Logo
{
    [HarmonyPatch(typeof(ModManager),nameof(ModManager.LateUpdate))]
    static void Prefix()
    {
        ModManager.Instance.ShowModStamp();
    }

}