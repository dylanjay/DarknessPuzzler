using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    [System.NonSerialized]
    public bool equipped = false;
    [System.NonSerialized]
    public Transform body;

    Transform bodyHolder;

    void Awake()
    {
        bodyHolder = transform.Find("BodyHolder");
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
