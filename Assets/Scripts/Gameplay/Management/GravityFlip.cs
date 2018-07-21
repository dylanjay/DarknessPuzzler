using UnityEngine;
using UnityEngine.Events;

public class GravityFlip : MonoBehaviour 
{
    public static GravityFlip instance;

    public UnityEvent onFlip = new UnityEvent();

    [System.NonSerialized]
    public bool flipped;

    void Awake()
    {
        instance = this;
    }

    public void Flip()
    {
        Physics2D.gravity = new Vector2(0, -Physics2D.gravity.y);
        flipped = !flipped;
        onFlip.Invoke();
    }
}
