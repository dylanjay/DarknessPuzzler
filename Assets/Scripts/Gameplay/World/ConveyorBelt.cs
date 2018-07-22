using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour 
{
    public bool goingRight = true;
    public float speed;
    List<Transform> moveObjects = new List<Transform>();

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
    //    {
    //        collision.GetComponent<Rigidbody2D>().AddForce(speed * (goingRight ? Vector3.right : Vector3.left));
    //    }
    //}

    void Update()
    {
        for (int i = 0; i < moveObjects.Count; i++)
        {
            if (moveObjects[i].GetComponent<PlayerMovement>())
            {
                moveObjects[i].GetComponent<PlayerMovement>().horizontalVelocity += speed * (goingRight ? 1 : -1);
            }
            else
            {
                moveObjects[i].GetComponent<Rigidbody2D>().AddForce(speed * (goingRight ? Vector3.right : Vector3.left));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
        {
            // TODO fix pickup collider activating conveyor belt
            moveObjects.Add(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
        {
            moveObjects.Remove(collision.transform);
        }
    }
}
