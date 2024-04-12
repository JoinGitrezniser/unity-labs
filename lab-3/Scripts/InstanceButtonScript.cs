using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstanceButtonScript : MonoBehaviour
{
    public TMP_Text text;
    public void SetText(string _text)
    {
        text.text = _text;
    }
}
