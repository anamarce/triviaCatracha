using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

	// Use this for initialization
    public Camera CameraObject;
    public string SceneToStart;
	public PanelScript [] Panels;
    private int CurrentIndexScene = 0;
  
	void Start ()
	{
	    CurrentIndexScene = 0;
        // Disable all panels Except the first panel
		DisableAllPanels();
      //  LoadLevel(SceneToStart);
	}

    void DisableAllPanels()
    {
        for (int i = 1; i < Panels.Length; i++)
        {
            if (Panels[i] != null)
            {
                Panels[i].enabled = false;
                NGUITools.SetActive(Panels[i].myPanel.gameObject, false);
            }
        }
    }
    int GetPanelIndex(string name)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i] != null)
            {
                if (Panels[i].name == name)
                    return i;
            }
        }
        return -1;
    }
    public void LoadLevel(string scenename)
    {
        int index = GetPanelIndex(scenename);
        CameraObject.transform.localPosition = Panels[index].myPanel.transform.localPosition;
        Panels[CurrentIndexScene].enabled = false;
        NGUITools.SetActive(Panels[CurrentIndexScene].myPanel.gameObject,false);
        NGUITools.SetActive(Panels[index].myPanel.gameObject, true);
        Panels[index].enabled = true;
        CurrentIndexScene = index;

    }
}
