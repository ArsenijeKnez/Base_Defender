using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour{
    private GameObject[] buildingPrefabs;

    public void Initialize(GameObject[] buildingPrefabs) {
        this.buildingPrefabs = buildingPrefabs;
    }

    public GameObject CreateBuilding(int index, Vector3 position, Quaternion rotation) {
        if (index < 0 || index >= buildingPrefabs.Length) {
            Debug.LogError("Invalid building index");
            return null;
        }
        return GameObject.Instantiate(buildingPrefabs[index], position, rotation);
    }
}
