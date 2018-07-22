using UnityEngine;

public class PlayerSkate : MonoBehaviour 
{
    public float speed;

    BodyHandler bodyHandler;
    CharacterController2D controller;
    PlayerMovement playerMovement;
    Rigidbody2D rigidBody;
    public bool on = false;
    float force;

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
        else if (on && bodyHandler.equipped != EquippedType.Skate)
        {
            DeSkate();
        }

        if (on)
        {
            playerMovement.horizontalVelocity += force;
            if (Input.GetButtonDown("Jump") && !controller.m_Grounded) 
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                bodyHandler.UnEquip();
            }
        }
    }
}
