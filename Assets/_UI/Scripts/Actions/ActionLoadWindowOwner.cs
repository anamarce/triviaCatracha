using UnityEngine;
using System.Collections;

namespace x16
{
    public class ActionLoadWindowOwner : ActionLoadWindow
    {

        public GameObject LoadButton;
        public UIPanel BackPanel;

        public override void ActionPerformed()
        {
            var parent = WindowLoader.Show(WindowPrefabName, WindowParent);

            var script = parent.GetComponentInChildren<ActionShowLoadOwner>();
            if (script != null)
            {
                script.Target = LoadButton;
                script.PanelTarget = BackPanel;
            }
            else
                Debug.LogWarning("No ActionShowLoadOwner script was found");

            if (BackPanel != null)
                NGUITools.SetActive(BackPanel.gameObject, false);
        }



    }
}
