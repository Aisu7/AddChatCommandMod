namespace AddChatCommandMod;

public static class MessageStore
{
    public static bool IsEnabled = true;
    public static string Text = "";
}

public class MessageSend : IAddChatCommand, ISendChatCommand
{
    // Category, Description, DisplayNameはガイド側で翻訳するという流れかな。
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!m";
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

        VanillaRpc.SendPrivateChat(sourcePlayer,MessageStore.Text);

    }

    bool ISendChatCommand.ExecuteSendChat(string argumentText,ChatController __instance)
    {
        if (!MessageStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            VanillaRpc.SendLocalChat("そのコマンドは無効化されています");
            __instance.freeChatField.Clear();
            return true;
        }
        
        if (MessageStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("登録されたメッセージがありません。");
            __instance.freeChatField.Clear();
            return false;
        }
        VanillaRpc.SendLocalChat(MessageStore.Text);
        __instance.freeChatField.Clear();
        return true;
    }
}



public class MessageSet : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!m s";
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

        MessageStore.Text = argumentText;
        VanillaRpc.SendAllClientChat(MessageStore.Text);
        __instance.freeChatField.Clear();
        return false;
    }
}


public class MessageSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!m on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        MessageStore.IsEnabled = true;
        VanillaRpc.SendAllClientChat("メッセージを有効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}

public class MessageSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "!m off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatController __instance)
    {
        MessageStore.IsEnabled = false;
        VanillaRpc.SendAllClientChat("メッセージを無効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}