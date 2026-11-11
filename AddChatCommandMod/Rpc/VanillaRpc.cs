using InnerNet;
using AmongUs;
using AmongUs.Data.Player;
using RewiredConsts;
using System.Linq;

namespace AddChatCommandMod;

public static class VanillaRpc
{
    public static void SendLocalChat(string message)
    {
    
  
    }
    public static void SendPrivateChat(string message,PlayerControl sourcePlayer)
    {
    

    }

    // 通常通りにチャットが送信できる
    public static void SendNormalChat(string message, PlayerControl sourcePlayer)
    {

    }

    public static void SendAllClientChat(string message, PlayerControl sourcePlayer)
    {
        
    }



    static PlayerControl SelectChatSender()
    {
        if (!PlayerControl.LocalPlayer.Data.IsDead)
        {
            return PlayerControl.LocalPlayer;
        }
        else
        {
            PlayerControl player;
            player = PlayerControl.AllPlayerControls.ToArray().Where(x x)
            return ;

        }
    
    }

}