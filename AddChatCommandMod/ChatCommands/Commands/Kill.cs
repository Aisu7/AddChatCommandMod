namespace AddChatCommandMod;

public static class KillStore
{
    public static bool IsEnabled = true;
}

public class KillSend : ISendChatCommand
{
    string IChatCommand.Category => "Kill";
    string IChatCommand.CommandKey => "!k";
    string IChatCommand.Description => "〇使い方\n!k {player名}  ・・・指定したプレイヤーをキルします。";
    string IChatCommand.DisplayName => "#キル";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        Dictionary<string, PlayerControl> AllPlayerCheckedData = new();

        if (!KillStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            VanillaRpc.SendLocalChat("そのコマンドは無効化されています");
            __instance.freeChatField.Clear();
            return false;
        }

        for (int i = 0; i < PlayerControl.AllPlayerControls.Count; i++)
        {
            PlayerControl player = PlayerControl.AllPlayerControls[i];
            var playerName = player.Data.PlayerName;
            AllPlayerCheckedData.Add(playerName,player);
        }

        if (AllPlayerCheckedData.TryGetValue(argumentText,out PlayerControl? argumentplayer))
        {
            VanillaRpc.MurderPlayer(argumentplayer);
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
    }
}


public class KillSwitchOn : ISendChatCommand
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

public class KillSwitchOff : ISendChatCommand
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