using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public GameObject bloodPoolPrefab;
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPoolPosition = Vector3.positiveInfinity;

    [Tooltip("Minimum distance needed to travel before making more blood.")]
    public float minDistance = 2;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        //spriteRenderer.flipY = Physics2D.gravity.y < 0;
        //transform.rotation = Quaternion.identity;
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

    private void CreateBloodPool(Vector2 position, Vector2 normal, float force)
    {
        Debug.DrawLine(position, position + normal, Color.green, 100);
        GameObject poolGo = Instantiate(bloodPoolPrefab, 
                                        position + (normal * .1f), 
                                        Quaternion.LookRotation(Vector3.forward, normal));

        BloodPool bloodPool = poolGo.GetComponent<BloodPool>();
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        bloodPool.amount = Mathf.Sqrt(force);
        if (Mathf.Abs(normal.x) > .9f)
        {
            Debug.Log("It's vertical");
            bloodPool.bias = Mathf.Sign(Physics2D.gravity.y * normal.x) > 0 ? 1 : 0;
        }
    }
}
