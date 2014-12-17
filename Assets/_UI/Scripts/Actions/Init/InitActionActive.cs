using UnityEngine;
using System.Collections;

namespace x16
{
    public class UiInitActive : InitAction
    {

        public bool Active = true;


        public override void ActionPerformed()
        {
            gameObject.SetActive(Active);
        }


    }
}
