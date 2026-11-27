using HarmonyLib;
using System.Collections;

namespace AddChatCommandMod;

[HarmonyPatch(typeof(MainMenuManager),nameof(MainMenuManager.Start))]
static class StartPatch
{
    static void Postfix(MainMenuManager __instance)
    {       
        
    }
}
