using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionCreateQuickMatch : Action
    {

      //  public UIPopupList numberList;

        public override void ActionPerformed()
        {
            Messenger.Broadcast("ActionCreateQuickMatch");


        }

    }
}
