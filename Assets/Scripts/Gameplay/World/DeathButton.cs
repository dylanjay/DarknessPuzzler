using System.Collections;
using UnityEngine;

public class DeathButton : MonoBehaviour 
{
    bool buttonRevealed = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Flip"))
        {
            collision.GetComponentInParent<PlayerKiller>().Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // All objects that can currently hit spikes can bleed.
        if (!buttonRevealed)
        {
            buttonRevealed = true;
            StartCoroutine(UpdateButtonReveal());
        }
    }

    IEnumerator UpdateButtonReveal()
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
