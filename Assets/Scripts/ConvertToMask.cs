using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToMask : MonoBehaviour
{
  
    [SerializeField] private int layer = 10;
    private int layerAsLayerMask;

    private void Start()
    {
        layerAsLayerMask = (1 << layer);
    }
}
