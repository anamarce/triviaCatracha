using UnityEngine;
using System.Collections;
using x16;

public class ActionTrigger : MonoBehaviour {
	
	
	
	
	public Action [] Actions;
	
	
	
	void Awake () {
	    Actions = this.transform.GetComponentsInChildren<Action>();
	}
	
	public void ExecuteAllActions()
	{
		foreach(var action in Actions)
		{
			action.ActionTriggered();
		}
	}
	
	
}
