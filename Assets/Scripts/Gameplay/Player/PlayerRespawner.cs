using UnityEngine;

public class PlayerRespawner : MonoBehaviour 
{
    public Transform spawnPoint;

    void Awake()
    {
        spawnPoint.SetParent(transform.parent);
    }


    public void Respawn()
    {
        transform.position = spawnPoint.position;
    }
}
