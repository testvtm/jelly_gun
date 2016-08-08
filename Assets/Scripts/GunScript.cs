using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public GameObject barrel;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dir = Input.mousePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        barrel.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // draw
    void draw()
    {

    }
}
