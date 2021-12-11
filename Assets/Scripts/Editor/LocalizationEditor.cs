using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationEditor : Editor
{
    private static string EXE_FILE = "EI18N.exe";
    private static string PREFAB_DATA_OUTPUT_PATH = string.Format(@"{0}/Tools/EI18N/prefab_output.json", Application.dataPath);

    private static void Excute(string arguments)
    {
        Process process = new Process();
        process.StartInfo.FileName = string.Format("{0}/Tools/EI18N/{1}", Application.dataPath, EXE_FILE);
        process.StartInfo.Arguments = arguments;
        process.Start();
        process.WaitForExit();

        AssetDatabase.Refresh();
    }

    [MenuItem("Localization/Extract")]
    public static void Extract()
    {
        ExportPrefabData();
        Excute(string.Format("Extract {0}", Application.dataPath));
    }

    [MenuItem("Localization/Merge")]
    public static void Merge()
    {
        Excute("Merge");
    }

    private static void ExportPrefabData()
    {
        var list = new Dictionary<string, List<KeyValuePair<string, string>>>();
        foreach (var prefabPath in GetAllPrefabs())
        {
            GameObject prefabOriginal = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefabOriginal == null)
                continue;

            var prefab = PrefabUtility.InstantiatePrefab(prefabOriginal) as GameObject;
            Text[] allText = prefab.GetComponentsInChildren<Text>(true);
            foreach (Text text in allText)
            {
                if (!text.text.StartsWith("$$"))
                    continue;

                if (!list.ContainsKey(text.text))
                    list.Add(text.text, new List<KeyValuePair<string, string>>());

                var hierarchy = GetFullName(text.gameObject);
                var pair = new KeyValuePair<string, string>(prefabPath, hierarchy);
                list[text.text].Add(pair);
            }

            GameObject.DestroyImmediate(prefab);
        }

        var json = JsonConvert.SerializeObject(list);
        System.IO.File.WriteAllText(PREFAB_DATA_OUTPUT_PATH, json);
    }

    private static string GetFullName(GameObject gameObject)
    {
        string name = gameObject.name;
        while (gameObject.transform.parent != null)
        {
            gameObject = gameObject.transform.parent.gameObject;
            //if (gameObject.transform.parent == null)
            //	break;

            name = gameObject.name + "/" + name;
        }
        return name;
    }

    public static string[] GetAllPrefabs()
    {
        string[] temp = AssetDatabase.GetAllAssetPaths();
        List<string> result = new List<string>();
        foreach (string s in temp)
        {
            if (s.Contains(".prefab"))
                result.Add(s);
        }
        return result.ToArray();
    }
}