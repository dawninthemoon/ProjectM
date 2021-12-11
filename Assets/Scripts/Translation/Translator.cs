using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

public enum LanguageCode
{
    None,
    Korean,
    English,
    Japanese,
}

public partial class LocalComponent
{
    private static string FileName => "Localization";

    [JsonProperty()]
    public string key;

    [JsonProperty()]
    public string ko;

    [JsonProperty()]
    public string en;

    [JsonProperty()]
    public string ja;

    static public string GetFileName(LanguageCode code)
    {
        switch (code)
        {
            default:
            case LanguageCode.None: return FileName;
            case LanguageCode.Korean: return $"{FileName}_ko";
            case LanguageCode.English: return $"{FileName}_en";
            case LanguageCode.Japanese: return $"{FileName}_ja";
        }
    }
}

public class Translator
{
    private static LanguageCode nationalCode = 0;

    public static LanguageCode NationalCode
    {
        get { return nationalCode; }
        set { LoadTable(value); }
    }

    private static Dictionary<string, string> table;

    public static string ReturnDefault = "Arguments Error!";

    public static void Clear()
    {
        table = null;
    }

    public static void LoadTable(LanguageCode code)
    {
        var fileName = LocalComponent.GetFileName(code);
        var components = ClientDataLoader.Load<LocalComponent>(fileName);

        table = new Dictionary<string, string>();

        string fieldName = code.ToString().Substring(0, 2).ToLower();
        FieldInfo info = typeof(LocalComponent).GetField(fieldName);

        for (int i = 0; i < components.Length; ++i)
        {
            if (table.ContainsKey(components[i].key))
            {
                //Logger.Warning("Error : There is duplicated key in Localization.xlsx : {0}", components[i].key);
                continue;
            }

            string value = (string)info.GetValue(components[i]);
            if (string.IsNullOrEmpty(value) == false)
            {
                table.Add(components[i].key, (string)info.GetValue(components[i]));
            }
        }
    }

    public static string Get(string key)
    {
        return FindKey(key);
    }

    public static string Get(string key, params object[] parameters)
    {
        string format = FindKey(key);
        if (format == null)
            return null;

        string ret = ReturnDefault;
        try
        {
            ret = string.Format(format, parameters);
        }
        catch
        {
        }
        return ret;
    }

    private static string FindKey(string key)
    {
        if (table == null)
            return key;

        if (key == null)
            return null;

        key = key.Trim();
        string value;
        if (table.TryGetValue(key, out value))
            return value.Replace("\\n", "\n").Replace("\\\\", "\\");

        return key.Replace("\\n", "\n").Replace("\\\\", "\\");//"Key Not Found :\"" + key + "\"";
    }
}