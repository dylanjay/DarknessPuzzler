using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Blitter : MonoBehaviour {
    
    public Material mat;
    public Texture2D source;
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            RenderTexture rTex = new RenderTexture(256, 256, 24);
            Texture2D dest = new Texture2D(source.width, source.height, TextureFormat.ARGB32, false);

            Graphics.Blit(source, rTex, mat);
            RenderTexture.active = rTex;
            dest.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            dest.Apply();
            File.WriteAllBytes(Application.dataPath + "/Blitted.png", dest.EncodeToPNG());
        }
	}
}
