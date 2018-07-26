using System.Collections;
using UnityEngine;

public class PlayerKiller : MonoBehaviour 
{
    PlayerRespawner respawner;
    DeadBodyManager deadbodyManager;
    GravityFlip gravityFlip;
    SoundManager soundManager;
    AudioSource audioSource;
    bool killable = true;

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
        if (!killable) return;

        audioSource.pitch = Random.Range(1f, 1.2f);
        soundManager.PlayOneShot(audioSource, "Death");
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
        killable = false;
        yield return new WaitForEndOfFrame();
        respawner.Respawn();
        killable = true;
    }

}
