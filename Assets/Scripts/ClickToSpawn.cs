using UnityEngine;
using System.Collections;

public class ClickToSpawn : MonoBehaviour {
    public GameObject bullet;
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

	void Start() {
		//Cursor.visible = false;
	}

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
				shoot();
                if(holder.transform.childCount > numBullet)
                {
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
		bulletClone.GetComponent<Rigidbody2D>().AddForce(mousePosition);
        bulletClone.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        bulletClone.transform.SetParent(holder.transform, false);
        //bulletClone.transform.localScale = new Vector2(1, 1);
        //bulletClone.transform.localScale = new Vector2(1, 1);
        //print("x = " + gun.transform.position.x+ 1);
        //print("y = " + gun.transform.position.y+ 1);

        //GUI.DrawTexture(new Rect(Input.mousePosition.x-cursorSizeX/2 + cursorSizeX/2, (Screen.height-Input.mousePosition.y)-cursorSizeY/2 + cursorSizeY/2, cursorSizeX, cursorSizeY),originalCursor);


        //print (mousePosition);
        //print (transform.forward * shootForce * Time.deltaTime);
        //print (mousePosition);
        //bullet = Instantiate(bullet, cannonPosition, Quaternion.identity) as GameObject;
        //bulletPosition = new Vector3(0, 2, 12);
        //bullet = Instantiate(bullet, bulletPosition, transform.rotation) as GameObject
        //bullet.GetComponent<Rigidbody2D>().AddForce(throwSpeed, ForceMode.Impulse);
        //bullet = Instantiate(bullet, cannonPosition, Quaternion.identity) as GameObject;
        //bullet.GetComponent<Rigidbody2D>().AddForce(throwSpeed);
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

        /*mousePosition = mousePosition - transform.position;
		float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
		int objectAngle = 90; // Default Angle of an Object
		transform.rotation = Quaternion.AngleAxis(angle - objectAngle, Vector3.forward);*/
    }

	//void OnGUI() {
		//int cursorSizeX = 25;
		//int cursorSizeY = 25;
		//GUI.DrawTexture(new Rect(Input.mousePosition.x-cursorSizeX/2 + cursorSizeX/2, (Screen.height-Input.mousePosition.y+2)-cursorSizeY/2 + cursorSizeY/2, cursorSizeX, cursorSizeY), cursorTexture);
	//}
}
