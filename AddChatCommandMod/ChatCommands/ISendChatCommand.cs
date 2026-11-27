namespace AddChatCommandMod;

interface ISendChatCommand : IChatCommand
{
    bool ExecuteSendChat(string text, ChatController __instance);
}
