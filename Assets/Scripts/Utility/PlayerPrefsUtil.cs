using UnityEngine;

public static class PlayerPrefsUtil
{
    public static void SetData<T>(T data, string key)
        where T : struct
    {
        var json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static T GetData<T>(string key)
        where T : struct
    {
        var str = PlayerPrefs.GetString(key);

        if (string.IsNullOrWhiteSpace(str))
            return default;

        return JsonUtility.FromJson<T>(str);
    }

    public static void SetBool(bool data, string key)
    {
        PlayerPrefs.SetInt(key, data ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key, 0) == 1;
    }
}
