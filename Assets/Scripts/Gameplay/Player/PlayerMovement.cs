using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float runSpeed;

    CharacterController2D controller;

    float horizontalMove = 0f;
    bool jump = false;

    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        

        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump = true;
        //}
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
