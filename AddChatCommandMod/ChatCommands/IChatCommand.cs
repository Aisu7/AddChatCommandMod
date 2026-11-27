namespace AddChatCommandMod;

interface IChatCommand
{
    string Category{get;}
    string CommandKey {get;}
    string DisplayName {get;}
    string Description {get;}
}
