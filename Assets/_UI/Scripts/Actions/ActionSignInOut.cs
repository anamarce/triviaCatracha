using UnityEngine;
using System.Collections;

namespace x16
{

	public class ActionSignInOut : Action
    {



        public override void ActionPerformed()
        {
            Messenger.Broadcast("ActionSigInOut");
           
        }
    }
}
