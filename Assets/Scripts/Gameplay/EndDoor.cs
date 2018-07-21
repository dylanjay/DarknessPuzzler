using UnityEngine;

public class EndDoor : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PRETEND THAT LOADED THE NEXT LEVEL");

            collision.gameObject.GetComponent<PlayerRespawner>().Respawn();
            // TODO
            //LevelLoader.instance.LoadNextLevel();
        }
    }
}
