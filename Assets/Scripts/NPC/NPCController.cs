using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NPCController
{
    private static List<GameObject> npcs = new List<GameObject>();
    private static List<GameObject> enemyNpcs = new List<GameObject>();

    private static Dictionary<string, int> targetTypes = new Dictionary<string, int>();

    private static List<INPCObserver> observers = new List<INPCObserver>();

    public static void RegisterNPC(GameObject npc)
    {
        if (!npcs.Contains(npc))
        {
            npcs.Add(npc);
            EnemyTarget target = npc.GetComponent<EnemyTarget>(); 
            if(targetTypes.ContainsKey(target.targetType))
                targetTypes[target.targetType] += 1; 
            else{
                targetTypes[target.targetType] = 1;
            }
            NotifyObservers();
        }
    }

    public static void UnregisterNPC(GameObject npc)
    {
        if (npcs.Contains(npc))
        {
            npcs.Remove(npc);
            EnemyTarget target = npc.GetComponent<EnemyTarget>();
            if(targetTypes.ContainsKey(target.targetType))
            {
                targetTypes[target.targetType] -= 1; 
                if(targetTypes[target.targetType] <0){
                    targetTypes[target.targetType] = 0;
                }
            }
            NotifyObservers();
        }
    }

    public static EnemyTarget GetNearestNPC(Vector2 position)
    {
        GameObject nearestNPC = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject npc in npcs)
        {
            float distance = Vector2.Distance(position, npc.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestNPC = npc;
            }
        }

        if(nearestNPC != null){
            EnemyTarget target = nearestNPC.GetComponent<EnemyTarget>();
            if(target != null)
                return target;
        }
        return null;
    }

    public static List<GameObject> GetAllNPCs()
    {
        return npcs;
    }

    public static void RegisterEnemyNPC(GameObject npc)
    {
        if (!enemyNpcs.Contains(npc))
        {
            enemyNpcs.Add(npc);
        }
    }

    public static void UnregisterEnemyNPC(GameObject npc)
    {
        if (enemyNpcs.Contains(npc))
        {
            enemyNpcs.Remove(npc);
        }
    }

    public static EnemyNPC GetNearestEnemyNPC(Vector2 position)
    {
        GameObject nearestNPC = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject npc in enemyNpcs)
        {
            float distance = Vector2.Distance(position, npc.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestNPC = npc;
            }
        }

        if(nearestNPC != null){
            EnemyNPC target = nearestNPC.GetComponent<EnemyNPC>();
            if(target != null)
                return target;
        }
        return null;
    }

    public static List<GameObject> GetAllEnemyNPCs()
    {
        return enemyNpcs;
    }

    public static bool IsNPCAlive(GameObject npc)
    {
        return npcs.Contains(npc);
    }

    public static void AddObserver(INPCObserver observer) {
        observers.Add(observer);
    }

    public static void RemoveObserver(INPCObserver observer) {
        observers.Remove(observer);
    }

    private static void NotifyObservers() {
        foreach (var observer in observers) {
            observer.UpdateNPCData(targetTypes);
        }
    }
}
