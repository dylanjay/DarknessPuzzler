using UnityEngine;

public class PlayerKiller : MonoBehaviour 
{
    PlayerRespawner respawner;
    DeadbodyManager deadbodyManager;

    void Awake()
    {
        respawner = GetComponent<PlayerRespawner>();
    }

    void Start()
    {
        deadbodyManager = DeadbodyManager.instance;
    }

    public void Kill()
    {
        deadbodyManager.CreateBody(transform);
        Debug.Break();
        respawner.Respawn();
    }
}
