using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float runSpeed;

    CharacterController2D controller;
    GravityFlip gravityFlip;

    public float horizontalVelocity = 0f;
    public bool disableHorizontalInput = false;
    bool jump = false;

    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Start()
    {
        gravityFlip = GravityFlip.instance;
    }

    void Update()
    {
        horizontalVelocity = disableHorizontalInput ? 0f : Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Flip") && controller.m_Grounded)
        {
            gravityFlip.Flip();
        }

        if (Input.GetButtonDown("Jump") && controller.m_Grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalVelocity * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
