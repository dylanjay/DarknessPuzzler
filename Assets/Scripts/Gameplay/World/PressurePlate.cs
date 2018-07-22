using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour 
{
    public UnityEvent onPress;
    public UnityEvent onRelease;

    int enterCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        enterCount++;
        onPress.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enterCount--;
        if (enterCount == 0)
        {
            onRelease.Invoke();
        }
    }
}
