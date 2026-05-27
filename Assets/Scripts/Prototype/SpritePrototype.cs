using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePrototype : MonoBehaviour, IPrototype
{
    public GameObject spritePrefab;

    public GameObject Clone(Vector3 position, Transform parent)
    {
        GameObject clone = Instantiate(spritePrefab, parent);
        clone.transform.localPosition = position;
        return clone;
    }
}