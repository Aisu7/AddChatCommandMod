namespace AddChatCommandMod;

public static class KillStore
{
    public static bool IsEnabled = true;
}

public class Kill : ISendChatCommand
{
    string IChatCommand.Category => "Other";
    string IChatCommand.CommandKey => "!kill";
    string IChatCommand.Description => "〇使い方\n!kill {player名}  ・・・指定したプレイヤーをキルします。";
    string IChatCommand.DisplayName => "キル";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        PlayerControl data = new();
        data.player
        argumentText
        __instance.freeChatField.Clear();
        return false;
    }
}