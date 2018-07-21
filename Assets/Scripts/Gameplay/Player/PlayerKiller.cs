using System.Collections;
using UnityEngine;

public class PlayerKiller : MonoBehaviour 
{
    PlayerRespawner respawner;
    DeadbodyManager deadbodyManager;
    public float shakeAmt = 0;
    Camera mainCamera;
    Vector3 originalCameraPosition;

    void Awake()
    {
        respawner = GetComponent<PlayerRespawner>();
        mainCamera = Camera.main;
    }

    void Start()
    {
        deadbodyManager = DeadbodyManager.instance;
    }

    public void Kill()
    {
        deadbodyManager.CreateBody(transform);
        originalCameraPosition = mainCamera.transform.position;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForEndOfFrame();

        respawner.Respawn();
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }
}
