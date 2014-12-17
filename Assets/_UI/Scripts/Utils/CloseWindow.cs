using UnityEngine;
using System.Collections;

namespace x16
{
    public class CloseWindow : MonoBehaviour
    {
        public UIPanel TargetWindow;
        public float DelayTime = 4F;
        public float elapsedTime;

        private void Start()
        {
            elapsedTime = 0F;

        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > DelayTime)
            {

                Destroy(TargetWindow.gameObject);
            }
            else
            {
                if (elapsedTime > (DelayTime/2))
                    TargetWindow.alpha -= 0.001f;
            }
        }





    }
}
