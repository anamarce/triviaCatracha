using UnityEngine;
using System.Collections;

namespace x16
{
    public abstract class Action : MonoBehaviour {
	
	
        public float Delay=0.0f;
        public bool Enabled=true;
	
	
        public void Enable()
        {
            Enabled = true;
        }
        public void Disable()
        {
            Enabled = false;
        }
	
        public abstract void ActionPerformed();
	
        public void ActionTriggered()
        {
            if(Enabled)
                Invoke("ActionPerformed",Delay);
        }
	
    }
}