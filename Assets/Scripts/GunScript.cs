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
    private float power = 3000;
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
        Rigidbody2D rigi = ball.GetComponent<Rigidbody2D>();
        setTrajectoryPoints(top.transform.position, vel / rigi.mass);
    }

    void shoot()
    {
        bl = (GameObject)Instantiate(bullet);
        bl.SetActive(true);
        Vector3 pos = top.transform.position;
        pos.z = 1;
        bl.transform.position = pos;
        Rigidbody2D rigi = bl.GetComponent<Rigidbody2D>();
        rigi.isKinematic = false;
        rigi.gravityScale = 0.0f;
        rigi.AddForce(GetForceFrom(bl.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))*5* Time.deltaTime, ForceMode2D.Impulse);

        bl.transform.SetParent(holder.transform, false);
        if (holder.transform.childCount > numBullet)
        {
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
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 0.2f);
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
}
