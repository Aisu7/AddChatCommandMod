namespace AddChatCommandMod;

public static class OnMeetingStartStore
{
    public static bool IsEnabled = true;
    public static string Text = "";
}

public class OnMeetingStartSend : IAddChatCommand, ISendChatCommand
{
    // Category, Description, DisplayNameはガイド側で翻訳するという流れかな。
    string IChatCommand.Category => "OnMeetingStart";
    string IChatCommand.CommandKey => "!onmeetingstart";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#送信";

    void IAddChatCommand.ExecuteAddChat(string argumentText, PlayerControl sourcePlayer)
    {
        if (!OnMeetingStartStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            // 各クライアントに情報は送らない
            return;
        }

        if (OnMeetingStartStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendPrivateChat(sourcePlayer,"会議開始時メッセージはありません。");
            return;
        }

        VanillaRpc.SendPrivateChat(sourcePlayer,OnMeetingStartStore.Text);

    }

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance )
    {
        if (!OnMeetingStartStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            __instance.freeChatField.Clear();
            return true;
        }
        
        if (OnMeetingStartStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("登録された会議開始時メッセージがありません。");
            __instance.freeChatField.Clear();
            return false;
        }
        VanillaRpc.SendLocalChat(OnMeetingStartStore.Text);
        __instance.freeChatField.Clear();
        return true;
    }
}



public class OnMeetingStartSet : ISendChatCommand
{
    string IChatCommand.Category => "OnMeetingStart";
    string IChatCommand.CommandKey => "!onmeetingstart set";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#設定";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        if (!OnMeetingStartStore.IsEnabled)
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
        if (argumentText == "Clear")
            OnMeetingStartStore.Text = argumentText;
        else
           OnMeetingStartStore.Text = argumentText; 
        VanillaRpc.SendAllClientsChat(OnMeetingStartStore.Text);
        __instance.freeChatField.Clear();
        return false;
    }
}


public class OnMeetingStartSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "OnMeetingStart";
    string IChatCommand.CommandKey => "!onmeetingstart on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        OnMeetingStartStore.IsEnabled = true;
        VanillaRpc.SendAllClientsChat("会議開始時メッセージを有効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}

public class OnMeetingStartSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "OnMeetingStart";
    string IChatCommand.CommandKey => "!onmeetingstart off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        OnMeetingStartStore.IsEnabled = false;
        VanillaRpc.SendAllClientsChat("会議開始時メッセージを無効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}
