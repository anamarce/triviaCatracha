using GooglePlayGames;
using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionCreateMatch : Action
    {

     
    
        public override void ActionPerformed()
        {
           Messenger.Broadcast("ActionCreateMatch");
         
        }

    }
}
