using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionNewLoadScene : Action
    {

        public string SceneName;

        public override void ActionPerformed()
        {

            Managers.SceneManager.LoadLevel(SceneName);
        }

    }
}
