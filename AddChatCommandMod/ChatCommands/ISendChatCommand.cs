namespace AddChatCommandMod;

public interface ISendChatCommand : IChatCommand
{
    bool ExecuteSendChat(string text, ChatController __instance);
}