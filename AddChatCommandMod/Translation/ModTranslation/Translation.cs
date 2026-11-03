using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace AddChatCommandMod;

public class TranslationCSVReader
{
    public TextFieldParser parser = new(@"AddChatCommandMod/Resources/Translations/ModTranslations.csv");
    public void ReadTranslations()
    {
        
        parser.SetDelimiters(",");
        while (!parser.EndOfData)
        {
            if (parser.le)
            {
                
            }
            //ローカル変数valueはループ処理のたびにリセットされる
            /// <summary>
            /// ReadFields ・・・ SetDelimitersを反映して同一行内の列を分けて文字列の配列を取得
            /// ReadLine ・・・SetDelimiters関係なしに指定行の文字列を取得
            /// </summary>
            string[]? row = parser.ReadFields();
            ModTranslations.SetTranslations(row[0],row[1]);    
        }
        parser.Close();
    }
}
public static class ModTranslations
{
    
    static readonly Dictionary<string,string> Translations = new();

    
    // readonlyをつけておきたいため、設定処理はModTranslations classに担当させる
    // 言語設定が変わった際、辞書型Translationsの中身をリセットすることを忘れないようにする。
    public static void SetTranslations(string key, string translationText)
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
