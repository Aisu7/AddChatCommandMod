using System.Linq;


namespace AddChatCommandMod;

public class MessageCommand : StanderdCommand
{
    public override string ShortCmdName => "m";
    public override string FullCmdName => "message";

    public MessageCommand()
    {
        standerdCommand = this;
    }

    public StanderdCommand standerdCommand {get;set;}
    public TypeSendandSet typeSendandSet {get;} = new();
    public TypeState typeState {get;} = new();
    public TypeOnandOff typeOnandOdf {get;} = new();
    
}
