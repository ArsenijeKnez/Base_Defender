using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public float health = 100f;
    public string targetType = "default"; 
    public bool IsNPC = false;
    private float currentHealth = 0f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy();
        }
    }

    public void Initialize(){
        currentHealth = health;
    }

    public void Restore(){
        health = currentHealth;
    }

    void Destroy()
    {
        if (IsNPC)
        {
            NPCController.UnregisterNPC(gameObject);
            Destroy(gameObject);
        }
        else
        {
            BuildingsController.RemoveBuilding(gameObject);
            if(targetType == "Wall")
            {
                Transform child = transform.Find("Wall_lvl1");

                if (child != null)
                {
                    child.gameObject.SetActive(false);
                    child = transform.Find("WallRuin");
                    if (child != null)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else{
                    child = transform.Find("Wall_lvl2");
                    if (child != null)
                    {
                        child.gameObject.SetActive(false);
                        child = transform.Find("WallRuin2");
                        if (child != null)
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
                }
                
            }
            else{
                Destroy(gameObject);
            }
        }
    }
}
