

using RewiredConsts;

namespace AddChatCommandMod;

public static class MessageStore
{
    public static bool IsEnable = true;
    public static string message = "";
}

public class MessageSend : IChatCommand
{
    string IChatCommand.Category => "Message";
    string IChatCommand.CommandKey => "m";
    string IChatCommand.Description => "";
    string IChatCommand.DisplayName => "固定メッセージ";
    void IChatCommand.Execute(string text, PlayerControl sourcePlayer)
    {
        if (sourcePlayer == null)
        {
            return;
        }

        if (!MessageStore.IsEnable)
        {
            Logger.AddChatCommandModLog()
            VanillaRpc.SendLocalChat("Message.csのmessageが空です");
        }

        VanillaRpc.SendPrivateChat(sourcePlayer,MessageStore.message);
    }
}