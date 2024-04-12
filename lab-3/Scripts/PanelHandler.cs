using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    public GameObject[] containers;
    public GameObject[] panels;
    public void OnDropdownOption(int option)
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
        panels[option].SetActive(true);
    }
}
