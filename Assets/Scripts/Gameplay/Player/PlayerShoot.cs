using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject projectilePrefab;
    public float speed;
    public Vector2 dir;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetButtonDown("Fire1")) {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(dir*speed, ForceMode2D.Impulse);
        }
	}
}
