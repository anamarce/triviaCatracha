using UnityEngine;
using System.Collections;

namespace x16
{
    public class ActionActive:Action
    {
        public bool Active=true;
        public GameObject Target;	
	
        public override void ActionPerformed ()
        {
            if(Target!=null)
                Target.SetActive(Active);
            else
                gameObject.SetActive(Active);
        }
    }
}