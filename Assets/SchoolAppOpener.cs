using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolAppOpener : MonoBehaviour
{
    public GameObject appOpener;
    public GameObject closestartmenu;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    appOpener.SetActive(true);
                    if (closestartmenu != null) { closestartmenu.SetActive(false); }
                }
            }
        }
    }
}
