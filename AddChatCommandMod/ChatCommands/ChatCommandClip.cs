using System.Linq;

namespace AddChatCommandMod;

public class ChatCommandClip
{
    public bool EnabledChatCmd {get; protected set;} = true;

    public readonly MessageCommand messageCommand = new();
    public readonly JoinCommand joinCommand = new();
    public readonly AnounceCommand anounceCommand = new();
    public readonly HelpCommand helpCommand = new();
    public readonly ServeyCommand serveyCommand = new();
    public readonly KillCommand killCommand = new();
}
