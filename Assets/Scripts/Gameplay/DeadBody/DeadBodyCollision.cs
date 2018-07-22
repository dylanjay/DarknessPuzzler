using UnityEngine;
using UnityEngine.Events;

public class DeadBodyCollision : MonoBehaviour 
{
    public bool active = true;
    public UnityEvent collided = new UnityEvent();
    Transform ignoredObj;

    void OnCollisionEnter2D(Collision2D collision)
    {
        RestoreCollision();
    }

    public void IgnoreCollision(Transform obj)
    {
        ignoredObj = obj;
        Collider2D[] colliders = obj.GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Physics2D.IgnoreCollision(colliders[i], GetComponent<BoxCollider2D>());
        }
        active = false;
        collided.AddListener(RestoreCollision);
    }

    public void RestoreCollision()
    {
        if (ignoredObj == null) return;
        active = true;
        collided.RemoveAllListeners();
        Collider2D[] colliders = ignoredObj.GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Physics2D.IgnoreCollision(colliders[i], GetComponent<BoxCollider2D>(), false);
        }
        ignoredObj = null;
    }
}
