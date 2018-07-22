using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float force;

    BodyHandler bodyHandler;
    GravityFlip gravityFlip;
    CharacterController2D controller;
    Transform body;

    void Awake()
    {
        bodyHandler = GetComponent<BodyHandler>();
        controller = GetComponent<CharacterController2D>();

        Debug.Log("CONTROLS");
        Debug.Log("J: Pick up body");
        Debug.Log("I: Throw body");
        Debug.Log("K: Reverse gravity");
        Debug.Log("L: Interact (skate on body(while holding) / death button)");
    }

    void Start()
    {
        gravityFlip = GravityFlip.instance;
    }

    void FixedUpdate () {
		if ((bodyHandler.equipped == EquippedType.Hold || bodyHandler.equipped == EquippedType.Skate) && Input.GetButtonDown("Throw"))
        {
            body = bodyHandler.body;
            if (bodyHandler.equipped != EquippedType.Skate || controller.m_Grounded)
            {
                bodyHandler.UnEquip();
            }
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dir == Vector2.zero) dir.x = controller.m_FacingRight ? 1 : -1;
            if (dir.x < 0) dir.x = -1;
            if (dir.x > 0) dir.x = 1;
            if (dir.y > 0) dir.y = gravityFlip.flipped ? 0 : 1;
            if (dir.y < 0) dir.y = gravityFlip.flipped ? -1 : 0;
            body.GetComponent<DeadBodyCollision>().IgnoreCollision(transform.Find("Colliders"));
            // TODO inherit speed from player
            body.GetComponent<Rigidbody2D>().AddForce(dir.normalized*force, ForceMode2D.Impulse);
        }
	}    
}
