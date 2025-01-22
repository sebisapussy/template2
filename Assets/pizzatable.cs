using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pizzatable : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    private Outline outline1;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        outline1 = gameObject.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (object1.activeSelf == true)
        {
            outline1.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (object1.activeSelf == true)
                        {
                            if (audioSource != null)
                            {
                                audioSource.Play();
                            }
                            else
                            {
                                Debug.LogWarning("AudioSource not assigned.");
                            }
                            object1.SetActive(false);
                            object2.SetActive(true);
                        }

                    }
                }
            }
        }
        else
        {
            outline1.enabled = false;
        }
    }
}
