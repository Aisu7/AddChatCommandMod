using InnerNet;
using AmongUs;
using AmongUs.Data.Player;
using RewiredConsts;
using System.Linq;
using AmongUs.InnerNet.GameDataMessages;
using Hazel;

namespace AddChatCommandMod;

public static class VanillaRpc
{
    public static void SendLocalChat(string message)
    {
        
    }
    public static void SendPrivateChat(string message,PlayerControl sourcePlayer)
    {
        var writer = SelectChatSender();
        AmongUsClient.Instance.StartRpcImmediately(
        writer.PlayerId,
        SendOption.None);
    }

    // 通常通りにチャットが送信できる
    public static void SendNormalChat(string message, PlayerControl sourcePlayer)
    {

    }

    public static void SendAllClientChat(string message, PlayerControl sourcePlayer)
    {
        SelectChatSender();
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
            player = PlayerControl.AllPlayerControls.ToArray().Where(x => x != null && x.Data != null && !x.Data.IsDead).OrderBy(x => x.PlayerId).FirstOrDefault();
            if (player == null)
            {
                Logger.AddChatCommandModLog(Logger.Log.LogInfo,"SelectChatSenderで生存playerが0のため、ホストが送信をします。");
                return PlayerControl.LocalPlayer;
            }
                
            return player;
        }
    
    }

}