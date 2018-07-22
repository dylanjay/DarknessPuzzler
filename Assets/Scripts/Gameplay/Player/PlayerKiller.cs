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
        GetComponent<BodyHandler>().UnEquip();
        GetComponent<PlayerSkate>().DeSkate();
        deadbodyManager.CreateBody(transform);
        Camera.main.GetComponent<CameraShake>().shakeCamera(1);
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForEndOfFrame();
        respawner.Respawn();
    }

}
