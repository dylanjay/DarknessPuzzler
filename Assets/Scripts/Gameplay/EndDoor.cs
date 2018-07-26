using System.Collections;
using UnityEngine;

public class EndDoor : MonoBehaviour 
{
    bool active = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
         if (active && collision.gameObject.tag == "Player")
        {
            //Debug.Log("PRETEND THAT LOADED THE NEXT LEVEL");

            //collision.gameObject.GetComponentInParent<PlayerRespawner>().Respawn();
            // TODO
            LevelLoader.instance.LoadNextLevel();
            active = false;
        }
    }
}
