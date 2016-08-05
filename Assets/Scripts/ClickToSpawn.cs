using UnityEngine;
using System.Collections;

public class ClickToSpawn : MonoBehaviour {
    public GameObject bullet;
    public float requiredEmptyRadius;

    const int Left = 0;

	//public GameObject bullet; //starting ball position
	public Vector3 bulletPosition; //starting ball position

	private Vector3 throwSpeed = new Vector3(0, 26, 40);
	private bool isThrown = false; //if ball has been thrown, prevents 2 or more balls

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            //Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
			//Instantiate(bullet, position, Quaternion.identity);
            //}
			shoot();
        }
    }

	void shoot() {
		throwSpeed.y = throwSpeed.y;
		throwSpeed.z = throwSpeed.z;

		print (123);

		//bullet = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



		Instantiate(bullet, mousePosition, Quaternion.identity);
			//bulletPosition = new Vector3(0, 2, 12);

		//bullet = Instantiate(bullet, bulletPosition, transform.rotation) as GameObject;

		//bullet.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);

		if (bullet != null && (bullet.transform.position.y <= -2.45))  {
			//Destroy (bullet);
			isThrown = false;
			throwSpeed = new Vector3(0, 26, 40);
		}
	}
}
