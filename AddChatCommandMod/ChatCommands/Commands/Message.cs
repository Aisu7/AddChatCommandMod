namespace AddChatCommandMod;

static class MessageStore
{
    public static bool IsEnabled = true;
    public static string Text = "";
}

class MessageSend : IAddChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!message";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#送信";

    void IAddChatCommand.ExecuteAddChat(string argumentText, PlayerControl sourcePlayer)
    {
        if (!MessageStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            // 各クライアントに情報は送らない
            return;
        }    

        if (MessageStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendPrivateChat(sourcePlayer,"登録されたメッセージがありません。");
            return;
        }

        if (sourcePlayer == AmongUsClient.Instance.AmHost && !sourcePlayer.Data.IsDead)
        {
           VanillaRpc.SendAllClientsChat(MessageStore.Text); 
        }
        else
        {
            VanillaRpc.SendPrivateChat(sourcePlayer,MessageStore.Text);
        }
    }
}



class MessageSet : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!message set";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#設定";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        if (!MessageStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            __instance.freeChatField.Clear();
            return false;
        }
        //　IsNullOrWhiteSpace ⊂ IsNullOrEmpty らしい。
        if (string.IsNullOrWhiteSpace(argumentText))
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("〇使い方\n!m s {設定したいテキスト}");
            __instance.freeChatField.Clear();
            return false;
        }
        if (argumentText == "Clear")
            MessageStore.Text = argumentText;
        else
           MessageStore.Text = argumentText; 
        VanillaRpc.SendAllClientsChat(MessageStore.Text);
        __instance.freeChatField.Clear();
        return false;
    }
}


class MessageSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!message on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        MessageStore.IsEnabled = true;
        VanillaRpc.SendAllClientsChat("メッセージを有効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}

class MessageSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!message off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        MessageStore.IsEnabled = false;
        VanillaRpc.SendAllClientsChat("メッセージを無効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}
