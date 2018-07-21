using UnityEngine;

public class DeadbodyManager : MonoBehaviour 
{
    public GameObject deadbodyPrefab;
    public static DeadbodyManager instance;
    [System.NonSerialized]
    public Transform deadbody;

    void Awake()
    {
        instance = this;
    }

    public void CreateBody(Transform reference)
    {
        if (deadbody != null)
        {
            Destroy(deadbody.gameObject);
        }

        deadbody = Instantiate(deadbodyPrefab, reference.position, reference.rotation).transform;
    }

    public void DestroyBody()
    {
        if (deadbody == null) return;

        Destroy(deadbody.gameObject);
        deadbody = null;
    }
}
