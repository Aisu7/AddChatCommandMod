namespace AddChatCommandMod;

public interface ISendChatCommand : IChatCommand
{
    bool ExecuteSendChat(string text);
}