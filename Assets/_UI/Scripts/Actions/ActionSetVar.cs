using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionSetVar : Action
    {

        public Vars Vars;
        public string Key;
        public string Value;

        public override void ActionPerformed()
        {
            Vars.Values[Key] = Value;
        }
    }
}
