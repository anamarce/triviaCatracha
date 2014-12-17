using UnityEngine;
using System.Collections;

namespace x16
{



    public class ActionSpinTopic : Action
    {

        // Use this for initialization
        private void Start()
        {

        }


        public override void ActionPerformed()
        {
            Messenger.Broadcast("SpinButtonClicked");
        }
    }
}