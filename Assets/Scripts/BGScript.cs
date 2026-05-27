using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    public void DarkenSprites(float amount)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteRenderer.color = new Color(spriteRenderer.color.r - amount, spriteRenderer.color.g - amount, spriteRenderer.color.b - amount, spriteRenderer.color.a);
    }
    public void LightenSprites(float amount)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteRenderer.color = new Color(spriteRenderer.color.r + amount, spriteRenderer.color.g + amount, spriteRenderer.color.b + amount, spriteRenderer.color.a);
    }
}
