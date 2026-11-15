using System.Reflection;
using System.Linq;
using Il2CppSystem.CodeDom;


namespace AddChatCommandMod;

static class Registry
{
    public static void ResisterCommands()
    {
        //  現在実行しているコードが入っているアセンブリ(dllなど)を取得する
        var assembly = Assembly.GetExecutingAssembly();

        var commandType = assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract && !type.ContainsGenericParameters && (typeof(IAddChatCommand).IsAssignableFrom(type) || typeof(ISendChatCommand).IsAssignableFrom(type)));

        foreach (Type type in commandType)
        {
            object? instance = Activator.CreateInstance(type);
            if (instance == null)
            {
                
                continue;
            }

            if (instance is IAddChatCommand addChatCommand)
            {
                AddChatCommandManager.Commands.Add(addChatCommand.CommandKey,addChatCommand);    
            }
            if (instance is ISendChatCommand sendChatCommand)
            {
                SendChatCommandManager.Commands.Add(sendChatCommand.CommandKey,sendChatCommand);
            }


        
        }

        

    }
}

public static class AddChatCommandManager
{
    public static Dictionary<string,IAddChatCommand> Commands = new();

    public static void AddChatCommand(string chatMessage,PlayerControl player)
    {
        string[] token = chatMessage.Split(" ");

        for (int i ; token.Length ;--i)
        {
            string chattext = "!";
            if (Commands.ContainsKey(text1))
            {
                if (Commands.TryGetValue(chatMessage,out IAddChatCommand? value))
                {
                    value.ExecuteAddChat(chatMessage,player);
                }
                else
                {
                
                }
            }   
        }
    }

}

public static class SendChatCommandManager
{

    public static Dictionary<string,ISendChatCommand> Commands = new();

    public static bool SendChat(string chatText,PlayerControl player)
    {
        if (Commands.ContainsKey(chatText))
        {
            if (Commands.TryGetValue(chatText,out ISendChatCommand? value))
            {
                return value.ExecuteSendChat(chatText);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

}