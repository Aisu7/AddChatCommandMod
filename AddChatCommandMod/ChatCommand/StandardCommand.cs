

namespace AddChatCommandMod;

public abstract class Template
{
    /// <summary>
    /// abstract・・・子クラスで変数を宣言しないとエラーになってくれる
    /// virtual・・・子クラスで宣言しなくてもいいやつ。
    /// 変数 {get;} 読み取り専用
    /// 変数 {get; private set;} これ書いたクラスのみ
    /// 変数 {get; protected set;} これと子クラスのみ
    /// 変数 {get; set;} どこからでも変更可能
    /// </summary>
    
    // {get}
    public abstract string ShortName {get; } //get=このクラスの中では読み取り専用の変数。別クラスで宣言(set)してもらう。
    public abstract string FullName {get;}
    public abstract string FullName_jp {get;}
    public virtual string SetText {get; protected set;} ="";
    public virtual bool IsActive {get; protected set;} = true; //書き忘れるなよ

    public virtual void IOnlySend()
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
    
    public virtual void Set(string secondpart)
    {
        if (IsActive)
        {
            if (secondpart == "")
            {
                // ($"使い方\n  {ShortName}s テキスト\n    テキストを入力してください)
            }
            else
            {
                
            }
        }
        else
        {
            
        }
    }
    public virtual void On() //書き忘れるなよ
    {
        IsActive = true;
        // Rpcでチャットを送るやつ(旧SendGlobal?の処理)[有効化]
    }
    public virtual void Off() //書き忘れるなよ
    {
        IsActive = false;
        // Rpcでチャットを送るやつ(旧SendGlobal?の処理)[無効化]
    }

    public virtual void AllClientSend() //sourcePlayerを引数としよう
    {
        if (IsActive)
        {
            if (SetText == "")
            {
                // ($"{FullName_jp}テキストは未設定です。");
            }
            else
            {
                //ホスト分岐書くけど後で
                // (SetText);
            }
        }
        else
        {
            //if (sourcePlayer = )
            //SendLocal
            // ($"そのコマンドは現在無効化されています。");
        }
    }
    public virtual void Other(){}
}