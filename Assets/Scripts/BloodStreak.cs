using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class BloodStreak : MonoBehaviour
{

    [Tooltip("The amount of time between the last time the body has been kicked and when the blood fades.")]
    public float keepAliveTime = 10;

    private const float MAX_OFF_DISTANCE = 0.2f;
    public LineRenderer lineRenderer;
    private float startDissipateTime = float.PositiveInfinity;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}

    private void Update()
    {
        if (Time.time - startDissipateTime >= keepAliveTime)
        {
            StartCoroutine(Dissipate());
            startDissipateTime = float.PositiveInfinity;
        }
    }

    public void Detach()
    {
        startDissipateTime = Time.time;
    }

	public void AddLocation(Vector3 point)
    {
        Vector3 localSpacePoint = Quaternion.Inverse(transform.rotation) * (point - transform.position);
        localSpacePoint.z = 1;
        if (Mathf.Clamp(localSpacePoint.y, -MAX_OFF_DISTANCE, MAX_OFF_DISTANCE) == localSpacePoint.y)
        {
            Vector3 startPosition = lineRenderer.GetPosition(0);
            Vector3 endPosition = lineRenderer.GetPosition(1);
            if (localSpacePoint.x > lineRenderer.GetPosition(0).x && localSpacePoint.x > lineRenderer.GetPosition(1).x)
            {
                int closestPoint = startPosition.x > endPosition.x ? 0 : 1;
                lineRenderer.SetPosition(closestPoint, localSpacePoint);
                Vector3 otherPoint = lineRenderer.GetPosition(1 - closestPoint);
                otherPoint.y = localSpacePoint.y;
                lineRenderer.SetPosition(1 - closestPoint, otherPoint);
            }
            else if (localSpacePoint.x < lineRenderer.GetPosition(0).x && localSpacePoint.x < lineRenderer.GetPosition(1).x)
            {
                int closestPoint = startPosition.x > localSpacePoint.x ? 0 : 1;
                lineRenderer.SetPosition(closestPoint, localSpacePoint);
                Vector3 otherPoint = lineRenderer.GetPosition(1 - closestPoint);
                otherPoint.y = localSpacePoint.y;
                lineRenderer.SetPosition(1 - closestPoint, otherPoint);
            }
        }
    }

    private IEnumerator Dissipate()
    {
        Material material = lineRenderer.material;
        float revealAmount = 1.0f;
        while (revealAmount > 0)
        {
            revealAmount -= Time.deltaTime;
            material.SetFloat("_RevealAmount", revealAmount);
            yield return null;
        }
        material.SetFloat("_RevealAmount", 0);

        yield return null;
        Destroy(gameObject);
    }
}
