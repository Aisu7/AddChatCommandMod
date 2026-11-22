using System.Reflection;
using System.Linq;

namespace AddChatCommandMod;

public static class Registry
{
    public static void RegisterCommands()
    {
        //  現在実行しているコードが入っているアセンブリ(dllなど)を取得する
        var assembly = Assembly.GetExecutingAssembly();

        var commandTypes = assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract 
            && !type.ContainsGenericParameters && (typeof(IAddChatCommand).IsAssignableFrom(type) 
            || typeof(ISendChatCommand).IsAssignableFrom(type))|| typeof(IMessageTag).IsAssignableFrom(type)));

        foreach (Type type in commandTypes)
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
                if (instance is IMessageTag messageTag)
                    AddChatCommandManager.Tags.Add(messageTag.TagKey,messageTag);
                else
                SendChatCommandManager.Commands.Add(sendChatCommand.CommandKey,sendChatCommand);
            }



        
        }

        

    }
}

public static class AddChatCommandManager
{
    public static Dictionary<string,IAddChatCommand> Commands = new();
    pulic static Dictionary<string,IMessageTag> Tahs = new();

    public static void AddChatCommand(string chatMessage,PlayerControl player)
    {
        string[] tokens = chatMessage.Split(" ");
        bool isCommand = false;
        for (int tokenCount = tokens.Length; tokenCount >= 1; tokenCount--)
        {
            // さらにfor文を書かなくても、このようにかける
            // Joinは引数2←配列を.Take(番目)まで、引数1を間に挟んで組み合わせる関数
            string commandKey = string.Join(" ", tokens.Take(tokenCount));

            if (Commands.TryGetValue(commandKey, out IAddChatCommand? command))
            {
                string argumentText = (commandKey.Length == chatMessage.Length)? chatMessage : chatMessage.Substring(commandKey.Length + 1);
                isCommand = true;
                Logger.AddChatCommandModLog(Logger.Log.LogInfo,$"\"{chatMessage}\" は、コマンド、 \"{argumentText}\" としてコマンド処理されました。");
                command.ExecuteAddChat(argumentText,player);
                break;
            }
        }
        if (!isCommand)
        {
            Logger.AddChatCommandModLog(Logger.Log.LogInfo,$"\"{chatMessage}\" は、非コマンドとして処理されました。");
        }
        
    }

}

public static class SendChatCommandManager
{

    public static Dictionary<string,ISendChatCommand> Commands = new();

    public static bool SendChat(string chatMessage, ChatController __instance)
    {
        string[] tokens = chatMessage.Split(" ");
        for (int tokenCount = tokens.Length; tokenCount >= 1; tokenCount--)
        {
            // さらにfor文を書かなくても、このようにかける
            // Joinは引数2←配列を.Take(番目)まで、引数1を間に挟んで組み合わせる関数
            string commandKey = string.Join(" ", tokens.Take(tokenCount));

            if (Commands.TryGetValue(commandKey, out ISendChatCommand? command))
            {
                string argumentText = (commandKey.Length == chatMessage.Length)? chatMessage : chatMessage.Substring(commandKey.Length + 1);
                Logger.AddChatCommandModLog(Logger.Log.LogInfo,$"\"{chatMessage}\" は、コマンド、 \"{argumentText}\" としてコマンド処理されました。");
                return command.ExecuteSendChat(argumentText,__instance);
            }
        }
        Logger.AddChatCommandModLog(Logger.Log.LogInfo,$"\"{chatMessage}\" は、非コマンドとして処理されました。");
        return true;
    }

}