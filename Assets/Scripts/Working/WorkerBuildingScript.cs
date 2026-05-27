using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBuildingScript : MonoBehaviour
{
    public bool isWorking = false; 
    public bool isSelected = false; 
    public InteractionScript interaction;

    public Transform targetObject;
    public float walkingArea = 2f;
    private float moveSpeed = 1f;
    private float minIdlePauseTime = 1f;
    private float maxIdlePauseTime = 2f;
    private float targetXPosition;
    private Rigidbody2D rb;
    private bool isPaused = false;
    private float idleTimer = 0f;
    private float currentIdlePauseTime = 0f;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentIdlePauseTime = Random.Range(minIdlePauseTime, maxIdlePauseTime);
        if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
        if(targetObject == null)
            targetObject = GameObject.FindGameObjectWithTag("MainCamp").transform;
    }

    void OnMouseDown (){
        if(!isWorking){
            if(isSelected){
                interaction.Deselect();
                Deselect();
            }
            else{
                if(interaction.selectedWorker != null){
                    /* interaction.selectedWorker.GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
                    foreach (Transform child in interaction.selectedWorker.transform)
                    {
                        child.GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
                    }
                    interaction.selectedWorker.GetComponent<WorkerBuildingScript>().isSelected = false; */
                    interaction.Deselect();
                }
                interaction.Select(gameObject);
                GetComponent<SpriteRenderer>().color = Color.white;
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpriteRenderer>().color = Color.white;
                }
                isSelected = true;
                }
            }
	}

    void Update()
    {
        if(isWorking)
        {
            targetXPosition = targetObject.position.x;
         if (!isPaused)
            {
                if (Mathf.Abs(transform.position.x - targetXPosition) < 0.1f)
                {
                    isPaused = true;
                    idleTimer = 0f;
                    animator.SetTrigger("build");
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
                    animator.SetTrigger("idle");
                    isPaused = false;
                    targetXPosition = Random.Range(targetObject.position.x - walkingArea, targetObject.position.x + walkingArea);
                    currentIdlePauseTime = Random.Range(minIdlePauseTime, maxIdlePauseTime);
                }
            }
        }
        else
            animator.SetTrigger("idle");
    }

    public void StartWork(Transform transform, float walkingArea){
        isWorking = true;
        targetObject = transform;
        this.walkingArea = walkingArea;
        Deselect();
    }

    public void Deselect(){
        GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
        foreach (Transform child in transform)
        {
            if(child.tag != "Indicator")
                child.GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
        }
        isSelected = false;
    }
}

