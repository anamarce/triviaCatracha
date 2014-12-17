using UnityEngine;
using System.Collections;

public class PanelSettings : PanelScript
{
    public LanguageSet LS;

    void OnEnable()
    {
      Debug.Log("Setting mmmm");
      LS.EnableInitial();
    }

}
