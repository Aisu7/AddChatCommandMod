
namespace AddChatCommandMod;

public abstract class TypeSendandSet
{
    public abstract bool SendType {get; protected set;}
    public virtual string SetText {get; protected set;} = "";


    
    //HostOnlyとAllClient両方ともチャット検知の時にしても良いが、return falseを使う処理があるため、早期に分岐して、別のvoidにする必要がありそう。
    public virtual void Send(bool IsEnabled)
    {
        if (IsEnabled)
        {

        }
        
    
            if (SetText == "")
            {
             // Rpcでローカルチャットを返す処理
                // ($"{FullName_jp}テキストが未設定です。"); 
                // 個人的なメモ。　ChatRpcのほうに、システムメッセージを書き忘れないようにして。
                // また、Globalのほうは第一引数bool型でホスト死亡時も送るか、第二引数string型でテキスト。Localは常時ホスト死亡時表示なので第一引数string型のテキスト
            }
            else
            {
                // Rpcでグローバルチャットを返す処理
                // (SetText);
            }
    }
        //else
        //{
            // Rpcでローカルチャットを返す処理
            // ("そのコマンドは現在無効化されています。");
        //}

    private void SendOption(PlayerControl sourcePlayer)
    {
        if (SendType == true)
        {

        }
        VanillaRpc.SendPrivateChat()

    }

    
}
  
