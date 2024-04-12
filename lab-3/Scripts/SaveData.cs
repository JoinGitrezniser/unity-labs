using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string name;
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

public class SaveList
{
    public List<SaveData> list = new();
}
