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
        if (collision.gameObject.tag == "Pickupable" && collision.gameObject.layer == LayerMask.NameToLayer("DeadBody"))
        {
            if (Input.GetButtonDown("Pickup"))
            {
                bodyHandler.Equip(collision.transform, bodyHandler.holdingPivot, EquippedType.Hold);
            }
        }
    }
}
