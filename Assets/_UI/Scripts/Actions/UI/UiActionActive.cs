using UnityEngine;
using System.Collections;

namespace x16
{
    public class UiActionActive : UiAction
    {
        public bool Active = true;

        public GameObject Target;

        public override void ActionPerformed()
        {

            if (Target != null)
                Target.gameObject.SetActive(Active);
            else
                this.gameObject.SetActive(Active);
        }


    }
}
