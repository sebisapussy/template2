using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;
        public GameObject enter;
		private int value;
		public AudioSource opensound;
        public AudioSource closesound;

        void Start()
		{
			open = false;
		}

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				if (Player)
				{
                    if (enter)
					{ 
						float dist = Vector3.Distance(Player.position, transform.position);
						if (hit.transform == transform && dist < 3) // If clicking on the cube
						{
							enter.SetActive(true);
							value = 1;
						}
						else if (value == 1)
						{
							enter.SetActive(false);
							value = 0;
						}

                    }

                }
			}
        }

        void OnMouseOver()
		{
            
            {
				if (Player)
                {
                    float dist = Vector3.Distance(Player.position, transform.position);
                    if (dist < 3)
					{
                        enter.SetActive(true);
                        if (open == false)
						{
							if (Input.GetKeyDown(KeyCode.E))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetKeyDown(KeyCode.E))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}

			}

		}

		IEnumerator opening()
		{
            if (opensound != null)
            {
                opensound.Play();
            }
            openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
            if (closesound != null)
            {
                closesound.Play();
            }
            openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}