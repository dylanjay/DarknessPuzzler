using UnityEngine;

public class ConveyorBelt : MonoBehaviour 
{
    public bool goingRight = true;
    public float speed;

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
    //    {
    //        collision.GetComponent<Rigidbody2D>().AddForce(speed * (goingRight ? Vector3.right : Vector3.left));
    //    }
    //}

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Pickupable")
        {
            // TODO Set constant speed by adding velocity here and to player
            // TODO fix pickup collider activating conveyor belt
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(speed * (goingRight ? Vector3.right : Vector3.left));
        }
    }
}
