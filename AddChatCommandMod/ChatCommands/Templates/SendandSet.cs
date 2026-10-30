
namespace AddChatCommandMod;


　　　// これをSetの方に配置。nullかどうかも見極めたいので、初期の代入はSetに回す
public abstract class TypeSendandSet
{
    public abstract SendCmdType sendCmdType {get; protected set;}

    public enum  SendCmdType
    {
        HostOnly,
        AllClientSend,
    }

    public virtual string SetText {get; protected set;} = "";


    
    //HostOnlyとAllClient両方ともチャット検知の時にしても良いが、return falseを使う処理があるため、早期に分岐して、別のvoidにする必要がありそう。
    public virtual void Send(bool IsEnabled)
    {
        if (IsEnabled)
        {
            if (sendCmdType == SendCmdType.HostOnly)
            {

                
            }
            if (sendCmdType == SendCmdType.AllClientSend)
            {


                
            }
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
        else
        {
            // Rpcでローカルチャットを返す処理
            // ("そのコマンドは現在無効化されています。");
        }
}
  
