using HarmonyLib;

namespace AddChatCommandMod;

[HarmonyPatch(typeof(AmongUsClient),nameof(AmongUsClient.CreatePlayer))]
class CreatPlayerPatch
{
    public static void Postfix(AmongUsClient __instance)
    {
        
    }
}