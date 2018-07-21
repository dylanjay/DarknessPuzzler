using UnityEngine;

public enum EquippedType { None, Hold, Skate }

public class BodyHandler : MonoBehaviour
{
    [System.NonSerialized]
    public EquippedType equipped = EquippedType.None;
    [System.NonSerialized]
    public Transform body;

    public Transform holdingPivot;
    public Transform skatingPivot;
    CharacterController2D controller;
    bool facingRight = true;

    void Awake()
    {
        holdingPivot = transform.Find("HoldingPivot");
        skatingPivot = transform.Find("SkatingPivot");
        controller = GetComponent<CharacterController2D>();
    }

    void Start()
    {
        GravityFlip.instance.onFlip.AddListener(Flip);
        controller.OnLandEvent.AddListener(TrySkate);
    }

    void Update()
    {
        if (controller.m_FacingRight != facingRight)
        {
            facingRight = controller.m_FacingRight;
            Flip();
        }
    }

    void TrySkate()
    {
        if (controller.landedOn.gameObject.layer == LayerMask.NameToLayer("DeadBody"))
        {
            Equip(controller.landedOn.transform, skatingPivot, EquippedType.Skate);
        }
    }

    void Flip()
    {
        holdingPivot.localPosition = new Vector3(-holdingPivot.localPosition.x, holdingPivot.localPosition.y, holdingPivot.localPosition.z);
    }

    public void Equip(Transform body, Transform pivot, EquippedType type)
    {
        //body.GetComponent<BoxCollider2D>().enabled = false;
        equipped = type;
        this.body = body;
        body.position = pivot.position;
        body.SetParent(pivot);
        body.GetComponent<Rigidbody2D>().isKinematic = true;
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), body.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), body.GetComponent<BoxCollider2D>());
    }

    public void UnEquip()
    {
        //body.GetComponent<BoxCollider2D>().enabled = true;
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), body.GetComponent<BoxCollider2D>(), false);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), body.GetComponent<BoxCollider2D>(), false);
        equipped = EquippedType.None;
        body.SetParent(transform.parent);
        body.GetComponent<Rigidbody2D>().isKinematic = false;
        body = null;
    }
}
