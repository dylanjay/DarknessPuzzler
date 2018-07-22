using UnityEngine;

public class DeadBody : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject bloodPoolPrefab;
    public GameObject bloodSpotPrefab;
    private Vector3 lastPoolPosition = Vector3.positiveInfinity;
    private Vector3 lastSpotPosition = Vector3.positiveInfinity;
    private float lastSpotTime = 0;

    [Tooltip("Minimum distance needed to travel before making more blood.")]
    public float minDistance = 2;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        spriteRenderer.flipY = Physics2D.gravity.y < 0;
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Vector2 center = GetCollisionCenter(collision);
            Vector2 normal = collision.contacts[0].normal;
            if (Vector2.Distance(lastPoolPosition, center) > minDistance)
            {
                lastPoolPosition = center;
                CreateBloodPool(center, normal, collision.relativeVelocity.magnitude);
            }
            else
            {
                lastSpotTime = Time.time;
                CreateBloodSpot(center);
            }
        }
    }

    private Vector2 GetCollisionCenter(Collision2D collision)
    {
        ContactPoint2D[] points = collision.contacts;
        Vector2 center = Vector3.zero;
        int length = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            center += points[i].point;
        }
        return center / length;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (lastSpotTime == Time.time) { return; }
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Vector2 center = GetCollisionCenter(collision);
            if (Vector2.Distance(lastSpotPosition, center) > minDistance / 50)
            {
                lastSpotTime = Time.time;
                lastSpotPosition = center;
                CreateBloodSpot(center);
            }
        }
    }

    private void CreateBloodPool(Vector2 position, Vector2 normal, float force)
    {
        GameObject poolGo = Instantiate(bloodPoolPrefab, 
                                        position + (normal * .05f), 
                                        Quaternion.LookRotation(Vector3.forward, normal));

        BloodPool bloodPool = poolGo.GetComponent<BloodPool>();
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        bloodPool.bias = Mathf.Clamp(velocity.x * -.05f, -.5f, .5f) + .5f;
        bloodPool.amount = Mathf.Sqrt(force);
    }

    private void CreateBloodSpot(Vector2 position)
    {
        //GameObject poolGo = Instantiate(bloodSpotPrefab, position, Quaternion.identity);
    }
}
