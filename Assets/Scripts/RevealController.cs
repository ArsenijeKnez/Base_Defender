using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealController : MonoBehaviour
{
    public Material revealMaterial;
    public float minX = 0f;
    public float maxX = 1f;

    public Color color = new Color(0.9f, 0.9f, 0.9f, 1f);

    private void Update()
    {
        revealMaterial.SetFloat("_MinX", minX);
        revealMaterial.SetFloat("_MaxX", maxX);
        revealMaterial.SetColor("_Color", color);
    }
}
