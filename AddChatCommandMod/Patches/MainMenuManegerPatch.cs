using HarmonyLib;
using System.Collections;

namespace AddChatCommandMod;

[HarmonyPatch(typeof(MainMenuManager),nameof(MainMenuManager.Start))]
public class StartPatch
{
    public static void Postfix()
    {       
        
    }
}