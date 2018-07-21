using UnityEngine;

public class PlayerSkate : MonoBehaviour 
{
    public float speed;

    BodyHandler bodyHandler;
    CharacterController2D controller;
    PlayerMovement playerMovement;
    bool on = false;
    float force;

    void Awake()
    {
        bodyHandler = GetComponent<BodyHandler>();
        controller = GetComponent<CharacterController2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!on && bodyHandler.equipped == EquippedType.Skate)
        {
            force = controller.m_FacingRight ? speed : -speed;
            on = true;
            playerMovement.disableHorizontalInput = true;
        }
        else if (on && bodyHandler.equipped != EquippedType.Skate)
        {
            on = false;
            playerMovement.disableHorizontalInput = false;
        }

        if (on)
        {
            playerMovement.horizontalVelocity += force;
        }
    }
}
