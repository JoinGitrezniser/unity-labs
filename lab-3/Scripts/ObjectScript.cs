using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    InstanceObject instanceObject = null;

    public Material normal;
    public Material selected;
    public Material collision;
    bool selection = false;

    Renderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        StartCoroutine(SetCreating());
    }

    public IEnumerator SetCreating()
    {
        yield return new WaitForEndOfFrame();
        Cursor.SetCreating(instanceObject);
    }

    public void Select()
    {
        meshRenderer.material = selected;
        selection = true;
    }

    public void Deselect()
    {
        meshRenderer.material = normal;
        selection = false;
    }

    public void SetObject(InstanceObject o)
    {
        instanceObject = o;
    }

    public InstanceObject GetObject()
    {
        return instanceObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("model"))
            meshRenderer.material = collision;
    }

    private void OnTriggerExit(Collider other)
    {
        if (selection == true)
            meshRenderer.material = selected;
        else
            meshRenderer.material = normal;
    }
}
