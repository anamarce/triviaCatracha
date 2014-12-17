using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionOpenUrl : Action
    {

        public string url;

        public override void ActionPerformed()
        {
            if (url != string.Empty)
                Application.OpenURL(url);
        }
    }
}
