using UnityEngine;
using System.Collections;

namespace x16
{
    public class UiActionSendMessage : UiAction
    {

        public GameObject Target;
        public string TargetMethod;
        public string[] Arguments;


        public override void ActionPerformed()
        {
            if (Target == null)
                Debug.LogError("Target is not specified");
            if (TargetMethod == null || TargetMethod == "")
                Debug.LogError("TargetMethod is not specified");

            Target.SendMessage(TargetMethod, Arguments, SendMessageOptions.RequireReceiver);
        }

    }
}
