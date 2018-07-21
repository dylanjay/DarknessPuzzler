using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour 
{
    public UnityEvent onPress;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onPress.Invoke();
        }
    }
}
