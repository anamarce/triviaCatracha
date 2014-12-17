using UnityEngine;
using System.Collections;

namespace x16
{


    public abstract class UiAction : Action
    {

        public enum UIActionEvent
        {
            OnClick,
            OnHoverIn,
            OnHoverOut,
            OnPressDown,
            OnPressUp
        };

        public UIActionEvent EventTrigger;

        private void OnClick()
        {
            if (EventTrigger == UIActionEvent.OnClick)
                EventTriggered();
        }

        private void EventTriggered()
        {
            if (Delay > 0.0f)
                Invoke("ActionPerformed", Delay);
            else
                ActionPerformed();
        }


    }
}
