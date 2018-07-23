using UnityEngine;
using UnityEngine.Events;

public class GravityFlipTrigger : MonoBehaviour
{
    public float reactivateDistance = 2;
    public UnityEvent onFlipPressed;
    GravityFlip gravityFlip;
    bool active = true;
    Transform player;

    void Start()
    {
        gravityFlip = GravityFlip.instance;
    }

    void Update()
    {
        if (!active)
        {
            if (Vector2.Distance(transform.position, player.position) > reactivateDistance)
            {
                active = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable"))
        {
            player = collision.transform;
            active = false;
            gravityFlip.Flip();
            onFlipPressed.Invoke();
        }
    }
}
