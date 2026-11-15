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
    string IChatCommand.CommandKey => "m";
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

        // Command側ではなく、Manager側で処理
        /*if (sourcePlayer == null)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"sourcePlayer == nullです");
            return false;
        }*/

        if (MessageStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendPrivateChat(sourcePlayer,"メッセージはありません。");
            return;
        }

        VanillaRpc.SendPrivateChat(sourcePlayer,MessageStore.Text);

    }

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        if (!MessageStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            return true;
        }
        
        if (MessageStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("登録されたメッセージがありません。");
            return false;
        }
        VanillaRpc.SendLocalChat(MessageStore.Text);

        return true;
    }
}



public class MessageSet : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "m s";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#設定";

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        if (!MessageStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            return false;
        }
        //　IsNullOrWhiteSpace ⊂ IsNullOrEmpty らしい。
        if (string.IsNullOrWhiteSpace(argumentText))
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("Textが空です");
            return false;
        }

        MessageStore.Text = argumentText;
        VanillaRpc.SendAllClientChat(MessageStore.Text);
        return false;
    }
}


public class MessageSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "m on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        MessageStore.IsEnabled = true;
        VanillaRpc.SendAllClientChat("メッセージを有効化しました。");
        return false;
    }
}

public class MessageSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "m off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        MessageStore.IsEnabled = false;
        VanillaRpc.SendAllClientChat("メッセージを無効化しました。");
        return false;
    }
}