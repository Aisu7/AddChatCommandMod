using HarmonyLib;
using InnerNet;

namespace AddChatCommandMod;



[HarmonyPatch(typeof(ChatController),nameof(ChatController.AddChat))]
class AddChatPatch
{
    static void Prefix(ChatController __instance, PlayerControl sourcePlayer, string chatText)
    {
        if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Joined)
            return;
        
        if (!AmongUsClient.Instance.AmHost)
            return;
        
        if (chatText.StartsWith("!"))
        {
            AddChatCommandManager.AddChatCommand(chatText,sourcePlayer);
        }
    }
}


[HarmonyPatch(typeof(ChatController),nameof(ChatController.SendChat))]
class SendChatPatch
{
    static bool Prefix(ChatController __instance)
    {
        
        if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Joined)
            return true;
        
        if (!AmongUsClient.Instance.AmHost)
            return true;

        string chatText = __instance.freeChatField.textArea.text;
        
        //翻訳試す仮の
        string kari = ModTranslations.GetString("COMMAND.NAME.MESSAGE");
        Logger.Log.LogInfo($"{kari}:ほんやく");


        if (chatText.StartsWith("!"))
        {
            return SendChatCommandManager.SendChat(chatText, __instance);
        }
        return true;
    }
}
