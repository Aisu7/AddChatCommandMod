namespace AddChatCommandMod;

interface IAddChatCommand : IChatCommand
{
    void ExecuteAddChat(string text,PlayerControl sourcePlayer);
}
