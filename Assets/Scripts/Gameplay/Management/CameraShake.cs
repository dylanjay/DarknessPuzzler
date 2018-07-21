using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float maxAngle = 5;
    public float maxOffset = 1;

    //initial values
    bool shaking;
    Vector3 _basePos;
    Vector3 _baseRot;
    
    float trauma = 0;

	// Use this for initialization
	void Start () {
        shaking = false;
        Random.InitState( (int)System.DateTime.Now.Ticks );

        _basePos = transform.localPosition;
        _baseRot = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		if(trauma > 0)
        {
            getShakeTranslation();
            trauma -= 0.01f;
        }
        else
        {
            resetCameraPosition();
        }
	}

    void resetCameraPosition()
    {
        transform.localPosition = _basePos;
        transform.localEulerAngles = _baseRot;
    }
    
    void getShakeTranslation()
    {
        float shake = Mathf.Pow(trauma, 3.0f);
        float offsetX = maxOffset * shake * Random.value;
        float offsetY = maxOffset * shake * Random.value;
        float angle = maxAngle * shake * Random.value;
        
        Vector3 newEulerAngle = new Vector3(_baseRot.x, _baseRot.y, _baseRot.z + angle);
        Vector3 newPosition =  new Vector3(_basePos.x + offsetX, _basePos.y + offsetY, _basePos.z);
        transform.localEulerAngles = newEulerAngle;
        transform.localPosition = newPosition;
    }

    public void shakeCamera(float t)
    {
        trauma = t;
        if (trauma > 1)
        {
            trauma = 1;
        }
    }
}
