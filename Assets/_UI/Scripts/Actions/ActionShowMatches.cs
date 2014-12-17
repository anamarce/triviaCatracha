using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionShowMatches : Action
    {

		public UILabel lblMessage;

        public override void ActionPerformed()
        {
			
			if (!Managers.Social.IsAuthenticated()) 
			{
				if (lblMessage!=null)
				{
					lblMessage.text = Localization.Localize("siginfirst");
					Invoke("Dissapear",2F);
				}
				return;
			}
            
            Messenger.Broadcast("ActionShowMatches");
            
        }
		public void  Dissapear()
		{
			if (lblMessage!=null)
			{
				lblMessage.text = "";
			}
		}



    }
}
