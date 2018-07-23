using UnityEngine;

public class BloodStreakManager : MonoBehaviour
{

    public GameObject bloodStreakPrefab;
    private BloodStreak currentBloodStreak;
    
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) { return; }
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            CharacterController2D player = GetComponent<CharacterController2D>();
            if (player != null)
            {
                if (collision.otherCollider.GetType() != typeof(CircleCollider2D))
                {
                    return;
                }
            }
            CreateBloodStreak(GetCollisionCenter(collision));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!enabled) { return; }
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && currentBloodStreak != null)
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (Mathf.Approximately(Mathf.Abs(collision.contacts[i].normal.y), 1.0f))
                {
                    currentBloodStreak.AddLocation(collision.contacts[i].point);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!enabled) { return; }
        if (currentBloodStreak != null && collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            currentBloodStreak.Detach();
            currentBloodStreak = null;
        }
    }

    private void CreateBloodStreak(Vector2 position)
    {
        if (currentBloodStreak != null)
        {
            currentBloodStreak.Detach();
        }
        GameObject streakGo = Instantiate(bloodStreakPrefab, position, Quaternion.identity);
        currentBloodStreak = streakGo.GetComponent<BloodStreak>();
        currentBloodStreak.AddLocation(position);
    }

    private void OnDisable()
    {
        if(currentBloodStreak != null)
        {
            currentBloodStreak.Detach();
            currentBloodStreak = null;
        }
    }

    private void OnEnable()
    {
        CharacterController2D player = GetComponent<CharacterController2D>();
        if (player != null)
        {
            if (player.m_Grounded) { CreateBloodStreak(transform.Find("SkatingPivot").position); }
        }
    }
}
