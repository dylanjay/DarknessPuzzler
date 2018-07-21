using UnityEngine;

public class ConveyorBelt : MonoBehaviour 
{
    public bool goingRight = true;
    public float speed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().AddForce(speed * (goingRight ? Vector3.right : Vector3.left));
        }
    }
}
