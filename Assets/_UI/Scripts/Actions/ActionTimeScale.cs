using UnityEngine;
using System.Collections;

namespace x16
{
    public class ActionTimeScale : Action
    {

        public float TimeScale;

        public override void ActionPerformed()
        {
            Time.timeScale = TimeScale;
        }
    }
}
