using UnityEngine;
using System.Collections;

namespace x16
{

    public abstract class InitAction : Action
    {

        // Use this for initialization
        private void Start()
        {
            Invoke("ActionTrigger", Delay);
        }

        private void ActionTrigger()
        {
            ActionPerformed();
        }


    }
}
