using UnityEngine;

public class DeadBody : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        spriteRenderer.flipY = Physics2D.gravity.y < 0;
        transform.rotation = Quaternion.identity;
    }
}
