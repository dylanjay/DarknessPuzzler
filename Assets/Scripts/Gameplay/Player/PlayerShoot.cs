using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float force;

    BodyHandler bodyHandler;

    void Awake()
    {
        bodyHandler = GetComponent<BodyHandler>();

        Debug.Log("CONTROLS");
        Debug.Log("Left Click: Pick up body");
        Debug.Log("Right Click: Throw body");
    }

    void Update () {
		if (bodyHandler.equipped && Input.GetButtonDown("Fire2")) {
            Transform body = bodyHandler.body;
            bodyHandler.UnEquip();
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dir.x < 0) dir.x = -1;
            if (dir.x > 0) dir.x = 1;
            if (dir.y > 0) dir.y = 1;
            if (dir.y < 0) dir.y = 0;
            body.GetComponent<Rigidbody2D>().AddForce(dir.normalized*force, ForceMode2D.Impulse);
        }
	}
}
