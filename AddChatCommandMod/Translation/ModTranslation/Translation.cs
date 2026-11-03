using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace AddChatCommandMod;

public static class ModTranslations
{
    static readonly Dictionary<string,string> Translations = new();

    public static void LoadTranslations()
    {
        using (TextFieldParser parser = new(@"AddChatCommandMod/Resources/Translations/ModTranslations.csv"))
        {
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
                    Main.Log.LogWarning($"{row[0]}の翻訳が不足しています");
                    continue;
                }
                
                ModTranslations.SetTranslation(row[0],row[1]);

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
        {
            return value;
        }
        else
        {
            Main.Log.LogWarning($"[{key}]の翻訳が未設定");
            return key;
        }
    }
}
