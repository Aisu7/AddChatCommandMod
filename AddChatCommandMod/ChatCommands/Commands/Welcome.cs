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

        if (WelcomeStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendPrivateChat(sourcePlayer,"入室メッセージはありません。");
            return;
        }

        VanillaRpc.SendPrivateChat(sourcePlayer,WelcomeStore.Text);

    }

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatControl __instance )
    {
        if (!WelcomeStore.IsEnabled)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"IsEnable == falseです");
            __instance.freeChatField.Clear();
            return true;
        }
        
        if (WelcomeStore.Text == "")
        {
            Logger.AddChatCommandModLog(Logger.Log.LogMessage,"Text == \"\" です");
            VanillaRpc.SendLocalChat("登録された入室メッセージがありません。");
            __instance.freeChatField.Clear();
            return false;
        }
        VanillaRpc.SendLocalChat(WelcomeStore.Text);
        __instance.freeChatField.Clear();
        return true;
    }
}



public class WelcomeSet : ISendChatCommand
{
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w s";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#設定";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatControl __instance)
    {
        if (!WelcomeStore.IsEnabled)
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

        WelcomeStore.Text = argumentText;
        VanillaRpc.SendAllClientChat(WelcomeStore.Text);
        __instance.freeChatField.Clear();
        return false;
    }
}


public class WelcomeSwitchOn : ISendChatCommand
{
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w on";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#有効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatControl __instance])
    {
        WelcomeStore.IsEnabled = true;
        VanillaRpc.SendAllClientChat("入室メッセージを有効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}

public class WelcomeSwitchOff : ISendChatCommand
{
    string IChatCommand.Category => "Welcome";
    string IChatCommand.CommandKey => "!w off";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "#無効化";

    bool ISendChatCommand.ExecuteSendChat(string argumentText, ChatControl __instance)
    {
        WelcomeStore.IsEnabled = false;
        VanillaRpc.SendAllClientChat("入室メッセージを無効化しました。");
        __instance.freeChatField.Clear();
        return false;
    }
}