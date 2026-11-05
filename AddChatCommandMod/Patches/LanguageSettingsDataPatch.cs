using HarmonyLib;

namespace AddChatCommandMod;

/*[HarmonyPatch(typeof(LanguageSettingsData),nameof(LanguageSettingsData.HandleChange))]
public class HandleChangePatch
{
    public static void Postfix(LanguageSettingsData __instance) 
    {        
        ModTranslations.GetCurrentLanguage(__instance);
    }
}*/


[HarmonyPatch(typeof(LanguageSettingsData),nameof(LanguageSettingsData.LoadComplete))]
public class LoadCompletePatch
{
    public static void Postfix(LanguageSettingsData __instance) 
    {        
        ModTranslations.GetCurrentLanguage(__instance);
    }
}