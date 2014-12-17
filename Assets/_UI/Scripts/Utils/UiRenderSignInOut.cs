using UnityEngine;
using System.Collections;

public class UiRenderSignInOut : MonoBehaviour
{

    public UISprite ButtonSprite;
	// Use this for initialization
	void Start () {
		UpdateLabel();
	}

	void UpdateLabel()
	{
        if (ButtonSprite == null) return;
		
		if (Managers.Social.IsAuthenticated())
            ButtonSprite.spriteName = "Icono-logout-cerrado";
		else
            ButtonSprite.spriteName = "Icono-logout-abierto";
	}

	// Update is called once per frame
	void Update () {
		UpdateLabel();
	}
}
