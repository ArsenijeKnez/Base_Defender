using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour {
    public GameObject[] buildingPrefabs;
    public GameObject[] transparentBuildingSprites;
    public LayerMask buildLayerMask;
    public Camera positionCamera;
    public BuildingItemScript[] buildingItems;

    private int itemIndex = -1;
    private BuildingItemScript currentItem = null;
    private BuildingFactory buildingFactory;


    void Start() {
        buildingFactory = gameObject.AddComponent<BuildingFactory>();
        buildingFactory.Initialize(buildingPrefabs);
    }

    void Update() {
        HandleBuildingSelection();
        HandleBuildingPlacement();
    }

    void HandleBuildingSelection()
    {
        for (int i = 0; i <= 5; i++) {
            if (i != itemIndex){
                buildingItems[i].itemSelected = false;
                transparentBuildingSprites[i].SetActive(false);
            }
        }

        if (itemIndex != -1 && !buildingItems[itemIndex].itemEnabled) {
            itemIndex = -1;
            currentItem = null;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectBuildingItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SelectBuildingItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SelectBuildingItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SelectBuildingItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) SelectBuildingItem(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) SelectBuildingItem(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7)) SelectBuildingItem(6);
    }

    void SelectBuildingItem(int index)
    {
        if (buildingItems[index].itemEnabled) {
            itemIndex = index;
            buildingItems[index].itemSelected = true;
            transparentBuildingSprites[index].SetActive(true);
        }
    }

    void HandleBuildingPlacement()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (itemIndex != -1) {
                Vector2 mousePosition = positionCamera.ScreenToWorldPoint(Input.mousePosition);
                
                if (IsPositionValid(mousePosition, buildingPrefabs[itemIndex])) {
                    currentItem = buildingItems[itemIndex];
                    DeductResources(currentItem);
                    GameObject newBuilding = buildingFactory.CreateBuilding(itemIndex, new Vector3(mousePosition.x, -7.9f, 0), Quaternion.identity);
                    if (newBuilding != null) {
                        if (itemIndex == 5){
                            DefenseWallManager.instance.AddWall(newBuilding.transform.position.x);
                        }
                        BuildingsController.AddBuilding(newBuilding);
                    }
                }
            }
        }
    }

    void DeductResources(BuildingItemScript item)
    {
        ResourceManager.WheatCount -= item.requiredWheat;
        ResourceManager.StoneCount -= item.requiredStone;
        ResourceManager.WoodCount -= item.requiredWood;
    }

    bool IsPositionValid(Vector2 position, GameObject building) {
        BoxCollider2D collider = building.GetComponent<BoxCollider2D>();
        Vector3 scale = building.transform.lossyScale;
        float worldSizeX = collider.size.x * scale.x;

        List<float> bounderies = NearestBoundaries.FindNearestBounds();
        if((position.x - worldSizeX / 2.0) < bounderies[0] || (position.x + worldSizeX / 2.0) > bounderies[1]){
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(position.x,-9), worldSizeX / 2, buildLayerMask);
        return colliders.Length == 0;
    }

    public void Unselect(){
        if (itemIndex != -1) {
            buildingItems[itemIndex].itemSelected = false;
            transparentBuildingSprites[itemIndex].SetActive(false);
            itemIndex = -1;
            currentItem = null;
        }
    }
}