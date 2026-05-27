using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestBoundaries : MonoBehaviour
{
    private static string resourceTag = "Resource";
    private static string mainCampTag = "MainCamp";

    public static List<float> FindNearestBounds()
    {
        GameObject mainCamp = GameObject.FindGameObjectWithTag(mainCampTag);

        if (mainCamp == null)
        {
            Debug.LogError("No object found with the tag: " + mainCampTag);
            return null;
        }

        float mainCampX = mainCamp.GetComponent<Collider2D>().bounds.center.x;
        float nearestLeftMaxX = float.NegativeInfinity;
        float nearestRightMinX = float.PositiveInfinity;

        GameObject[] resources = GameObject.FindGameObjectsWithTag(resourceTag);

        foreach (GameObject resource in resources)
        {
            Collider2D resourceCollider = resource.GetComponent<Collider2D>();
            if (resourceCollider != null)
            {
                Bounds bounds = resourceCollider.bounds;

                if (bounds.max.x < mainCampX && bounds.max.x > nearestLeftMaxX)
                {
                    nearestLeftMaxX = bounds.max.x;
                }

                if (bounds.min.x > mainCampX && bounds.min.x < nearestRightMinX)
                {
                    nearestRightMinX = bounds.min.x;
                }
            }
        }

        return new List<float>() { nearestLeftMaxX, nearestRightMinX, mainCampX };
    }
}
