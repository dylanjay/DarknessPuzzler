﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float runSpeed;

    CharacterController2D controller;
    GravityFlip gravityFlip;
    BodyHandler bodyHandler;
    AudioSource audioSource;
    SoundManager soundManager;

    public float horizontalVelocity = 0f;
    public bool disableHorizontalInput = false;
    bool jump = false;
    bool doubleJump = false;

    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        bodyHandler = GetComponent<BodyHandler>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        soundManager = SoundManager.instance;
        gravityFlip = GravityFlip.instance;
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    void DebugGravityFlip()
    {
        //if (Input.GetButtonDown("Flip") && controller.m_Grounded)
        //{
        //    gravityFlip.Flip();
        //}
    }

    void Update()
    {
        horizontalVelocity = disableHorizontalInput ? 0f : Input.GetAxisRaw("Horizontal") * runSpeed;
        DebugGravityFlip();
        if (Input.GetButtonDown("Jump") && (controller.m_Grounded || bodyHandler.equipped == EquippedType.Skate))
        {
            jump = true;
            audioSource.pitch = Random.Range(1f, 1.2f);
            soundManager.PlayOneShot(audioSource, "Jump");
            if (!controller.m_Grounded && bodyHandler.equipped == EquippedType.Skate)
            {
                doubleJump = true;
            }
        }
    }

    void FixedUpdate()
    {
        //if (doubleJump) { controller.m_JumpForce *= 4; Debug.Log("here"); }
        controller.Move(horizontalVelocity * Time.fixedDeltaTime, false, jump);
        //if (doubleJump) controller.m_JumpForce /= 4;
        doubleJump = false;
        jump = false;
    }
}
