using UnityEngine;
using System.Collections;

public class SoundSet : MonoBehaviour
{

	// Use this for initialization
	public UIPopupList popuplist;
	void Start () {
		if (popuplist!=null)
		{
            RefreshOptions();

		}
	
	}

    public void RefreshOptions()
    {
        popuplist.items.Clear();
        popuplist.items.Add(Localization.Localize("on"));
        popuplist.items.Add(Localization.Localize("off"));
        if (Managers.Game.preferences.EnableSound)
            popuplist.selection = Localization.Localize("on");
        else
            popuplist.selection = Localization.Localize("off");

    }
	
	void OnSelectionChange (string val)
	{
	    if (val == Localization.Localize("on"))
	        Managers.Game.preferences.EnableSound = true;
	    else
            Managers.Game.preferences.EnableSound = false;
        
        Managers.Game.preferences.SavePreferences();

        popuplist.selection = Localization.Localize(val);
       
	}
}
