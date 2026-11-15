namespace AddChatCommandMod;

public interface IAddChatCommand : IChatCommand
{
    void ExecuteAddChat(string text,PlayerControl sourcePlayer);
}