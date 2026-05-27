using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public ObjectPool cloudPool; 
    public int spriteCount = 10;
    public Vector2 areaSize = new Vector2(10f, 10f);
    public float minZOrder = -10f;
    public float maxZOrder = 10f;
    public float movementSpeed = 1f;

    private List<GameObject> activeClouds = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < spriteCount; i++)
        {
            SpawnNewCloudOnLeft();
        }
    }

    void Update()
    {
        MoveAndDespawnClouds();
    }

    void MoveAndDespawnClouds()
    {
        for (int i = 0; i < activeClouds.Count; i++)
        {
            GameObject cloud = activeClouds[i];
            cloud.transform.localPosition += new Vector3(movementSpeed * Time.deltaTime, 0, 0);

            if (cloud.transform.localPosition.x > areaSize.x / 2f)
            {
                cloudPool.ReturnObject(cloud);
                activeClouds.RemoveAt(i);
                i--;

                SpawnNewCloudOnLeft();
            }
        }
    }

    void SpawnNewCloudOnLeft()
    {
        GameObject newCloud = cloudPool.GetObject();
        Vector2 position = new Vector2(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), Random.Range(-areaSize.y / 2f, areaSize.y / 2f));
        float zOrder = Random.Range(minZOrder, maxZOrder);
        newCloud.transform.localPosition = new Vector3(transform.localPosition.x + position.x, transform.localPosition.y + position.y, transform.localPosition.z + zOrder);
        activeClouds.Add(newCloud);
    }

    public void DarkenSprites(float amount)
    {
        foreach (GameObject cloud in activeClouds)
        {
            SpriteRenderer spriteRenderer = cloud.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spriteRenderer.color.r - amount, spriteRenderer.color.g - amount, spriteRenderer.color.b - amount, spriteRenderer.color.a + amount);
        }
    }

    public void LightenSprites(float amount)
    {
        foreach (GameObject cloud in activeClouds)
        {
            SpriteRenderer spriteRenderer = cloud.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spriteRenderer.color.r + amount, spriteRenderer.color.g + amount, spriteRenderer.color.b + amount, spriteRenderer.color.a - amount);
        }
    }
}
