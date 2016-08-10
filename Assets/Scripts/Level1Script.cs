using UnityEngine;
using System.Collections;

public class Level1Script : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        SettingScript.score = 0;
        SettingScript.currentScene = SettingScript.SCENES.LEVEL1;
    }
	
}
