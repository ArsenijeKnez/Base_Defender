using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    public GameObject[] spritePrefabs; 
    public int spriteCount = 10; 
    public Vector2 areaSize = new Vector2(10f, 10f); 
    public float minZOrder = -10f; 
    public float maxZOrder = 10f; 

    private List<IPrototype> prototypes;

    void Start()
    {
        InitializePrototypes();
        GenerateSprites(transform); 
    }

    void InitializePrototypes()
    {
        prototypes = new List<IPrototype>();

        foreach (GameObject prefab in spritePrefabs)
        {
            GameObject prototypeObject = new GameObject(prefab.name + "Prototype");
            SpritePrototype prototype = prototypeObject.AddComponent<SpritePrototype>();
            prototype.spritePrefab = prefab;
            prototypes.Add(prototype);
        }
    }

    void GenerateSprites(Transform parent)
    {

for (int i = 0; i < spriteCount; i++) 
{
    Vector2 position = Vector2.zero;
    bool positionFound = false;

    while (!positionFound)
    {
        position = new Vector2(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), Random.Range(-areaSize.y / 2f, areaSize.y / 2f));

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); 
        if (colliders.Length == 0)
        {
            positionFound = true;
        }
    }

    float yMin = -areaSize.y / 2f;
    float yMax = areaSize.y / 2f;
    float zMin = minZOrder; 
    float zMax = maxZOrder;

    float zOrder = Mathf.Lerp(zMin, zMax, Mathf.InverseLerp(yMin, yMax, position.y));

    IPrototype prototype = prototypes[Random.Range(0, prototypes.Count)];
    GameObject newSpriteObject = prototype.Clone(new Vector3(position.x, position.y, zOrder), parent);

    SpriteRenderer spriteRenderer = newSpriteObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sortingOrder = 2; 
    }



        /* for (int i = 0; i < spriteCount; i++)
        {
            Vector2 position = Vector2.zero;
            bool positionFound = false;

            while (!positionFound)
            {
                position = new Vector2(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), Random.Range(-areaSize.y / 2f, areaSize.y / 2f));


                Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); 
                if (colliders.Length == 0)
                {
                    positionFound = true;
                }
            }
          
            float zOrder = Random.Range(minZOrder, maxZOrder);
            IPrototype prototype = prototypes[Random.Range(0, prototypes.Count)];
            GameObject newSpriteObject = prototype.Clone(new Vector3(position.x, position.y, zOrder), parent);
            SpriteRenderer spriteRenderer = newSpriteObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 2; 
        } */
    }
}