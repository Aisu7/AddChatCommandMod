using HarmonyLib;
using BepInEx;
using System.IO;
namespace AddChatCommandMod;

[HarmonyPatch(typeof(PlayerVoteArea),nameof(PlayerVoteArea.Select))]
public class TestClass
{
    public static bool Prefix(PlayerVoteArea __instance,ChatController)
    {
        if (UnityInput.Current.GetKeyDown(UnityEngine.KeyCode.LeftControl) || UnityInput.Current.GetKeyDown(UnityEngine.KeyCode.RightControl))
        {

            GameData.Instance.GetPlayerById(__instance.PlayerId)?.Object.Data.PlayerName;
            return false;
        }
        return true;
    }
}
