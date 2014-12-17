using UnityEngine;
using System.Collections;

namespace x16
{
    public class UiActionLoadScene : UiAction
    {

        public string SceneName;


        public override void ActionPerformed()
        {

            Application.LoadLevel(SceneName);

        }



    }
}
