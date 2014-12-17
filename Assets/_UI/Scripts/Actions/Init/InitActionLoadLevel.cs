using UnityEngine;
using System.Collections;

namespace x16
{
    public class InitActionLoadLevel : InitAction
    {

        public string SceneName;



        public override void ActionPerformed()
        {

            Application.LoadLevel(SceneName);

        }

    }
}
