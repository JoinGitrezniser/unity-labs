
using System;
using System.Collections.Generic;
using UnityEngine;

public class InstanceObject
{
    public GameObject obj;
    public string name;
    public string type;
    public GameObject button = null;

    public InstanceObject(GameObject o, string _name)
    {
        obj = o;
        name = _name;
    }
}

public class Cursor
{
    public static InstanceObject objectReference = null;
    public static bool move = false;
    public static bool creating = false;

    public static void Select(InstanceObject o)
    {
        objectReference = o;
        objectReference.obj.GetComponent<ObjectScript>().Select();
    }

    public static void Deselect()
    {
        objectReference.obj.GetComponent<ObjectScript>().Deselect();
        objectReference = null;
    }
    public static void SetCreating(InstanceObject instanceObject)
    {
        Select(instanceObject);
        creating = true;
    }
    public static void SetPosition(Vector3 position)
    {
        objectReference.obj.transform.position = position;
    }

}

public class GlobalScript
{
    public static Dictionary<string, GameObject> prefabs = new();
    public static List<InstanceObject> objects = new();
}
