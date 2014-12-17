using UnityEngine;
using System.Collections;


namespace x16
{

    public class WindowLoader
    {




        public static GameObject Show(string prefabName, string parentGameObjectName)
        {
            var root = GameObject.Find(parentGameObjectName);
            if (root == null)
                Debug.LogError("There is no " + parentGameObjectName + " game object defined in the scene");

            return Show(prefabName, root.transform);
        }

        public static GameObject Show(string prefabName, Transform parent)
        {

            var panel = (GameObject) GameObject.Instantiate(Resources.Load(prefabName));
            Vector3 origScale = panel.transform.localScale;
            panel.transform.parent = parent.transform;
            panel.transform.localScale = origScale;
            return panel;
        }


    }
}
