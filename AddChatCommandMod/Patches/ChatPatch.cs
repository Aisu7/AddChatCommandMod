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
        
        //翻訳試す仮の
        string kari = ModTranslations.GetString(COMMAND.NAME.MESSAGE);
        Logger.Log.LogInfo($"{kari}:ほんやく");
        return true;
    }
}
        
    
[HarmonyPatch(typeof(ChatController),nameof(ChatController.AddChat))]
public class AddChatPatch
{
    public static void Postfix(ChatController __instance, PlayerControl sourcePlayer, string chatText)
    {
        
    }
}
