using UnityEngine;

public class PlayerRespawner : MonoBehaviour 
{
    public Transform spawnPoint;

    public void Respawn()
    {
        transform.position = spawnPoint.position;
    }
}
