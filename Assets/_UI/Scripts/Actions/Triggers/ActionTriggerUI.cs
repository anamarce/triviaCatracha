using UnityEngine;
using System.Collections;

public class ActionTriggerUI : ActionTrigger {

	public enum UIActionEvent{OnClick,OnHoverIn,OnHoverOut,OnPressDown,OnPressUp};
	public UIActionEvent EventTrigger;
		
	void OnClick()
	{
		if(EventTrigger==UIActionEvent.OnClick)
			ExecuteAllActions();
	}
	
    
}
