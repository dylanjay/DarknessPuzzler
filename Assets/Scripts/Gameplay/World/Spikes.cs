﻿using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool spikesRevealed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponentInParent<PlayerKiller>().Kill();
        }

        // All objects that can currently hit spikes can bleed.
        if (!spikesRevealed)
        {
            spikesRevealed = true;
            StartCoroutine(UpdateSpikeReveal());
        }

        if (collision.gameObject.tag == "DeadBody")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    IEnumerator UpdateSpikeReveal()
    {
        Material material = GetComponent<Renderer>().material;
        float revealAmount = 0;
        while (revealAmount < .999f)
        {
            revealAmount += Time.deltaTime / 2.0f;
            material.SetFloat("_RevealAmount", revealAmount);
            yield return null;
        }
        material.SetFloat("_RevealAmount", 1.0f);
    }
}
