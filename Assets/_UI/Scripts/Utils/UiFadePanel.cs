using UnityEngine;
using System.Collections;

public class UiFadePanel : MonoBehaviour {

	public UIPanel panel;
	public float delay=1F;
	public float deltaalpha=0.01F;
	float initialtime=0F;
	// Use this for initialization
	void Start () {

	  
	}
	
	// Update is called once per frame
	void Update () {
		initialtime += Time.deltaTime;
		if (initialtime<=delay)
		   panel.alpha = panel.alpha - deltaalpha;
	}
}
