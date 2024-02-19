using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorblind : MonoBehaviour
{
    private Color[,] RGB;
    private ColorBlindMode mode;
    private ColorBlindMode previousMode;
    private Material material;
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/ChannelMixer"));
        material.SetColor("_R", RGB[0, 0]);
        material.SetColor("_G", RGB[0, 1]);
        material.SetColor("_B", RGB[0, 2]);

        mode = ColorBlindMode.Normal;

    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // No effect
        if (mode == ColorBlindMode.Normal)
        {
            Graphics.Blit(source, destination);
            return;
        }
        // Change effect
        if (mode != previousMode)
        {
            material.SetColor("_R", RGB[(int)mode, 0]);
            material.SetColor("_G", RGB[(int)mode, 1]);
            material.SetColor("_B", RGB[(int)mode, 2]);
            previousMode = mode;
        }
        // Apply effect
        Graphics.Blit(source, destination, material);
    }

    public enum ColorBlindMode
    {
        Normal = 0,
        Protanopia = 1,
        Protanomaly = 2,
        Deuteranopia = 3,
        Deuteranomaly = 4,
        Tritanopia = 5,
        Tritanomaly = 6,
        Achromatopsia = 7,
        Achromatomaly = 8,
    }
}
