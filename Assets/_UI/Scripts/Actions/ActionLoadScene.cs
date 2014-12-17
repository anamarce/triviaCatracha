using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionLoadScene : Action
    {

        public string SceneName;

        public override void ActionPerformed()
        {

            Application.LoadLevel(SceneName);

        }

    }
}
