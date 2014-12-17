using UnityEngine;
using System.Collections;

namespace x16
{
    public class NavigationScript : MonoBehaviour
    {

        public string SceneNameBackButton;
        public bool isMainMenu = false;

        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isMainMenu)
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }
                else
                {


                    Application.LoadLevel(SceneNameBackButton);
                }

            }
        }
    }
}
