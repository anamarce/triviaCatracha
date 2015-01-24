using UnityEngine;
using System.Collections;

public class inicio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("loadMenu", 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void loadMenu()
	{

		Application.LoadLevel ("Xtudio16StartScene");
	}
}
