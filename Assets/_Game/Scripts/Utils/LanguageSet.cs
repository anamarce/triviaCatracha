using UnityEngine;
using System.Collections;

public class LanguageSet : MonoBehaviour {

	// Use this for initialization
	public UIPopupList popuplist;
    public SoundSet soundSetList;
	void Start () {
   
	}
	public void EnableInitial()
	{
		
		if (popuplist!=null)
		{
			//Debug.Log( Managers.Game.preferences.CurrentLanguage);


		}

	}
	void OnSelectionChange (string val)
	{
        Managers.Game.preferences.SavePreferences();

	    if (soundSetList!=null)
            soundSetList.RefreshOptions();
    }

   
}
