using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour 
{
    BodyHandler bodyHandler;

    void Awake()
    {
        bodyHandler = GetComponentInParent<BodyHandler>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Fire1") && collision.gameObject.tag == "Pickupable")
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("DeadBody"))
            {
                bodyHandler.Equip(collision.transform);
            }
        }
    }
}
