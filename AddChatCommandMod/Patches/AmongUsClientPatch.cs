using HarmonyLib;

namespace AddChatCommandMod;

[HarmonyPatch(typeof(AmongUsClient),nameof(AmongUsClient.CreatePlayer))]
class CreatPlayerPatch
{
    static void Postfix(AmongUsClient __instance)
    {
        
    }
}
