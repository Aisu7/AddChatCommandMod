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
        var writer = SelectChatSender();
        AmongUsClient.Instance.StartRpcImmediately(
        PlayerControl.Instance.LocalPlayer.PlayerId,
        ,,)   
    }
    public static void SendPrivateChat(string message,PlayerControl sourcePlayer)
    {
        var writer = SelectChatSender();
        AmongUsClient.Instance.StartRpcImmediately(
        writer.PlayerId,
        ,,);
    }

    // 通常通りにチャットが送信できる
    public static void SendNormalChat(string message, PlayerControl sourcePlayer)
    {
        AmongUsClient.Instance.StartRpcImmediately(
        sourcePlayer.PlayerId,
        ,,)
    }

    public static void SendAllClientChat(string message, PlayerControl sourcePlayer)
    {
        var writer = SelectChatSender();
        AmongUsClient.Instance.StartRpcImmediately(
        writer.PlayerId,
        ,,)
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