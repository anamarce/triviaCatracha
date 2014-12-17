using UnityEngine;
using System.Collections;

public class ChkLanguageSet : MonoBehaviour
{

    public string language="English";

    public  UICheckbox langCheckBox;
    void Start()
    {
       
    }
    void OnActivate()
    {

        if (langCheckBox != null)
        {
          if (langCheckBox.isChecked)
              Managers.Social.SetMatchLanguage(language);
          else
              Managers.Social.SetMatchLanguage("");
        }
        else
        {
            Managers.Social.SetMatchLanguage("");
        }
    }
}
