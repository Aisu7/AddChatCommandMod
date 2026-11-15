namespace AddChatCommandMod;

public interface IAddChatCommand
{
    void ExecuteAddChat(string text,PlayerControl sourcePlayer);
}