using UnityEngine;
using System.Collections;

namespace x16
{

	public class ActionNewMatch : Action
    {

        public string SceneName;
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
           
            Managers.SceneManager.LoadLevel(SceneName);

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
