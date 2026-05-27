using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;

    private Vector2 direction;

    private EnemyNPC target;

    public void SetDirection(EnemyNPC target)
    {
        this.target = target;
        direction = (target.transform.position - transform.position).normalized;

        if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Update()
    {
        if(target != null)
            direction = (target.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyNPC"))
        {
            EnemyNPC targetNPC = collision.gameObject.transform.GetComponent<EnemyNPC>();

            if (targetNPC != null)
            {
                targetNPC.TakeDamage(damage);
                Destroy(gameObject); 
            }
        }
    }
}

