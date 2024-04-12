using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    public new Camera camera;
    public LayerMask vertical;
    public LayerMask floor;
    public LayerMask objects;
    public GameObject infoPanel;
    public GameObject verticalPlane;
    public GameObject horizontalPlane;

    void MoveVerticalPlane()
    {
        // установка позиции плоскости на место выделенного объекта
        var objectPos = Cursor.objectReference.obj.transform.position;
        verticalPlane.transform.position = new Vector3(objectPos.x, 0, objectPos.z);

        //  поворот в сторону камеры
        var lookPos = transform.position - verticalPlane.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        verticalPlane.transform.rotation = rotation;
    }

    void MoveHorizontalPlane()
    {
        // установка позиции плоскости на место выделенного объекта
        var objectPos = Cursor.objectReference.obj.transform.position;
        horizontalPlane.transform.position = new Vector3(objectPos.x, objectPos.y, objectPos.z);
    }


    void LateUpdate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.creating = false;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Physics.Raycast(ray, out hit, 1000, objects))
            {
                if (Cursor.objectReference == null)
                {
                    Cursor.Select(hit.transform.GetComponent<ObjectScript>().GetObject());
                    infoPanel.SetActive(true);
                    InfoPanelScript infoPanelScript = infoPanel.GetComponent<InfoPanelScript>();
                    infoPanelScript.SetText(hit.transform.name);
                    infoPanelScript.SetRotation(hit.transform.rotation.eulerAngles.y);
                }
                else
                if (hit.transform.GetComponent<ObjectScript>().GetObject() != Cursor.objectReference)
                {
                    Cursor.Deselect();
                    Cursor.Select(hit.transform.GetComponent<ObjectScript>().GetObject());
                }

                Cursor.move = true;
            }
            else
            {
                if (Cursor.objectReference != null)
                {
                    Cursor.Deselect();
                    infoPanel.SetActive(false);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
            Cursor.move = false;

        var moveVertically = Input.GetButton("Fire3");  // l shift на клавиатуре

        if ((Cursor.move == true || Cursor.creating) && Cursor.objectReference != null)
        {
            MoveVerticalPlane();
            MoveHorizontalPlane();

            ray = camera.ScreenPointToRay(Input.mousePosition); // обновляем пересечения с курсором

            var currentPos = Cursor.objectReference.obj.transform.position;

            if (moveVertically && Physics.Raycast(ray, out hit, 100, vertical))         // Двигаем (только) вертикально
                Cursor.SetPosition(new Vector3(currentPos.x, hit.point.y, currentPos.z));
            else
            if (Physics.Raycast(ray, out hit, 100, floor))                              // Двигаем во всех осях кроме y
                Cursor.SetPosition(new Vector3(hit.point.x, currentPos.y, hit.point.z));
        }
    }

}
