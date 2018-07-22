using UnityEngine;

public class PlayerSkate : MonoBehaviour 
{
    public float speed;

    BodyHandler bodyHandler;
    CharacterController2D controller;
    PlayerMovement playerMovement;
    Rigidbody2D rigidBody;
    [System.NonSerialized]
    public bool on = false;
    float force;
    bool jumpThrow = false;
    bool goingRight = true;

    void Awake()
    {
        bodyHandler = GetComponent<BodyHandler>();
        controller = GetComponent<CharacterController2D>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Skate()
    {
        bodyHandler.Equip(bodyHandler.body, EquippedType.Skate);
        force = controller.m_FacingRight ? speed : -speed;
        on = true;
        playerMovement.disableHorizontalInput = true;
    }

    public void DeSkate()
    {
        on = false;
        playerMovement.disableHorizontalInput = false;
    }

    void Update()
    {
        if (bodyHandler.equipped == EquippedType.Hold && Input.GetButtonDown("Interact"))
        {
            Skate();
        }
        else if (on)
        {
            playerMovement.horizontalVelocity += force;

            if (bodyHandler.equipped != EquippedType.Skate && !jumpThrow)
            {
                bodyHandler.UnEquip();
                DeSkate();
            }
            else if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Pickup"))
            {
                bodyHandler.Equip(bodyHandler.body, EquippedType.Hold);
                DeSkate();
            }
            else if (Input.GetButtonDown("Throw"))
            {
                bodyHandler.UnEquip();
                playerMovement.disableHorizontalInput = false;
                jumpThrow = true;
                goingRight = controller.m_FacingRight;
            }
            else if (Input.GetButtonDown("Jump") && !controller.m_Grounded)
            {
                //rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                //bodyHandler.UnEquip();
                bodyHandler.body.GetComponent<DeadBodyCollision>().IgnoreCollision(transform.Find("Colliders"));
                bodyHandler.UnEquip();
                jumpThrow = true;
                playerMovement.disableHorizontalInput = false;
                goingRight = controller.m_FacingRight;
            }
        }

        if (jumpThrow && (controller.m_Grounded ||
            (Input.GetAxisRaw("Horizontal") < 0 && goingRight) ||
            (Input.GetAxisRaw("Horizontal") > 0 && !goingRight)))
        {
            on = false;
            jumpThrow = false;
        }
    }
}
