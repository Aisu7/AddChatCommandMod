using HarmonyLib;
using InnerNet;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace AddChatCommandMod;



[HarmonyPatch(typeof(ChatController),nameof(ChatController.AddChat))]
class AddChatPatch
{
    static void Prefix(ChatController __instance, PlayerControl sourcePlayer, string chatText)
    {
        if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Joined && AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
            return;
        
        if (!AmongUsClient.Instance.AmHost)
            return;
        
        AddChatCommandManager.AddChatCommand(chatText,sourcePlayer);
    }
}


[HarmonyPatch(typeof(ChatController),nameof(ChatController.SendChat))]
class SendChatPatch
{
    public static bool Prefix(ChatController __instance)
    {
        
        if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Joined && AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
            return true;
        
        if (!AmongUsClient.Instance.AmHost)
            return true;

        string chatText = __instance.freeChatField.textArea.text;

        //翻訳試す仮の
        string kari = ModTranslations.GetString("COMMAND.NAME.MESSAGE");
        Logger.AddChatCommandModLog(Logger.Log.LogInfo,$"{kari}:ほんやく");

        return SendChatCommandManager.SendChat(chatText, __instance);
    }
}




[HarmonyPatch(typeof(ChatController),nameof(ChatController.Toggle))]
static class TogglePatch
{
    private static string? _selectedPlayerName;
    
    static void Postfix(ChatController __instance)
    {
        if (_selectedPlayerName != null)
        {
            if(string.IsNullOrWhiteSpace(__instance.freeChatField.textArea.text) || __instance.freeChatField.textArea.text.EndsWith(" "))
                __instance.freeChatField.textArea.SetText(_selectedPlayerName);
            else
            {
                __instance.freeChatField.textArea.SetText(__instance.freeChatField.textArea.text + " " + _selectedPlayerName);
            }
        }

        _selectedPlayerName = null;
    }

    public static void SetSelectedPlayerName(string? playerName)
    {
        Logger.AddChatCommandModLog(Logger.Log.LogInfo, "FieldSetter Postfix called");
        if (playerName != null)
        {
            _selectedPlayerName = playerName;
        }
        else
        {
            Logger.AddChatCommandModLog(Logger.Log.LogError,"_selectedPlayerName　が null です。}");
        }
    }
}