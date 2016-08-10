using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickToSpawn : MonoBehaviour {
    public GameObject bullet;
	public GameObject bigBullet;
	public GameObject gun;
	public GameObject gunTop;


    public GameObject holder;
    public float requiredEmptyRadius;
	const int Left = 0;


	//public GameObject bullet; //starting ball position
	public Vector2 bulletPosition; //starting ball position
	float shootForce = 10;
    int numBullet = 10;
	public float range = 4;
    
	float delayTime = 0.2f;
	float nextTime = 0.0f;

	void Start() {
//		GameObject bigBulletClone1 = Instantiate(bigBullet, new Vector2(0, -4.2f), Quaternion.identity) as GameObject;
//		bigBulletClone1.transform.localScale = new Vector3(1f, 0.95f, 1f);
//		GameObject bigBulletClone2 = Instantiate(bigBullet, new Vector2(4, -4.2f), Quaternion.identity) as GameObject;
//		bigBulletClone2.transform.localScale = new Vector3(1f, 0.95f, 1f);
	}

    void Update()
    {
		/*if (Input.GetMouseButtonDown(Left) && Time.time > nextTime)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
                shoot();
				nextTime = Time.time + delayTime;
                if(holder.transform.childCount > numBullet) {
                    Transform child = holder.transform.GetChild(0);
                    GameObject.Destroy(child.gameObject);
                }
            }
		}*/
    }
    
	void shoot() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 bulletStartPosition = new Vector2(gunTop.transform.position.x, gunTop.transform.position.y);
		GameObject bulletClone = Instantiate(bullet, bulletStartPosition, Quaternion.identity) as GameObject;
        bulletClone.transform.rotation = gunTop.transform.rotation;
        bulletClone.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        bulletClone.transform.SetParent(holder.transform, false);
		bulletClone.GetComponent<Rigidbody2D>().velocity = calculateBestThrowSpeed(bulletStartPosition, mousePosition, 0.5f);
    }




    private Vector2 calculateBestThrowSpeed(Vector2 origin, Vector2 target, float timeToTarget)
    {
		// calculate vectors
		Vector2 toTarget = target - origin;
		Vector2 toTargetXZ = toTarget;
		toTargetXZ.y = 0;

		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;

		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;

		// create result vector for calculated starting speeds
		Vector2 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)

		return result;
	}
}