using UnityEngine;
using System.Collections;

public class MG : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset () {
        Application.LoadLevel (Application.loadedLevelName);
    }
}
