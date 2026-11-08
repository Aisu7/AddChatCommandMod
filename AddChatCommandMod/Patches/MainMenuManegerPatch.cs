using HarmonyLib;
using System.Collections;

namespace AddChatCommandMod;

[HarmonyPatch(typeof(MainMenuManager),nameof(MainMenuManager.RunStartUp))]
public class RunStartUpPatch
{
    public static void Postfix()
    {       
        
    }
}