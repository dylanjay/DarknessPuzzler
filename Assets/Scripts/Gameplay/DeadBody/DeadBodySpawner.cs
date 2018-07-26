using UnityEngine;

public class DeadBodySpawner : MonoBehaviour 
{
    [SerializeField]
    Transform deadBody;

    void Start()
    {
        //deadBody = DeadBodyManager.instance.SpawnDeadBody(transform.position).transform;
        Player.instance.GetComponent<PlayerRespawner>().onRespawn.AddListener(Respawn);
    }

    public void Respawn()
    {
        deadBody.position = transform.position;
    }
}
