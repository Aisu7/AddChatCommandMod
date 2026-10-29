using System.Linq;


namespace AddChatCommandMod;

public class MessageCommand
{
    public override string ShortCmdName => "m";
    public override string FullCmdName => "message";

    public Send Send {get;} = new();
    public Set Set {get;} = new();
    public State State {get:} = new();
    public OnorOff OnorOff {get;} = new();
    
}
