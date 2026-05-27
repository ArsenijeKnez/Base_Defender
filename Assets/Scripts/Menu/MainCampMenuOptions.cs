using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCampMenuOptions : MonoBehaviour, IResourceObserver, INPCObserver
{
    public Button farmerButton;

    public Button builderButton;
    public Button archerButton;

    public Button archerWarriorButton;
    public Button warriorButton;


    public Text farmerCostText;
    public Text builderCostText;
    public Text warriorCostText;
    public Text archerCostText;
    public Text archerWarriorCostText;

    public Text farmerCountText;
    public Text builderCountText;
    public Text warriorCountText;
    public Text archerCountText;
    public Text archerWarriorCountText;
    public int builderCost = 50;
    public int archerCost = 30;
    public int warriorCost = 50;
    public int archerWarriorCost = 50;
    public int farmerCost = 50;

    public GameObject[] recruits;
    public GameObject[] camps;



    private int farmerCount = 2;
    private int builderCount = 0;
    private int archerCount = 0;
    private int warriorCount = 0;
    private int archerWarriorCount = 0;

    private int wood = 0;
    private int stone = 0;
    private int wheat = 0;

    private int campLevel = 0;
    private BuildingFactory buildingFactory;
    private Transform mainSpawnArea;

    private GameObject menuDisplay;


    void Start(){
        menuDisplay = gameObject.transform.GetChild(0).gameObject;
        mainSpawnArea = GameObject.FindGameObjectWithTag("MainNPCSpawnArea").transform;
        ResourceManager.AddObserver(this);
        NPCController.AddObserver(this);
        buildingFactory = gameObject.AddComponent<BuildingFactory>();
        buildingFactory.Initialize(recruits);
        farmerCostText.text = farmerCost.ToString();
        builderCostText.text = builderCost.ToString();
        archerCostText.text = archerCost.ToString();
        archerWarriorCostText.text = archerWarriorCost.ToString();
        warriorCostText.text = warriorCost.ToString();

        this.wood = ResourceManager.WoodCount;
        this.stone = ResourceManager.StoneCount;
        this.wheat = ResourceManager.WheatCount;

    }

    public void CloseCampMenu(){
        if(menuDisplay.activeSelf){
            menuDisplay.SetActive(false);
            return;
        }
    }

    public void SetCampLevel(int campLevel){
        if(menuDisplay.activeSelf){
            return;
        }
        menuDisplay.SetActive(true);
        this.campLevel = campLevel;
        for(int i = 0; i<= camps.Length - 1; i++){
            if(i == campLevel - 1)
                camps[i].SetActive(true);
            else
                camps[i].SetActive(false);
        }
        
        UpdateMenuText();
        UpdateButtonIntractability();
    }

    public void UpdateResourceData(int wood, int stone, int wheat){
        this.wood = wood;
        this.stone = stone;
        this.wheat = wheat;

        UpdateMenuText();
        UpdateButtonIntractability();
    }

    public void UpdateNPCData(Dictionary<string, int> npcs){

        foreach(KeyValuePair<string, int> kvp in npcs){
            switch(kvp.Key){
                case "Farmer":
                    farmerCount = kvp.Value;
                    break;
                case "Archer":
                    archerCount = kvp.Value;
                    break;
                case "Builder":
                    builderCount = kvp.Value;
                    break;
                case "Warrior":
                    warriorCount = kvp.Value;
                    break;
                case "ArcherWarrior":
                    archerWarriorCount = kvp.Value;
                    break;
            }
        }
        
        UpdateMenuText();
        UpdateButtonIntractability();
    }

    void UpdateMenuText(){
        int archerHouseCount = BuildingsController.GetBuildingCount("ArcherHouse");
        int builderHouseCount = BuildingsController.GetBuildingCount("BuilderHouse");
        int warriorHouseCount = BuildingsController.GetBuildingCount("WarriorHouse");

        farmerCountText.text = farmerCount.ToString() + "/" + (campLevel*2).ToString();
        builderCountText.text = builderCount.ToString() + "/" + (builderHouseCount+1).ToString();
        archerCountText.text = archerCount.ToString() + "/" + (archerHouseCount*3 - archerWarriorCount).ToString();
        warriorCountText.text = warriorCount.ToString() + "/" + (warriorHouseCount).ToString();
        archerWarriorCountText.text = archerWarriorCount.ToString() + "/" + (Mathf.Min((archerHouseCount*3-archerCount), warriorHouseCount)).ToString();
    }

    void UpdateButtonIntractability(){

        int archerHouseCount = BuildingsController.GetBuildingCount("ArcherHouse");
        int builderHouseCount = BuildingsController.GetBuildingCount("BuilderHouse");
        int warriorHouseCount = BuildingsController.GetBuildingCount("WarriorHouse");


        if(wheat >= farmerCost && farmerCount < campLevel*2){
            farmerButton.interactable = true;
        }
        else{
            farmerButton.interactable = false; 
        }
        if(wheat >= builderCost && builderCount < (builderHouseCount+1)){
            builderButton.interactable = true;
        }
        else{
            builderButton.interactable = false; 
        }
        if(wheat >= archerCost && archerCount < (archerHouseCount*3 - archerWarriorCount)){
            archerButton.interactable = true;
        }
        else{
            archerButton.interactable = false; 
        }
        if(wheat >= warriorCost && warriorCount < (warriorHouseCount - archerWarriorCount)){
            warriorButton.interactable = true;
        }
        else{
           warriorButton.interactable = false; 
        }
        if(wheat >= archerWarriorCost && archerWarriorCount < (Mathf.Min((archerHouseCount*3-archerCount), warriorHouseCount))){
            archerWarriorButton.interactable = true;
        }
        else{
            archerWarriorButton.interactable = false; 
        }
    }

    public void RecruiteFarmer(){
        int index = Random.Range(0, 2);
        GameObject newFarmer = buildingFactory.CreateBuilding(index, mainSpawnArea.position, Quaternion.identity);
        ResourceManager.WheatCount -= farmerCost;
        NPCController.RegisterNPC(newFarmer);
    }

    public void RecruiteBuilder(){
        GameObject newBuilder = buildingFactory.CreateBuilding(3, mainSpawnArea.position, Quaternion.identity);
        ResourceManager.WheatCount -= builderCost;
        NPCController.RegisterNPC(newBuilder);
    }

    public void RecruiteArcher(){
        GameObject newArcher = buildingFactory.CreateBuilding(4, mainSpawnArea.position, Quaternion.identity);
        ResourceManager.WheatCount -= archerCost;
        NPCController.RegisterNPC(newArcher);
    }

    public void RecruiteWarrior(){
        GameObject newWarrior = buildingFactory.CreateBuilding(5, mainSpawnArea.position, Quaternion.identity);
        ResourceManager.WheatCount -= warriorCost;
        NPCController.RegisterNPC(newWarrior);
    }

    public void RecruiteArcherWarrior(){
        GameObject newArcherWarrior = buildingFactory.CreateBuilding(6, mainSpawnArea.position, Quaternion.identity);
        ResourceManager.WheatCount -= archerWarriorCost;
        NPCController.RegisterNPC(newArcherWarrior);
    }

}
