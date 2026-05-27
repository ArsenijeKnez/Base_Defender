using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int difficulty = 0;

    public GameObject woodTextObject;
    public GameObject stoneTextObject;
    public GameObject wheatTextObject;

    public GameObject npcsTextObject;

    public GameObject[] starterRecruits;

    private BuildingFactory buildingFactory;

    private Transform mainSpawnArea;

    void Start()
    {
        mainSpawnArea = GameObject.FindGameObjectWithTag("MainNPCSpawnArea").transform;
        buildingFactory = gameObject.AddComponent<BuildingFactory>();
        buildingFactory.Initialize(starterRecruits);

        InitiateTextObservers();

        switch(difficulty){
            case 0:
                StartGameDifficulty0();
                break;
        }

    }

    void InitiateTextObservers(){
        var woodTextObserver = woodTextObject.AddComponent<ResourceTextObserver>();
        var stoneTextObserver = stoneTextObject.AddComponent<ResourceTextObserver>();
        var wheatTextObserver = wheatTextObject.AddComponent<ResourceTextObserver>();
        var npcsTextObserver = npcsTextObject.AddComponent<NPCTextObserver>();

        woodTextObserver.Initialize("Wood");
        stoneTextObserver.Initialize("Stone");
        wheatTextObserver.Initialize("Wheat");
        npcsTextObserver.Initialize();

        ResourceManager.AddObserver(woodTextObserver);
        ResourceManager.AddObserver(stoneTextObserver);
        ResourceManager.AddObserver(wheatTextObserver);
        NPCController.AddObserver(npcsTextObserver);
    }

    void StartGameDifficulty0(){

        GameObject newRecruit1 = buildingFactory.CreateBuilding(0, mainSpawnArea.position, Quaternion.identity);
        GameObject newRecruit2 = buildingFactory.CreateBuilding(0, mainSpawnArea.position, Quaternion.identity);

        NPCController.RegisterNPC(newRecruit1);
        NPCController.RegisterNPC(newRecruit2);

        ResourceManager.WoodCount += 25;
        ResourceManager.StoneCount += 20;
        ResourceManager.WheatCount += 40;
    }
}
