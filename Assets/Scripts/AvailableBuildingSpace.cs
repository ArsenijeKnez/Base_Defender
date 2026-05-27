using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableBuildingSpace : MonoBehaviour
{
    public string[] tagsToCheck;
    public float[] minXPositions;
    public float[] maxXPositions;
    public Material revealMaterial;
    private const int MaxArraySize = 1000;

    void Start()
    {
        minXPositions = new float[MaxArraySize];
        maxXPositions = new float[MaxArraySize];
    }

    void Update()
    {
        List<float> bounderies = NearestBoundaries.FindNearestBounds();
        minXPositions[0] = bounderies[2]-5000f;
        maxXPositions[0] = bounderies[0];

        minXPositions[1] = bounderies[1];
        maxXPositions[1] = bounderies[2]+5000f;

        int count = 2;
        foreach (string tag in tagsToCheck)
        {
            if (count >= MaxArraySize)
                break;

            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsWithTag)
            {
                if (count >= MaxArraySize)
                    break;

                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    Bounds bounds = collider.bounds;
                    minXPositions[count] = bounds.min.x;
                    maxXPositions[count] = bounds.max.x;
                    count++;
                }
            }
        }
        

        revealMaterial.SetInt("_ArrayLength", count);
        revealMaterial.SetFloatArray("_MinXArray", minXPositions);
        revealMaterial.SetFloatArray("_MaxXArray", maxXPositions);
    }
}
