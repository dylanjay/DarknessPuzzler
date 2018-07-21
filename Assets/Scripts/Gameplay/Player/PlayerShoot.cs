﻿using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float force;

    BodyHandler bodyHandler;
    GravityFlip gravityFlip;
    CharacterController2D controller;

    void Awake()
    {
        bodyHandler = GetComponent<BodyHandler>();
        controller = GetComponent<CharacterController2D>();

        Debug.Log("CONTROLS");
        Debug.Log("Mouse 1: Pick up body");
        Debug.Log("Mouse 2: Throw body");
        Debug.Log("Mouse 3: Reverse gravity");
    }

    void Start()
    {
        gravityFlip = GravityFlip.instance;
    }

    void FixedUpdate () {
		if (bodyHandler.equipped == EquippedType.Hold && Input.GetButtonDown("Fire2"))
        {
            Transform body = bodyHandler.body;
            bodyHandler.UnEquip();
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dir == Vector2.zero) dir.x = controller.m_FacingRight ? 1 : -1;
            if (dir.x < 0) dir.x = -1;
            if (dir.x > 0) dir.x = 1;
            if (dir.y > 0) dir.y = gravityFlip.flipped ? 0 : 1;
            if (dir.y < 0) dir.y = gravityFlip.flipped ? -1 : 0;
            body.GetComponent<Rigidbody2D>().AddForce(dir.normalized*force, ForceMode2D.Impulse);
        }
	}
}
