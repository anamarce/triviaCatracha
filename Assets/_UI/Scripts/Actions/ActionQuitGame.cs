using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionQuitGame : Action
    {



        public override void ActionPerformed()
        {
            print("QuitGame");
            Application.Quit();
        }
    }
}
