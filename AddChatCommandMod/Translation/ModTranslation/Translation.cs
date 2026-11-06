using System.Collections.Generic;
using AmongUs.Data.Settings;
using Microsoft.VisualBasic.FileIO;

namespace AddChatCommandMod;

public static class ModTranslations
{
    static readonly Dictionary<string,string> Translations = new();
    static Stream? TranslationsFilePath;
    
    public static void GetFilePath(Stream stream)
    {
        TranslationsFilePath = stream;
    }

    public static void GetCurrentLanguage(LanguageSettingsData __instance)
    {
        byte Translationsindex = __instance.CurrentLanguage switch
        {
            SupportedLangs.Japanese => 1,
            SupportedLangs.English => 2,
            _ => 2,
       };
        
        SetTranslations(Translationsindex);
    }

    static void SetTranslations(byte index)
    {
        try
        {
            using (TextFieldParser parser = new(TranslationsFilePath))
            {
                // 毎回初期化
                Translations.Clear();
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //ローカル変数 row はループ処理のたびにリセットされる
                    // ReadFields ・・・ SetDelimitersを反映して同一行内の列を分けて文字列の配列を取得
                    // ReadLine ・・・SetDelimiters関係なしに指定行の文字列を取得
                    
                    string[]? row = parser.ReadFields();

                    // 「」のときと、「,」のときなど
                    if (row == null || row.Length == 0 || row[0] == "" || row[0].StartsWith("#"))
                        continue;
                    
                    if(row.Length < 3)
                    {
                        Logger.Log.LogWarning($"{row[0]}の翻訳が不足しています");
                        continue;
                    }
                    
                    SetTranslation(row[0],row[index]);

                }
            }   
        }
        catch
        {
            Logger.Log.LogError("ModTranslations.csvが読み込めていません。翻訳処理を中断します");
            return;
        }
    }


    // 言語設定が変わった際、辞書型Translationsの中身をリセットすることを忘れないようにする。
    static void SetTranslation(string key, string translationText)
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
