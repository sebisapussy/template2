using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class MaterialCopyScript : MonoBehaviour
{
    public GameObject IGNORETHISNOTRELATEDBUTIMPORTANT;
    Material dup;
    public Material dup3;
    public TMP_Text displayText2;
    public GameObject ANOTHERIGNOIRE;
    public static GameObject ANOTHERIGNOIRE2;
    public static string rem;
    public static Renderer targetRenderer;

    public static TMP_Text displayText;
    void Start()
    {
        ANOTHERIGNOIRE2 = ANOTHERIGNOIRE;
        //FIX THIS
        targetRenderer =  IGNORETHISNOTRELATEDBUTIMPORTANT.GetComponent<Renderer>();
    }

    void Update()
    {
        if (rem == null || rem == "")
        {
            
        }
        else
        {
            displayText2.text = rem;
        }
    }

    public static void Updater()
    {
        if (PlayerMovement.chair && PlayerMovement.Freeze)
        {
            Material dup = WindowsTaskbarIconsApp.GetMaterialFoccused();
            if (dup != null)
            {
                ANOTHERIGNOIRE2.SetActive(true);
                
                if (targetRenderer != null)
                {
                    targetRenderer.material = dup;
                }

                
            }
        }
    }
}
