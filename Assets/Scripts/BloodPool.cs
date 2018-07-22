﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour
{

    [Tooltip("How much the blood should flow one way or the other. Useful for inclines and initial velocity.")]
    [Range(0, 1)]
    public float bias = 0.5f;

    [Tooltip("The total distance the blood should cover.")]
    public float amount = 3;

    private float amountUsed = 0;

    [Tooltip("How fast the blood should flow.")]
    public float growthFactor = .1f;

    public LineRenderer lineRenderer;


    public GameObject bloodDropsPrefab;
    
    private GameObject[] bloodDrippers = new GameObject[2];

    // Update is called once per frame
    void Update()
    {
        if (amount - amountUsed > 0)
        {
            UpdateBlood();
        }
    }

    public void UpdateBlood()
    {
        float diff = (amount - amountUsed) * growthFactor;
        Vector3 leftGrowth = Grow(0, -bias * diff);
        Vector3 rightGrowth = Grow(1, (1.0f - bias) * diff);
        amountUsed += diff;


        lineRenderer.SetPosition(0, lineRenderer.GetPosition(0) + leftGrowth);
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(1) + rightGrowth);
        if (amount - amountUsed < .001)
        {
            amountUsed = amount;
        }
    }

    public Vector3 Grow(int index, float growthDistance)
    {
        Vector3 position = transform.rotation * lineRenderer.GetPosition(index) + transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(position, transform.rotation * Vector3.down, .1f, LayerMask.GetMask("Default"));
        Debug.DrawLine(position, position + transform.rotation * Vector3.down * .1f);
        bool groundExists = hit2D.collider != null;
        hit2D = Physics2D.Raycast(position, transform.rotation * Vector3.right * Mathf.Sign(growthDistance), 
                                            Mathf.Abs(growthDistance), LayerMask.GetMask("Default"));
        if (!groundExists)
        {
            // Debug.LogFormat("Here {0}", index);
            if (bloodDrippers[index] == null)
            {
                CreateBloodDrops(index, position, growthDistance);
            }
            SetDripSpeed(index, growthDistance);
            return Vector2.zero;
        }
        if (hit2D.collider != null)
        {
            // Debug.LogFormat("There {0}", index);
            return Vector3.zero;
        }
        // Debug.LogFormat("ADSF {0}", index);
        return transform.rotation * Vector3.right * growthDistance;
    }

    public void CreateBloodDrops(int index, Vector3 worldspaceLocation, float growthDistance)
    {
        GameObject go = Instantiate(bloodDropsPrefab,
                                    transform.rotation * lineRenderer.GetPosition(index) + lineRenderer.transform.position
                                    + transform.rotation * (index == 1 ? Vector3.right : Vector3.left) * .0625f,
                                    Quaternion.identity, transform);
        bloodDrippers[index] = go;
    }

    public void SetDripSpeed(int index, float growth)
    {
        if (bloodDrippers[index] == null)
        {
            Debug.LogWarning("Trying to set drip speed when no blood particle system exists.");
            return;
        }
        ParticleSystem dripper = bloodDrippers[index].GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule emission = dripper.emission;
        emission.rateOverTime = Mathf.Abs(growth) * 1000;

    }
}
