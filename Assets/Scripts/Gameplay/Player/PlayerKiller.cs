using System.Collections;
using UnityEngine;

public class PlayerKiller : MonoBehaviour 
{
    PlayerRespawner respawner;
    DeadBodyManager deadbodyManager;
    GravityFlip gravityFlip;

    void Awake()
    {
        respawner = GetComponent<PlayerRespawner>();
    }

    void Start()
    {
        deadbodyManager = DeadBodyManager.instance;
        gravityFlip = GravityFlip.instance;
    }

    public void Kill()
    {
        GetComponent<BodyHandler>().UnEquip();
        GetComponent<PlayerSkate>().DeSkate();
        deadbodyManager.CreateBody(transform);
        if (gravityFlip.flipped)
        {
            gravityFlip.Flip();
        }
        Camera.main.GetComponent<CameraShake>().shakeCamera(1);
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForEndOfFrame();
        respawner.Respawn();
    }

}
