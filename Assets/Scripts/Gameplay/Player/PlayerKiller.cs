using System.Collections;
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
        Camera.main.GetComponent<CameraShake>().shakeCamera(1);
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForEndOfFrame();
        respawner.Respawn();
    }

}
