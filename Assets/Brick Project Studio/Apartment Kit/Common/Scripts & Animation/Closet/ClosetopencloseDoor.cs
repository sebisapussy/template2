using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SojaExiles

{
	public class ClosetopencloseDoor : MonoBehaviour
	{

		public Animator Closetopenandclose;
		public bool open;
		public bool chagnescene2;
        public string scenechanger;
        public bool chagnescene;
        public Transform Player;
        public GameObject enter;
        private int value;

        void Start()
		{
			open = false;
		}

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 3)
					{	
						if (open == false)
						{
                            if (Input.GetKeyDown(KeyCode.E))
                            {
								if (chagnescene)
                                {

                                    ResetAll.RESET = true;
                                    StartCoroutine(LoadSceneWithDelay("Road"));
                                    return;
                                }
								if (chagnescene2)
								{
                                    SceneManager.LoadScene("Computer");
									return;
								}

                                if (scenechanger != "")
                                {
                                     SceneManager.LoadScene(scenechanger);
                                     return;
                                }
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

        IEnumerator opening()
		{
			print("you are opening the door");
			Closetopenandclose.Play("ClosetOpening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			Closetopenandclose.Play("ClosetClosing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}

        private IEnumerator LoadSceneWithDelay(string test)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(0.15f);

            // After 1 second, load the scene
            SceneManager.LoadScene(test);
        }




    }
}