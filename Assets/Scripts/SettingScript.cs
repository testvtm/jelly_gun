using UnityEngine;
using System.Collections;

public class SettingScript
{
    public enum SCENES
    {
        LEVEL1 = 0,
        LEVEL2,
    }
    public static int goal(SCENES  scene)
    {
        int g = 1;
        switch(scene)
        {
            case SCENES.LEVEL1:
                g = 1;
                break;
            case SCENES.LEVEL2:
                g = 3;
                break;
        }
        return g;
    }
    public static int score = 1;
    public static SCENES currentScene { get; set; }
}
