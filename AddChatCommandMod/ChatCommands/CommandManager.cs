using System.Collections.Generic;

namespace AddChatCommandMod;

static class Registry
{

}

public static class AddChatCommandManager
{
    public static Dictionary<string,IAddChatCommand> Commands = new();

}

public static class SendChatCommandManager
{

    public static Dictionary<string,ISendChatCommand> Commands = new();

}