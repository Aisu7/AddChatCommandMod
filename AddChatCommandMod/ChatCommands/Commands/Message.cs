using System.Linq;


namespace AddChatCommandMod;

public class MessageCommand : StanderdCommand
{
    public override string ShortCmdName => "m";
    public override string FullCmdName => "message";
    public StanderdCommand StandardCommand {get;set;}
    public TypeSendandSet typeSendandSet {get;set;}
    public TypeState typeState {get;set;}
    public TypeOnandOff typeOnandOdf {get;set;}
    
}
