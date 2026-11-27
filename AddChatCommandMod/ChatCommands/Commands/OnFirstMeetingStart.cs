namespace AddChatCommandMod;

static class OnFirstMeetingStartStore
{
    public static bool IsEnabled = true;
    public static string Text = "";
}

class OnFirstMeetingStartSend : IAddChatCommand, ISendChatCommand
{
    // Category, Description, DisplayNameはガイド側で翻訳するという流れかな。
    string IChatCommand.Category => "OnFirstMeetingStart";
    string IChatCommand.CommandKey => "!onfirstmeetingstart";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#送信";

    void IAddChatCommand.ExecuteAddChat(string argumentText, PlayerControl sourcePlayer)
    {
        if (!OnFirstMeetingStartStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            // 各クライアントに情報は送らない
            return;
        }

        if (OnFirstMeetingStartStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendPrivateChat(sourcePlayer,"初回会議開始時メッセージはありません。");
            return;
        }

        VanillaRpc.SendPrivateChat(sourcePlayer,OnFirstMeetingStartStore.Text);

    }

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance )
    {
        if (!OnFirstMeetingStartStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            __instance.freeChatField.Clear();
            return true;
        }
        
        if (OnFirstMeetingStartStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("登録された初回会議開始時メッセージがありません。");
            __instance.freeChatField.Clear();
            return false;
        }
        VanillaRpc.SendLocalChat(OnFirstMeetingStartStore.Text);
        __instance.freeChatField.Clear();
        return true;
    }
}



class OnFirstMeetingStartSet : ISendChatCommand
{
    string IChatCommand.Category => "OnFirstMeetingStart";
    string IChatCommand.CommandKey => "!onfirstmeetingstart set";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#設定";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        if (!OnFirstMeetingStartStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            __instance.freeChatField.Clear();
            return false;
        }
        //　IsNullOrWhiteSpace ⊂ IsNullOrEmpty らしい。
        if (string.IsNullOrWhiteSpace(argumentText))
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("Textが空です");
            __instance.freeChatField.Clear();
            return false;
        }

        OnFirstMeetingStartStore.Text = argumentText;
        VanillaRpc.SendAllClientsChat(OnFirstMeetingStartStore.Text);
        __instance.freeChatField.Clear();
        return false;
    }
}


class OnFirstMeetingStartSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "OnFirstMeetingStart";
    string IChatCommand.CommandKey => "!onfirstmeetingstart on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        OnFirstMeetingStartStore.IsEnabled = true;
        VanillaRpc.SendAllClientsChat("初回会議開始時メッセージを有効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}

class OnFirstMeetingStartSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "OnFirstMeetingStart";
    string IChatCommand.CommandKey => "!onfirstmeetingstart off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        OnFirstMeetingStartStore.IsEnabled = false;
        VanillaRpc.SendAllClientsChat("初回会議開始時メッセージを無効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}
