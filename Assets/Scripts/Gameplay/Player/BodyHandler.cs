using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    [System.NonSerialized]
    public bool equipped = false;
    [System.NonSerialized]
    public Transform body;

    Transform bodyHolder;
    CharacterController2D controller;
    bool facingRight = true;

    void Awake()
    {
        bodyHolder = transform.Find("BodyHolder");
        controller = GetComponent<CharacterController2D>();
    }

    void Start()
    {
        GravityFlip.instance.onFlip.AddListener(Flip);
    }

    void Update()
    {
        if (controller.m_FacingRight != facingRight)
        {
            facingRight = controller.m_FacingRight;
            Flip();
        }
    }

    void Flip()
    {
        bodyHolder.localPosition = new Vector3(-bodyHolder.localPosition.x, bodyHolder.localPosition.y, bodyHolder.localPosition.z);
    }

    public void Equip(Transform body)
    {
        body.GetComponent<BoxCollider2D>().enabled = false;
        equipped = true;
        this.body = body;
        body.position = bodyHolder.position;
        body.SetParent(bodyHolder);
        body.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void UnEquip()
    {
        body.GetComponent<BoxCollider2D>().enabled = true;
        equipped = false;
        body.SetParent(transform.parent);
        body.GetComponent<Rigidbody2D>().isKinematic = false;
        body = null;
    }
}
