using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelScript : MonoBehaviour
{
    public TMP_InputField SelectedItemName;
    public Slider rotationSlider;
    public void Start()
    {
        SelectedItemName.onEndEdit.AddListener(name => OnEndEdit(name));
        rotationSlider.onValueChanged.AddListener(value => OnRotationChanged(value));
    }

    public void OnEndEdit(string newName)
    {
        if (Cursor.objectReference == null) return;
        Cursor.objectReference.name = newName;
        Cursor.objectReference.obj.name = newName;
        Cursor.objectReference.button.GetComponent<InstanceButtonScript>().SetText(newName);
    }

    public void SetText(string _text)
    {
        SelectedItemName.text = _text;
    }

    public void SetRotation(float rotation)
    {
        rotationSlider.value = rotation;
    }

    public void OnRotationChanged(float rotation)
    {
        if (Cursor.objectReference == null) return;

        Cursor.objectReference.obj.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void Delete()
    {
        GlobalScript.objects.Remove(Cursor.objectReference);
        Destroy(Cursor.objectReference.obj);
        Destroy(Cursor.objectReference.button);

        Cursor.objectReference = null;

        transform.gameObject.SetActive(false);
    }


}
