using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public GameObject prefabLoaderGameObject;
    public GameObject infoPanel;
    public PrefabLoader prefabLoader;

    public void SaveScene()
    {
        SaveList list = new();
        string json = "";

        foreach (var o in GlobalScript.objects)
        {
            SaveData cfo = new();

            cfo.name = o.name;
            cfo.type = o.type;
            cfo.position = o.obj.transform.position;
            cfo.rotation = o.obj.transform.rotation;
            cfo.scale = o.obj.transform.localScale;
            list.list.Add(cfo);
        }

        json = JsonUtility.ToJson(list);
        var path = EditorUtility.SaveFilePanel("Save scene as Json", "", "scene.json", "json");
        File.WriteAllText(path, json);
    }

    public void LoadScene()
    {
        string path = EditorUtility.OpenFilePanel("Load scene from json, ", "", "json");
        string json = File.ReadAllText(path);

        SaveList list = JsonUtility.FromJson<SaveList>(json);
        prefabLoader.ClearLevel();

        foreach (var saveData in list.list)
        {
            prefabLoader.CreateObject(saveData.type, saveData);
        }
    }

}
