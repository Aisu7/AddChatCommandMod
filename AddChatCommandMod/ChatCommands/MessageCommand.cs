using System.Linq;


namespace AddChatCommandMod;

public class MessageCommand
{
    public override string ShortCmdName => "m";
    public override string FullCmdName => "message";

    public TypeSendandSet typeSendandSet {get;} = new();
    public TypeState typeState {get:} = new();
    public TypeOnandOff typeOnandOdf {get;} = new();
    
}
