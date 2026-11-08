using AmongUs.Data.Settings;
using Microsoft.VisualBasic.FileIO;

namespace AddChatCommandMod;

public static class ModTranslations
{
    static readonly Dictionary<string, string> Translations = new();
    public static void SetCurrentLanguage(LanguageSettingsData __instance)
    {
        byte translationColumnIndex = __instance.CurrentLanguage switch
        {
            SupportedLangs.Japanese => 1,
            SupportedLangs.English => 2,
            _ => 2,
       };
        SetTranslations(translationColumnIndex);
    }

    static void SetTranslations(byte columnIndex)
    {
        byte[]? translationData = ResourceManager.GetBytes("AddChatCommandMod.Resources.Translations.ModTranslations.csv");
        Stream? translationStream = translationData switch
        {
            null => null,
            _ => new MemoryStream(translationData),
        };

        if (translationStream == null)
        {
            Logger.Log.LogError("TranslationsStreamがnullのため、翻訳を作成しませんでした。");
            return;
        }
        
        try
        { 
            // 毎回初期化
            Translations.Clear();
            using (TextFieldParser parser = new(translationStream,System.Text.Encoding.UTF8,true))
            {
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //ローカル変数 row はループ処理のたびにリセットされる
                    // ReadFields ・・・ SetDelimitersを反映して同一行内の列を分けて文字列の配列を取得
                    // ReadLine ・・・SetDelimiters関係なしに指定行の文字列を取得
                    
                    string[]? row = parser.ReadFields();

                    // 「」のときと、「,」のときなど
                    if (string.IsNullOrEmpty(row?[0]) || row.Length == 0 || row[0].StartsWith("#"))
                        continue;
                    
                    if(row.Length < 3)
                    {
                        Logger.Log.LogWarning($"{row[0]}の翻訳が不足しています");
                        continue;
                    }
                    
                    AddTranslation(row[0],row[columnIndex]);
                }
            }
        }
        catch(Exception ex)
        {
            Logger.Log.LogError($"翻訳処理中に例外{ex}が発生。");
        }
    }
            


    // 言語設定が変わった際、辞書型Translationsの中身をリセットすることを忘れないようにする。
    static void AddTranslation(string key, string translationText)
    {
        if (Translations.ContainsKey(key))
        {
            Logger.Log.LogWarning($"[{key}]の翻訳が重複しています。上書きします");
            Translations.Remove(key);
        }

        Translations.Add(key,translationText);
    }

    public static string GetString(string key)
    {
        if (Translations.TryGetValue(key,out string? value))
            return value;
        else
        {
            Logger.Log.LogWarning($"[{key}]の翻訳が未設定");
            return key;
        }
    }
}
