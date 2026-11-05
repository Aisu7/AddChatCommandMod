using System.Collections.Generic;
using AmongUs.Data.Settings;
using Microsoft.VisualBasic.FileIO;

namespace AddChatCommandMod;

public static class ModTranslations
{
    static readonly Dictionary<string,string> Translations = new();

    public static void GetCurrentLanguage(LanguageSettingsData __instance)
    {
        byte Translationsindex = 2;
        switch (__instance.CurrentLanguage)
        {
            case SupportedLangs.Japanese :
                Translationsindex = 1;
                SetTranslations(Translationsindex);
                break;

            case SupportedLangs.English :
                Translationsindex = 2;
                SetTranslations(Translationsindex);
                break;
        }
    }

    static void SetTranslations(byte index)
    {
        using (TextFieldParser parser = new(@"AddChatCommandMod/Resources/Translations/ModTranslations.csv"))
        {
            // 毎回初期化
            Translations.Clear();

            if (parser == null)
            {
                Logger.Log.LogError("ModTranslations.csvが読み込めていません。翻訳処理を中断します");
                return;
            }

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
                
                ModTranslations.SetTranslation(row[0],row[index]);

            }
        }
    }


    // 言語設定が変わった際、辞書型Translationsの中身をリセットすることを忘れないようにする。
    public static void SetTranslation(string key, string translationText)
    {
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
