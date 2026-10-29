using System.Linq;

namespace AddChatCommandMod;

public class ChatCommandClip
{
    public bool EnabledChatComd {get; protected set;} = true;

    public readonly MessageCommand messageCommand = new MessageCommand();
    public readonly JoinCommand joinCommand = new JoinCommand();
    public readonly AnounceCommand anouncecommand = new AnounceCommand();
    
}
