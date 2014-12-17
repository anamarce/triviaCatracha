using UnityEngine;
using System.Collections;

public class UnloadResourcesScript : MonoBehaviour {

	// Use this for initialization
    private IEnumerator Start()
    {
        AsyncOperation async = Resources.UnloadUnusedAssets();
        yield return async;
    }
	
	
}
