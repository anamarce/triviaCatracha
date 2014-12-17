using UnityEngine;
using System.Collections;

namespace x16
{

	public class ActionSendMail : Action
    {

        public string mailto;
		public string subject;
		public string body;

		string MyEscapeURL (string url)
		{
			return WWW.EscapeURL(url).Replace("+","%20");
		}
        public override void ActionPerformed()
        {
			 mailto = MyEscapeURL(mailto);
			 subject = MyEscapeURL(subject);
			body = MyEscapeURL(body);

			Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);

        }
    }
}
