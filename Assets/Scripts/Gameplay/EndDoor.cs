using UnityEngine;

public class EndDoor : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PRETEND THAT LOADED THE NEXT LEVEL");

            collision.gameObject.GetComponentInParent<PlayerRespawner>().Respawn();
            // TODO
            //LevelLoader.instance.LoadNextLevel();
        }
    }
}
