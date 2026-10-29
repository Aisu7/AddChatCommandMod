using HarmonyLib;
using InnerNet;


namespace AddChatCommandMod;


    [HarmonyPatch(typeof(ChatController),nameof(ChatController.SendChat))]
    public class SendChatPatch
    {
        
    
        public static bool Prefix(ChatController __instance)
        {
        
            if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Joined)
                return true;
        
            if (!AmongUsClient.Instance.AmHost)
                return true;

            string ChatText = __instance.freeChatField.textArea.text;
            

        

        
        
    
        }

    [HarmonyPatch(typeof(ChatController),nameof(ChatController.AddChat))]
    public class AddChatPatch
    {
        public static bool Prefix（ChatController __instance,PlayerController __instance）
            
    }
}
