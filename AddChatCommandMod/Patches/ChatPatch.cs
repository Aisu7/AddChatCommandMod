using HarmonyLib;
using InnerNet;


namespace AddChatCommandMod;

public static class a
{

    [HarmonyPatch(typeof(ChatController),nameof(ChatController.SendChat))]
    public static void Prifix(ChatController __instance)
    {
        if (!AmongUsClient.Instance.AmHost)
            return;

        
        
    }
}