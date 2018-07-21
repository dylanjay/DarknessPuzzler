using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Other/Dithering")]
public class DitheringEffect : PostEffectsBase
{
    public int ColorCount = 4;
    public int PaletteHeight = 64;
    public Texture PaletteTexture;
    public int DitherSize = 8;
    public Texture DitherTexture;
    public Material material;
    int i = 0;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //if (i++ % 2 == 0) { Graphics.Blit(source, destination); }
        material.SetFloat("_ColorCount", ColorCount);
        material.SetFloat("_PaletteHeight", PaletteHeight);
        material.SetTexture("_PaletteTex", PaletteTexture);
        material.SetFloat("_DitherSize", DitherSize);
        material.SetTexture("_DitherTex", DitherTexture);
        Graphics.Blit(source, destination, material);
    }
}