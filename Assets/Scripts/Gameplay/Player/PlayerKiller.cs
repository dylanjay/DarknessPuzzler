using System.Collections;
using UnityEngine;

public class PlayerKiller : MonoBehaviour 
{
    PlayerRespawner respawner;
    DeadBodyManager deadbodyManager;
    GravityFlip gravityFlip;
    SoundManager soundManager;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        respawner = GetComponent<PlayerRespawner>();
    }

    void Start()
    {
        deadbodyManager = DeadBodyManager.instance;
        gravityFlip = GravityFlip.instance;
        soundManager = SoundManager.instance;
    }

    public void Kill()
    {
        audioSource.pitch = Random.Range(1f, 1.2f);
        soundManager.PlayOneShot(audioSource, "death");
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
