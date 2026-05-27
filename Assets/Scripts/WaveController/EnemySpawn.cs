using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform spawnL;
    public Transform spawnR;
    public static EnemySpawn instance = null;

    private BuildingFactory enemyFactory;
    public GameObject[] enemyPrefabs;
    private int round = 1;

    void Awake()
    {
        if (instance == null)
        {
            enemyFactory = gameObject.AddComponent<BuildingFactory>();
            enemyFactory.Initialize(enemyPrefabs);
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public void Spawn(float interval)
    {
        
        List<float> bounds = NearestBoundaries.FindNearestBounds();
        spawnL.position = new Vector3(bounds[0] - 30f, spawnL.position.y, spawnL.position.z);
        spawnR.position = new Vector3(bounds[1] + 30f, spawnR.position.y, spawnR.position.z);

        spawnL.GetChild(0).gameObject.SetActive(true);
        spawnR.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(SpawnEnemies(interval));
    }

    private IEnumerator SpawnEnemies(float interval)
    {
        int npcsToSpawn = Mathf.CeilToInt(round * 1.5f);
        float waitTime = (interval/npcsToSpawn)/1.2f;
        
        for (int i = 0; i < npcsToSpawn; i++)
        {
            yield return new WaitForSeconds(waitTime); 
            int index = Random.Range(0, enemyPrefabs.Length); 
            GameObject newEnemyL = enemyFactory.CreateBuilding(index, spawnL.position, Quaternion.identity);
            GameObject newEnemyR = enemyFactory.CreateBuilding(index, spawnR.position, Quaternion.identity);

            NPCController.RegisterEnemyNPC(newEnemyL);
            NPCController.RegisterEnemyNPC(newEnemyR);
        }

        spawnL.GetChild(0).gameObject.SetActive(false);
        spawnR.GetChild(0).gameObject.SetActive(false);
        round++;
    }

}
