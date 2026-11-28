using HarmonyLib;
using BepInEx;
using System.IO;
using Il2CppSystem.Configuration;
namespace AddChatCommandMod;

[HarmonyPatch(typeof(PlayerVoteArea),nameof(PlayerVoteArea.Select))]
public class TestClass
{
    public static bool Prefix(PlayerVoteArea __instance)
    {
        if (UnityInput.Current.GetKeyDown(UnityEngine.KeyCode.LeftControl) || UnityInput.Current.GetKeyDown(UnityEngine.KeyCode.RightControl))
        {
            var selectedPlayerName = GameData.Instance.GetPlayerById(__instance.PlayerId)?.Object.Data.PlayerName;
            FieldSetterPatch.SetSelectedPlayerName(selectedPlayerName);
            return false;
        }
        return true;
    }
}
