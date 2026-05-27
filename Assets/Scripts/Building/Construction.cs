using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    public float buildTime;
    public GameObject progressBarBG;
    public ProgressBar progressBarScript;
    public InteractionScript interaction;
    public float walkingArea = 1f; 
    private IdleScript workerIdle;
    private WorkerBuildingScript workerWorking;
    private Color initialColor;

    void Start()
    {
        initialColor = GetComponent<SpriteRenderer>().color;
        progressBarScript.duration = buildTime;
        if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
        InitiateBuild();
    }

    public void InitiateBuild(){
        GetComponent<SpriteRenderer>().color = Color.gray;
        workerIdle = interaction.selectedWorker.GetComponent<IdleScript>();
        workerWorking = interaction.selectedWorker.GetComponent<WorkerBuildingScript>();
        workerIdle.isIdle = false;
        workerWorking.StartWork(transform, walkingArea);
        interaction.Deselect();
        progressBarBG.SetActive(true);
        StartCoroutine(StartBuild());
    }

    public void UnitiateConstruction(){
        workerWorking.isWorking = false;
        workerIdle.isIdle = true;
        workerWorking.animator.SetTrigger("idle");
        GetComponent<SpriteRenderer>().color = initialColor;
        Destroy(progressBarBG);
        Destroy(this);
    }

    void Update(){
        if(workerWorking == null){
            Destroy(gameObject);
        }
    }

    IEnumerator StartBuild()
    {
        yield return new WaitForSeconds(buildTime);
        UnitiateConstruction();
    }

}
