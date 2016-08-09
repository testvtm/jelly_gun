using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public GameObject barrel;
    private bool facingRight = true;

    void Update () {
        Vector3 pos = Camera.main.WorldToScreenPoint(barrel.transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if ((angle >= -20 && angle <= 130)) {
            barrel.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 thescale = transform.localScale;
        thescale.y = -thescale.y;
        //barrel.transform.localScale = thescale;
    }
}
