namespace AddChatCommandMod;

public static class KillStore
{
    public static bool IsEnabled = true;
}

public class Kill : ISendChatCommand
{
    string IChatCommand.Category => "Kill";
    string IChatCommand.CommandKey => "!k";
    string IChatCommand.Description => "〇使い方\n!k {player名}  ・・・指定したプレイヤーをキルします。";
    string IChatCommand.DisplayName => "#キル";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        PlayerController __instance = new()
        readonly Dictionary<string, PlayerController> AllPlayerCheckedData = new();
        foreach (string playerName in PlayerController.AllPlayerControl.Data.playerName)
        {
            playerName = __instance.Data.playerName;
            var playerId = __instance.Data.playerId;
            AllPlayerCheckedData.Add(playerName,playerId)
        }

        if (AllPlayerCheckedData.TryGetValue(argumentText,out PlayerController? playerId)
        {
            VanillaRpc.MurderPlayer(playerId);
            VanillaRpc.SendLocalChat($"{argumentText} を処刑しました。");
            __instance.freeChatField.Clear();
            return false;
        }
        else
        {
            VanillaRpc.SendLocalChat($"{argumentText} というプレイヤー名が見つかりませんでした。");
            __instance.freeChatField.Clear();
            return false;
        
        }
        __instance.freeChatField.Clear();
        return false;
    }
}


public class MessageSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "Kill";
    string IChatCommand.CommandKey => "!k on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        KillStore.IsEnabled = true;
        VanillaRpc.SendAllClientsChat("キルコマンドを有効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}

public class MessageSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "Kill";
    string IChatCommand.CommandKey => "!k off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        KillStore.IsEnabled = false;
        VanillaRpc.SendAllClientsChat("キルコマンドを無効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}