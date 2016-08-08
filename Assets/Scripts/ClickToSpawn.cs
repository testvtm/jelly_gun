using UnityEngine;
using System.Collections;

public class ClickToSpawn : MonoBehaviour {
    public GameObject bullet;
	public GameObject bigBullet;
	public GameObject gun;

    public GameObject holder;
    public float requiredEmptyRadius;
	const int Left = 0;

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	//public GameObject bullet; //starting ball position
	public Vector2 bulletPosition; //starting ball position
	float shootForce = 10;
    int numBullet = 10;
	int lv = 1;

	void Start() {
		//Cursor.visible = false;
		GameObject bigBulletClone1 = Instantiate(bigBullet, new Vector2(0, gun.transform.position.y-1), Quaternion.identity) as GameObject;
		bigBulletClone1.transform.localScale = new Vector3(1.5f, 1.4f, 1.5f);
		GameObject bigBulletClone2 = Instantiate(bigBullet, new Vector2(4, gun.transform.position.y-1), Quaternion.identity) as GameObject;
		bigBulletClone2.transform.localScale = new Vector3(1.5f, 1.4f, 1.5f);
	}

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
				shoot();
                if(holder.transform.childCount > numBullet) {
                    Transform child = holder.transform.GetChild(0);
                    GameObject.Destroy(child.gameObject);
                }
            }
		}
    }

	void shoot() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 cannonPosition = new Vector2(gun.transform.position.x + 1, gun.transform.position.y + 1);
		GameObject bulletClone = Instantiate(bullet, cannonPosition, Quaternion.identity) as GameObject;
        bulletClone.transform.localScale = new Vector3(0.45f, 0.4f, 0.45f);
        bulletClone.transform.SetParent(holder.transform, false);






		//bulletClone.GetComponent<Rigidbody2D>().AddForce(Vector2.up * moveSpeed);


        //bulletClone.transform.localScale = new Vector2(1, 1);
        //bulletClone.transform.localScale = new Vector2(1, 1);
        //print("x = " + gun.transform.position.x+ 1);
        //print("y = " + gun.transform.position.y+ 1);

    }
}
