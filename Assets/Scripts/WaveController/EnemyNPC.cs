using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{
    public float speed = 2f;
    public float demage = 2f;
    private EnemyTarget target;
    private Rigidbody2D rb;

    public float health = 100f;

    public bool IsAlive = true;

    public float Height = 0f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Vector3 newPosition = transform.position;
        newPosition.y += Height;
        transform.position = newPosition;
    }

    void Update()
    {
        TryFindTarget();
        if (target != null)
        {
            MoveTowardsTarget();
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void MoveTowardsTarget()
    {
        if(Mathf.Abs(target.transform.position.x - gameObject.transform.position.x) < 0.5f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            AttackBuilding();
            return;
        }
        float direction = Mathf.Sign(target.transform.position.x - transform.position.x);
        if(direction == -1){
            transform.rotation =  Quaternion.Euler(0, 180, 0);
         }
        else
        {
            transform.rotation =  Quaternion.Euler(0, 0, 0);
        }
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void SetTarget(EnemyTarget newTarget)
    {
        target = newTarget;
    }

    private void TryFindTarget(){
        EnemyTarget buildingTarget = BuildingsController.GetNearestBuilding(gameObject.transform.localPosition);
        EnemyTarget npcTarget = NPCController.GetNearestNPC(gameObject.transform.localPosition);
        
        if (buildingTarget != null && npcTarget != null)
        {
            float distanceToBuilding = Vector2.Distance(transform.localPosition, buildingTarget.transform.position);
            float distanceToNPC = Vector2.Distance(transform.localPosition, npcTarget.transform.position);

            target = distanceToBuilding < distanceToNPC ? buildingTarget : npcTarget;
        }
        else if (buildingTarget != null)
        {
            target = buildingTarget;
        }
        else if (npcTarget != null)
        {
            target = npcTarget;
        }
        else
        {
            target = null;
        }
    }

    private void AttackBuilding()
    {
        if (target != null)
        {
            target.TakeDamage(demage); 
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy();
        }
    }

    void Destroy(){
        NPCController.UnregisterEnemyNPC(gameObject);
        IsAlive = false;
        Destroy(gameObject);
    }
}