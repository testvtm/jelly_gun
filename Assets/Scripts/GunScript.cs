using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunScript : MonoBehaviour
{
    public GameObject barrel;
    public GameObject bullet;
    public GameObject trajectoryPointPrefab;
    public GameObject top;
    public GameObject holder;

    private GameObject ball;
    private GameObject bl;
    private bool isPressed, isBallThrown;
    private float power = 5;
    int numBullet = 10;
    private int numOfTrajectoryPoints = 30;
    private List<GameObject> trajectoryPoints;
    private bool facingRight = true;
   

    void Start()
    {
        trajectoryPoints = new List<GameObject>();
        isPressed = isBallThrown = false;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(trajectoryPointPrefab);
            dot.SetActive(false);
            trajectoryPoints.Insert(i, dot);
        }
        ball = (GameObject)Instantiate(bullet);
        Vector3 pos = top.transform.position;
        pos.z = 1;
        ball.transform.position = pos;
        ball.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            //if (!isBallThrown)
                //throwBall();
        }
        // when mouse button is pressed, cannon is rotated as per mouse movement and projectile trajectory path is displayed.
        Vector3 vel = GetForceFrom(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
        barrel.transform.eulerAngles = new Vector3(0, 0, angle);
//        Rigidbody2D rigi = ball.GetComponent<Rigidbody2D>();
//        setTrajectoryPoints(top.transform.position, vel / rigi.mass);
    }

    void shoot()
    {
        bl = (GameObject)Instantiate(bullet);
        bl.SetActive(true);
        bl.transform.SetParent(holder.transform, false);
        Vector3 pos = top.transform.position;
        pos.z = 1;
        bl.transform.position = pos;
        Rigidbody2D rigi = bl.GetComponent<Rigidbody2D>();
        rigi.isKinematic = true;
        rigi.gravityScale = 0.0f;
        //rigi.AddForce(Vector3.up * 10 * Time.deltaTime, ForceMode2D.Impulse);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigi.velocity = GetForceFrom(pos, mousePosition);

        if (holder.transform.childCount > numBullet) {
            Transform child = holder.transform.GetChild(0);
            GameObject.Destroy(child.gameObject);
        }
    }

    Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;
    }

    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        //fTime += 0.01f;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 0.6f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 0);
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].SetActive(true);
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 thescale = transform.localScale;
        thescale.y = -thescale.y;
        //barrel.transform.localScale = thescale;
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
