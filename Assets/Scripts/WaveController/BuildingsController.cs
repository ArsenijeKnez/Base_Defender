using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingsController
{
    private static List<GameObject> buildings = new List<GameObject>();
    private static Dictionary<string, int> buildingTypes = new Dictionary<string, int>();

    public static int GetBuildingCount(string type){
        if(type == "All")
            return buildings.Count;
        if(buildingTypes.ContainsKey(type))
            return buildingTypes[type];
        else
            return 0;
    }

    public static EnemyTarget GetNearestBuilding(Vector2 position)
    {
        GameObject nearestBuilding = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject building in buildings)
        {
            float distance = Vector2.Distance(position, building.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestBuilding = building;
            }
        }
        if(nearestBuilding != null){
            EnemyTarget target = nearestBuilding.GetComponent<EnemyTarget>();
            if(target != null)
                return target;
        }
        return null;
    }

    public static void AddBuilding(GameObject building){
        buildings.Add(building);
        EnemyTarget target = building.GetComponent<EnemyTarget>(); 
        target.Initialize();
            if(buildingTypes.ContainsKey(target.targetType))
                buildingTypes[target.targetType] += 1; 
            else{
                buildingTypes[target.targetType] = 1;
            }
    }

    public static void RemoveBuilding(GameObject building){
        EnemyTarget target = building.GetComponent<EnemyTarget>();

        if(buildingTypes.ContainsKey(target.targetType)){
            buildingTypes[target.targetType] -= 1;
        }
        else{
            buildingTypes[target.targetType] = 0;
        }
        if(buildings.Contains(building))
            buildings.Remove(building);

        if(target.targetType == "MainCamp"){
            Debug.Log("GAME OVER");
        }

        buildings.Remove(building);
    }
}
