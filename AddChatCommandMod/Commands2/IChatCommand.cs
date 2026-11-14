

namespace AddChatCommandMod;

public interface IChatCommand
{
    string Category{get;}
    string CommandKey {get;}
    string DisplayName {get;}
    string Description {get;}
    void Execute(string text,PlayerControl sourcePlayer);

}