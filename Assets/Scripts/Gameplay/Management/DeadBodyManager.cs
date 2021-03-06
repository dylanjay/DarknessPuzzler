﻿using System.Collections;
using UnityEngine;

public class DeadBodyManager : MonoBehaviour 
{
    public GameObject deadbodyPrefab;
    public static DeadBodyManager instance;
    [System.NonSerialized]
    public Transform deadbody;

    void Awake()
    {
        instance = this;
    }

    public GameObject SpawnDeadBody(Vector3 position)
    {
        return Instantiate(deadbodyPrefab, position, Quaternion.identity);
    }

    public void CreateBody(Transform reference)
    {
        if (deadbody != null)
        {
            DestroyBody(deadbody.gameObject);
        }

        deadbody = Instantiate(deadbodyPrefab, reference.position, reference.rotation).transform;
    }

    public void DestroyBody()
    {
        if (deadbody == null) return;

        Destroy(deadbody.gameObject);
        deadbody = null;
    }

    public void DestroyBody(GameObject obj)
    {
        obj.transform.position = new Vector3(-10000, -10000);
        StartCoroutine(KillAfterFrame(obj));
    }

    IEnumerator KillAfterFrame(GameObject obj)
    {
        yield return null;
        Destroy(obj);
    }
}
