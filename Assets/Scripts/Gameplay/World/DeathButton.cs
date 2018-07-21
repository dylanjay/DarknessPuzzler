using UnityEngine;

public class DeathButton : MonoBehaviour 
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Fire2"))
        {
            collision.GetComponent<PlayerKiller>().Kill();
        }
    }
}
