using UnityEngine;
using System.Collections;

namespace x16
{

	public class ActionSignIn : Action
    {


        public override void ActionPerformed()
        {
            Messenger.Broadcast("SignInMessage");
		
			
        }
    }
}
