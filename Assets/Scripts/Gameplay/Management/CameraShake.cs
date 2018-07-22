using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float maxAngle = 5;
    public float maxOffset = 1;

    //initial values
    bool shaking;
    Vector3 _basePos;
    Vector3 _baseRot;
    float height;
    float width;
    
    float trauma = 0;

    //move values
    GameObject env;
    GameObject target;
    Vector3 topLeft = Vector3.zero;
    Vector3 bottomRight = Vector3.zero;
    Vector3 center = Vector3.zero;
    Vector3 extents = Vector3.zero;

    void Start () {
        env = GameObject.Find("Environment");
        target = GameObject.Find("Player");
        shaking = false;
        Random.InitState( (int)System.DateTime.Now.Ticks );
        _basePos = transform.position;
        _baseRot = transform.eulerAngles;
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void Update()
    {
        if (topLeft == Vector3.zero && bottomRight == Vector3.zero)
        {
            defineCorners();
        }

        if (target == null)
        {
            defineTarget();
        }

        Vector3 offset = Vector3.zero;
        if (trauma > 0 && shaking)
        {
            offset = getShakeTranslation();
            trauma -= 0.01f;
        }
        //else if (trauma > 0 && !shaking)
        //{
        //    _basePos = transform.localPosition;
        //    _baseRot = transform.localEulerAngles;
        //    shaking = true;
        //}
        else
        {
            shaking = false;
            resetCameraPosition();
        }

        float incrX = (target.transform.position.x - _basePos.x) * .50f * Time.deltaTime;
        float incrY = (target.transform.position.y - _basePos.y) * .50f * Time.deltaTime;

        float newX = _basePos.x + incrX;
        float newY = _basePos.y + incrY;

        newX = newX - width / 2 < topLeft.x ? _basePos.x : newX;
        newX = newX + width / 2 > bottomRight.x ? _basePos.x : newX;

        newY = newY - height / 2 < topLeft.y ? _basePos.y : newY;
        newY = newY + height / 2 > bottomRight.y ? _basePos.y : newY;

        Vector3 update = new Vector3(newX, newY, _basePos.z);
        _basePos = update;
        transform.position = update + offset;
    }

    //void Update ()
    //   {
    //	if(trauma > 0 && shaking)
    //       {
    //           getShakeTranslation();
    //           trauma -= 0.01f;
    //       }
    //       else if (trauma > 0 && !shaking)
    //       {
    //           _basePos = transform.localPosition;
    //           _baseRot = transform.localEulerAngles;
    //           shaking = true;
    //       }
    //       else
    //       {
    //           shaking = false;
    //           resetCameraPosition();

    //           if (topLeft == Vector3.zero && bottomRight == Vector3.zero)
    //           {
    //               defineCorners();
    //           }

    //           if (target == null)
    //           {
    //               defineTarget();
    //           }

    //           float incrX = (target.transform.position.x - transform.position.x) * .10f;
    //           float incrY = (target.transform.position.y - transform.position.y) * .10f;

    //           float newX = transform.position.x + incrX;
    //           float newY = transform.position.y + incrY;

    //           float height = 2f * Camera.main.orthographicSize;
    //           float width = height * Camera.main.aspect; 
    //           newX = newX - width/2 < topLeft.x ? transform.position.x : newX;
    //           newX = newX + width/2 > bottomRight.x ? transform.position.x : newX;

    //           newY = newY - height/2 < topLeft.y ? transform.position.y  : newY;
    //           newY = newY + height/2 > bottomRight.y ? transform.position.y  : newY;

    //           Vector3 update = new Vector3(newX, newY, transform.position.z);
    //           transform.position = update;
    //       }
    //   }

    void resetCameraPosition()
    {
        //transform.localPosition = _basePos;
        transform.localEulerAngles = _baseRot;
    }
    
    Vector3 getShakeTranslation()
    {
        float shake = Mathf.Pow(trauma, 3.0f);
        float offsetX = maxOffset * shake * Random.value;
        float offsetY = maxOffset * shake * Random.value;
        float angle = maxAngle * shake * Random.value;
        
        Vector3 newEulerAngle = new Vector3(_baseRot.x, _baseRot.y, _baseRot.z + angle);
        Vector3 newPosition =  new Vector3(_basePos.x + offsetX, _basePos.y + offsetY, _basePos.z);
        transform.localEulerAngles = newEulerAngle;
        return newPosition;
    }

    public void shakeCamera(float t)
    {
        trauma = t;
        if (trauma > 1)
        {
            trauma = 1;
        }
    }

    void defineCorners()
    {
        center = env.GetComponent<CompositeCollider2D>().bounds.center;
        extents = env.GetComponent<CompositeCollider2D>().bounds.extents;

        topLeft = center - extents;
        bottomRight = center + extents;
    }

    void defineTarget()
    {
        target = GameObject.Find("Player");
    }
}
