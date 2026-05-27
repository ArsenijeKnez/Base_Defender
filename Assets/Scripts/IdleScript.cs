using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleScript : MonoBehaviour
{
    public Transform targetObject;
    public float walkingArea = 2f;
    public float moveSpeed = 1f;
    public float minIdlePauseTime = 1f;
    public float maxIdlePauseTime = 3f;
    public bool isIdle = true;

    private float targetXPosition;
    private Rigidbody2D rb;
    private bool isPaused = false;
    private float idleTimer = 0f;
    private float currentIdlePauseTime = 0f;

    private bool getInside = false;

    void Start()
    {
        if(targetObject == null){
            targetObject = GameObject.FindGameObjectWithTag("MainCamp").transform;
        }
        rb = GetComponent<Rigidbody2D>();
        targetXPosition = targetObject.position.x;
        currentIdlePauseTime = Random.Range(minIdlePauseTime, maxIdlePauseTime);
    }

    void Update()
    {
        if (isIdle)
        {
            if (!isPaused)
            {
                if (Mathf.Abs(transform.position.x - targetXPosition) < 0.1f)
                {
                    isPaused = true;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    idleTimer = 0f;
                }
                else
                {
                    float direction = Mathf.Sign(targetXPosition - transform.position.x);
                    if(direction == -1){
                        transform.rotation =  Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        transform.rotation =  Quaternion.Euler(0, 0, 0);
                    }
                    rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
                }
            }
            else
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= currentIdlePauseTime)
                {
                    isPaused = false;
                    targetXPosition = Random.Range(targetObject.position.x - walkingArea, targetObject.position.x + walkingArea);
                    currentIdlePauseTime = Random.Range(minIdlePauseTime, maxIdlePauseTime);
                }
            }
        }
        else if(getInside){
            if (Mathf.Abs(transform.position.x - targetXPosition) < 0.1f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                idleTimer = 0f;
                gameObject.SetActive(false);
            }
            else{
                float direction = Mathf.Sign(targetXPosition - transform.position.x);
                if(direction == -1){
                    transform.rotation =  Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation =  Quaternion.Euler(0, 0, 0);
                }
               
                rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            }
        }
    }   

    public void GetInside(){
        if(isIdle){
            isIdle = false;
            targetXPosition = targetObject.position.x;
            getInside = true;
        }
    }

    public void GoOutside(){
        if(!gameObject.activeSelf){
            gameObject.SetActive(true);
            isIdle = true;
            getInside = false;
            targetXPosition = targetObject.position.x;
            currentIdlePauseTime = Random.Range(minIdlePauseTime, maxIdlePauseTime);
        }
    }
}

