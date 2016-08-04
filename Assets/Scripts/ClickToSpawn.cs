using UnityEngine;
using System.Collections;

public class ClickToSpawn : MonoBehaviour {
    public GameObject prefab;
    public float requiredEmptyRadius;

    const int Left = 0;

	public GameObject bullet; //starting ball position
	public Vector3 bulletPosition; //starting ball position

	private Vector3 throwSpeed = new Vector3(0, 26, 40);
	private bool isThrown = false; //if ball has been thrown, prevents 2 or more balls

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            //Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
                //Instantiate(prefab, position, Quaternion.identity);
            //}
			throwBall();
        }
    }

	void throwBall() {

			print (123);
			throwSpeed.y = throwSpeed.y;
			throwSpeed.z = throwSpeed.z;
			//prefab = Instantiate(prefab, bulletPosition, Quaternion.identity) as GameObject;
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Instantiate(prefab, bulletPosition, Quaternion.identity);
			//bulletPosition = new Vector3(0, 2, 12);

			//prefab = Instantiate(bullet, bulletPosition, transform.rotation) as GameObject;

			//prefab.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);

		if (prefab != null && (prefab.transform.position.y <= -2.45))  {
			//Destroy (prefab);
			isThrown = false;
			throwSpeed = new Vector3(0, 26, 40);
		}
	}
}
