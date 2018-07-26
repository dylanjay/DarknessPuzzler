using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawner : MonoBehaviour 
{
    public UnityEvent onRespawn = new UnityEvent();

    public Transform spawnPoint;

    void Awake()
    {
        spawnPoint.SetParent(transform.parent);
    }

    public void Respawn()
    {
        onRespawn.Invoke();
        transform.position = spawnPoint.position;
    }
}
