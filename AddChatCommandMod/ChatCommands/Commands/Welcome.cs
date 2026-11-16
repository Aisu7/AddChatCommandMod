namespace AddChatCommandMod;

public static class WelcomeStore
{
    public static bool IsEnabled = true;
    public static string Text = "";
}

public class WelcomeSend : IAddChatCommand, ISendChatCommand
{
    // Category, Description, DisplayNameはガイド側で翻訳するという流れかな。
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#送信";

    void IAddChatCommand.ExecuteAddChat(string argumentText, PlayerControl sourcePlayer)
    {
        if (!WelcomeStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            // 各クライアントに情報は送らない
            return;
        }

        // Command側ではなく、Manager側で処理
        /*if (sourcePlayer == null)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogWelcome,"sourcePlayer == nullです");
            return false;
        }*/

        if (WelcomeStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendPrivateChat(sourcePlayer,"入室メッセージはありません。");
            return;
        }

        VanillaRpc.SendPrivateChat(sourcePlayer,WelcomeStore.Text);

    }

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        if (!WelcomeStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            return true;
        }
        
        if (WelcomeStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("登録された入室メッセージがありません。");
            return false;
        }
        VanillaRpc.SendLocalChat(WelcomeStore.Text);

        return true;
    }
}



public class WelcomeSet : ISendChatCommand
{
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w s";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#設定";

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        if (!WelcomeStore.IsEnabled)
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

        WelcomeStore.Text = argumentText;
        VanillaRpc.SendAllClientChat(WelcomeStore.Text);
        return false;
    }
}


public class WelcomeSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        WelcomeStore.IsEnabled = true;
        VanillaRpc.SendAllClientChat("入室メッセージを有効化しました。");
        return false;
    }
}

public class WelcomeSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText)
    {
        WelcomeStore.IsEnabled = false;
        VanillaRpc.SendAllClientChat("入室メッセージを無効化しました。");
        return false;
    }
}