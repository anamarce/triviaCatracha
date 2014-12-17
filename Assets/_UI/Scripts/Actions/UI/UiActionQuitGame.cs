using UnityEngine;
using System.Collections;

namespace x16
{
    public class UiActionQuitGame : UiAction
    {

        public override void ActionPerformed()
        {
            Application.Quit();
        }

    }
}
