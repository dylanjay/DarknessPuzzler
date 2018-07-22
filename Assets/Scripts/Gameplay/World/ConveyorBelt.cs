using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public bool goingRight = true;
    public float speed;
    List<Transform> moveObjects = new List<Transform>();
    CharacterController2D controller;

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
    //    {
    //        collision.GetComponent<Rigidbody2D>().AddForce(speed * (goingRight ? Vector3.right : Vector3.left));
    //    }
    //}

    void Update()
    {
        if (controller != null)
        {
            if (controller.m_Grounded)
            {
                controller = null;
            }
            else
            {
                Debug.Log("CONVEYOR BELT WORKED");
                controller.GetComponent<PlayerMovement>().horizontalVelocity += speed * (goingRight ? 1 : -1);
            }
        }

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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )//&& !collision.gameObject.GetComponent<CharacterController2D>().m_Grounded)
        {
            //if (collision.gameObject.GetComponent<CharacterController2D>().m_Grounded)
            //Debug.Log(collision.gameObject.GetComponent<CharacterController2D>().groundedOn);
            controller = collision.gameObject.GetComponent<CharacterController2D>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
        {
            // TODO fix pickup collider activating conveyor belt
            moveObjects.Add(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
        {
            moveObjects.Remove(collision.transform);
        }
    }
}
