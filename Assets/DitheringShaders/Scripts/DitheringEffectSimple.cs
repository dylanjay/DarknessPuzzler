﻿using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Other/Dithering Simple")]
public class DitheringEffectSimple : MonoBehaviour
{
	public int ColorCount = 4;
	public int PaletteHeight = 64;
	public Texture PaletteTexture;
    public Material material;

	void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //GL.Clear(true, true, Color.white);
        Debug.Log("Ayy");
        material.SetFloat("_ColorCount", ColorCount);
		material.SetFloat("_PaletteHeight", PaletteHeight);
		material.SetTexture("_PaletteTex", PaletteTexture);
		Graphics.Blit(source, destination, material);
	}
}