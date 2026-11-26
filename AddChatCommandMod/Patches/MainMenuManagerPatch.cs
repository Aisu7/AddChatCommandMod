using HarmonyLib;
using System.Collections;

namespace AddChatCommandMod;

[HarmonyPatch(typeof(MainMenuManager),nameof(MainMenuManager.Start))]
public static class StartPatch
{
    public static void Postfix(MainMenuManager __instance)
    {       
        
    }
}