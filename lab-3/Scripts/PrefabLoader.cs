using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabLoader : MonoBehaviour
{
    public GameObject button;
    public PanelHandler panelHandler;
    public GameObject level;

    public GameObject infoPanel;
    public GameObject refButton;
    public GameObject refButtonList;

    const string objectsDirName = "Objects";
    const string spritesDirName = "Sprites";

    // Start is called before the first frame update
    void Start()
    {
        var panels = panelHandler.containers;
        for (int i = 0; i < panels.Length; i++)
            LoadPanel(panels[i], $"Panel-{i + 1}");
    }

    public void LoadPanel(GameObject buttonList, string path)
    {
        GameObject[] folderObjects = Resources.LoadAll<GameObject>($"{objectsDirName}/{path}");

        foreach (var obj in folderObjects)
        {
            GlobalScript.prefabs.Add(obj.name, obj);

            GameObject o = Instantiate(button);

            o.transform.SetParent(buttonList.transform);
            o.GetComponentInChildren<ButtonScript>().SetText(obj.name);
            o.GetComponent<Button>().onClick.AddListener(delegate { CreateObject(obj.name); });
        }

        Object[] textures = Resources.LoadAll($"{objectsDirName}/{path}/{spritesDirName}", typeof(Sprite));

        for (int j = 0; j < textures.Length; j++)
        {
            Transform t = buttonList.transform.GetChild(j);
            t.gameObject.GetComponent<ButtonScript>().SetSprite(textures[j] as Sprite);
        }
    }

    public void CreateObject(string type, SaveData saveData = null)
    {
        GameObject o = Instantiate(GlobalScript.prefabs[type]);

        o.transform.SetParent(level.transform);

        InstanceObject instanceObject = new(o, o.name);
        GlobalScript.objects.Add(instanceObject);
        instanceObject.type = type;

        if (saveData != null)
        {
            o.transform.SetPositionAndRotation(saveData.position, saveData.rotation);
            o.transform.localScale = saveData.scale;
            instanceObject.name = saveData.name;
        }
        else{
            o.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        o.GetComponent<ObjectScript>().SetObject(instanceObject);

        GameObject refButtonInstance = Instantiate(refButton);
        refButtonInstance.GetComponent<InstanceButtonScript>().SetText(o.name);
        refButtonInstance.GetComponent<Button>().onClick.AddListener(delegate { SelectObject(instanceObject); });
        refButtonInstance.transform.SetParent(refButtonList.transform);
        instanceObject.button = refButtonInstance;
    }
    public void ClearLevel()
    {
        infoPanel.SetActive(false);
        Cursor.objectReference = null;

        foreach (var o in GlobalScript.objects)
        {
            Destroy(o.obj);
            Destroy(o.button);
        }
        GlobalScript.objects.Clear();
    }

    public void SelectObject(InstanceObject o)
    {
        infoPanel.SetActive(true);
        InfoPanelScript infoPanelScript = infoPanel.GetComponent<InfoPanelScript>();
        infoPanelScript.SetText(o.name);
        infoPanelScript.SetRotation(o.obj.transform.rotation.y);

        Cursor.Select(o);
    }

}
