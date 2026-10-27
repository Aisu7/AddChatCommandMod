
namespace AddXhatCommandMod;


    // public virtual string SetText {get; protected set;} ="";
　　　// これをSetの方に配置。nullかどうかも見極めたいので、初期の代入はSetに回す
public class typeSend
{

  public abstract SendChattype
  
    public void IOnlySend()
    {
        if (IsActive)
        {
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
  )
  
