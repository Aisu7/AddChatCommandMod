using InnerNet;
using AmongUs;
using AmongUs.Data.Player;
using RewiredConsts;
using System.Linq;
using AmongUs.InnerNet.GameDataMessages;
using Hazel;

namespace AddChatCommandMod;

static class VanillaRpc
{
    public static void SendLocalChat(string message)
    {
        var player = SelectChatSender();
        MessageWriter writer = 
            AmongUsClient.Instance.StartRpcImmediately(
            player.NetId,
            (byte)RpcCalls.SendChat,
            SendOption.Reliable,
            AmongUsClient.Instance.ClientId);
        writer.Write(message);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
    }
    public static void SendPrivateChat(PlayerControl readerPlayer, string message)
    {
        var player = SelectChatSender();
        MessageWriter writer =
            AmongUsClient.Instance.StartRpcImmediately(
            player.NetId,
            (byte)RpcCalls.SendChat,
            SendOption.Reliable,
            readerPlayer.OwnerId);

        writer.Write(message);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
    }

    // 通常通りにチャットが送信できる
    public static void SendNormalChat(PlayerControl sourcePlayer, string message)
    {
        MessageWriter writer =
            AmongUsClient.Instance.StartRpcImmediately(
            sourcePlayer.NetId,
            (byte)RpcCalls.SendChat,
            SendOption.Reliable);

        writer.Write(message);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
    }

    public static void SendAllClientsChat(string message)
    {
        var player = SelectChatSender();
        MessageWriter writer =
            AmongUsClient.Instance.StartRpcImmediately(
            player.NetId,
            (byte)RpcCalls.SendChat,
            SendOption.Reliable);

        writer.Write(message);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
    }



    static PlayerControl SelectChatSender()
    {
        if (!PlayerControl.LocalPlayer.Data.IsDead)
        {
            return PlayerControl.LocalPlayer;
        }
        else
        {
            PlayerControl? player;
            player = PlayerControl.AllPlayerControls.ToArray().Where(x => x != null && x.Data != null && !x.Data.IsDead).OrderBy(x => x.PlayerId).FirstOrDefault();
            if (player == null)
            {
                Logger.AddChatCommandModLog(Logger.Log.LogInfo,"SelectChatSenderで生存playerが0のため、ホストが送信をします。");
                return PlayerControl.LocalPlayer;
            }
                
            return player;
        }
    
    }

    public static void MurderPlayer(PlayerControl targetPlayer)
    {
        var player = SelectChatSender();
        if (targetPlayer.Data.IsDead)
        {
            
        }
        else
        {
            player.RpcMurderPlayer(targetPlayer,true);  
        }
    }
}
