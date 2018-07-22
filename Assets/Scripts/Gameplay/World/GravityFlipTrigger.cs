using UnityEngine;

public class GravityFlipTrigger : MonoBehaviour
{
    public float reactivateDistance;

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
        if (active && collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            active = false;
            gravityFlip.Flip();
        }
    }
}
