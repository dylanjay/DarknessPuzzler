using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealManager : MonoBehaviour {
    public void UpdateRevealShader(Renderer renderer)
    {
        StartCoroutine(UpdateSpikeReveal(renderer));
    }

    public void UpdateRevealShaderX4(Renderer renderer)
    {
        StartCoroutine(UpdateSpikeReveal(renderer, speed: 4.0f));
    }

    IEnumerator UpdateSpikeReveal(Renderer renderer, float speed = .5f)
    {
        Material material = renderer.material;
        float revealAmount = material.GetFloat("_RevealAmount");
        while (revealAmount < .999f)
        {
            revealAmount += Time.deltaTime * speed;
            material.SetFloat("_RevealAmount", revealAmount);
            yield return null;
        }
        material.SetFloat("_RevealAmount", 1.0f);
    }
}
