using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArcher : MonoBehaviour
{
    public Transform arrowSpawnPoint;
    public GameObject arrowPrefab;
    public float fireRate = 10f;
    public float detectionRange = 10f;

    private float fireCooldown = 0f;
    private EnemyNPC target;

    void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (target == null || Vector2.Distance(transform.position, target.transform.position) > detectionRange)
        {
            EnemyNPC newTarget = NPCController.GetNearestEnemyNPC(transform.position);

            if(newTarget != null)
                target = Vector2.Distance(transform.position, newTarget.transform.position) < detectionRange? newTarget : null;
            else
                newTarget = null;
        }

        if (target != null && fireCooldown <= 0 && target.IsAlive)
        {
            ShootArrow();
            fireCooldown = 1f / fireRate;
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetDirection(target);
    }
}
