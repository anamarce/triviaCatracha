using UnityEngine;
using System.Collections;

namespace x16
{
    public class DelayStartScene : MonoBehaviour
    {

        public float DelayTime = 1F;
        public string SceneName;
        // Use this for initialization
        private void Start()
        {
            Invoke("LoadScene", DelayTime);
        }


        private void LoadScene()
        {
            Application.LoadLevel(SceneName);
        }
    }
}
