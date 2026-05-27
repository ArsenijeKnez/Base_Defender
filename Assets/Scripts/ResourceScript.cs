using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
  
    public GameObject current;
    public AudioSource audi;
    public float buildTime;
    public GameObject progressBarBG;
    public ProgressBar progressBarScript;
    public InteractionScript interaction;
    public float walkingArea = 1f; 
    public bool beingCleard = false;

    private IdleScript workerIdle;
    private WorkerBuildingScript workerWorking;

    void Start()
    {
        progressBarScript.duration = buildTime;
        if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
    }

    public void InitiateBuild(){
            GetComponent<SpriteRenderer>().color = Color.gray;
            beingCleard = true;
    }

	void OnMouseDown (){

        if(interaction.selectedWorker != null && beingCleard == false){
            workerIdle = interaction.selectedWorker.GetComponent<IdleScript>();
            workerWorking = interaction.selectedWorker.GetComponent<WorkerBuildingScript>();
            workerIdle.isIdle = false;
            workerWorking.isWorking = true;
            workerWorking.targetObject = transform;
            workerWorking.walkingArea = walkingArea;
            interaction.selectedWorker.GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
            foreach (Transform child in interaction.selectedWorker.transform)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
            }
            interaction.Deselect();
	        audi.Play();
            progressBarBG.SetActive(true);
            InitiateBuild();
            StartCoroutine(StartWork());
        }

	}

    IEnumerator StartWork()
    {
        yield return new WaitForSeconds(buildTime);
        workerWorking.isWorking = false;
        workerIdle.isIdle = true;
        workerWorking.animator.SetTrigger("idle");
        current.SetActive(false);
    }

}
