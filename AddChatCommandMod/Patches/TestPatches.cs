/*using HarmonyLib;
using BepInEx;
using System.IO;
using Il2CppSystem.Configuration; 


namespace AddChatCommandMod;

[HarmonyPatch(typeof(PlayerVoteArea),nameof(PlayerVoteArea.Select))]
public class TestClass
{
    public static void Postfix(PlayerVoteArea  __instance)
    {
        if (UnityInput.Current.GetKey(UnityEngine.KeyCode.LeftControl) || UnityInput.Current.GetKey(UnityEngine.KeyCode.RightControl))
        {
            FieldSetterPatch.SetSelectedPlayerName(GameData.Instance.GetPlayerById(__instance.PlayerId).PlayerName);
            Logger.AddChatCommandModLog(Logger.Log.LogInfo,"PlayerVoteArea Postfix　ifの処理完了起動");
        }
    }
}*/
