using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolAppChanger : MonoBehaviour
{
    public int start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                    SchoolAppClipChanger2.value = start;
                }
            }
        }
    }
}
