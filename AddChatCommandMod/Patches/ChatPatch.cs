using AmongUs.Data.Settings;
using HarmonyLib;
using InnerNet;
using Rewired.UI.ControlMapper;


namespace AddChatCommandMod;


[HarmonyPatch(typeof(ChatController),nameof(ChatController.SendChat))]
public class SendChatPatch
{
    public static bool Prefix(ChatController __instance)
    {
        
        if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Joined)
            return true;
        
        if (!AmongUsClient.Instance.AmHost)
            return true;

        string ChatText = __instance.freeChatField.textArea.text;
        return true;
    }
}
        
    
[HarmonyPatch(typeof(ChatController),nameof(ChatController.AddChat))]
public class AddChatPatch
{
    public static void Prefix(ChatController __instance, PlayerControl sourceplayer)
    {
        
    }
}

[HarmonyPatch(typeof(MainMenuManager),nameof(MainMenuManager.RunStartUp))]
public class RunStartUpPatch
{
    public static void Postfix(LanguageSettingsData languageSettingsData)
    {
        //コールチンで待つ

        //languageSettingsData.add_OnLanguageChanged
        //ModTranslations.LoadTranslations();
    }
}

[HarmonyPatch(typeof(LanguageSettingsData),nameof(LanguageSettingsData.add_OnLanguageChanged))]
public class OnChangedEvent
{
    public static void Postfix(LanguageSettingsData __instance) 
    {        
        //コールチンで待つ

    }
}


