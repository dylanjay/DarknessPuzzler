using UnityEngine;

public class PlayerView : MonoBehaviour 
{
    SpriteRenderer sprite;
    GravityFlip gravityFlip;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gravityFlip = GravityFlip.instance;
        gravityFlip.onFlip.AddListener(OnFlip);
    }

    void OnFlip()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, -transform.up);
    }
}
