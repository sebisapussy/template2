using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int flashlight = 0;
    public static int usb = 0;
    public static int axe = 0;
    public static int value = 0;
    public int value2 = 0;
    private int checker = 0;

    public GameObject flashlight1;
    public GameObject usb1;
    public GameObject axe1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (value2!=checker)
        {
            checker = value2;
            value = value2;
        }

        if (value != 0)
        {
            if (value == 1) {flashlight = 1; value = 0; }
            if (value == 2) {usb = 1; value = 0; }
            if (value == 3) {axe = 1; value = 0; }

        }
        if (flashlight == 1)
        {
            flashlight1.SetActive(true);
        }
        else if (flashlight == 0)
        {
            flashlight1.SetActive(false);
        }

        if (usb == 1)
        {
            usb1.SetActive(true);

        }
        else if (usb == 0)
        {
            usb1.SetActive(false);
        }

        if (axe == 1)
        {
            axe1.SetActive(true);
        }
        else if (axe == 0)
        {
            axe1.SetActive(false);
        }

    }
}
