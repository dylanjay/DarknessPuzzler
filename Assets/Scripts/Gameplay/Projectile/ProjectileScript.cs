using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer != LayerMask.NameToLayer("Player")){
            Debug.Log(col.gameObject.name);
            Destroy(gameObject);
        }
    }
}
