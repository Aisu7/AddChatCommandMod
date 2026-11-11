using AmongUs.Data.Settings;
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


[HarmonyPatch(typeof(LanguageSettingsData),nameof(LanguageSettingsData.OnLoadComplete))]
public class LoadCompletePatch
{
    public static void Postfix(LanguageSettingsData __instance) 
    {        
        ModTranslations.SetCurrentLanguage(__instance);
    }
}