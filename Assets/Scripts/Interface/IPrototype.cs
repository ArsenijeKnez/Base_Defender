using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPrototype
{
    GameObject Clone(Vector3 position, Transform parent);
}