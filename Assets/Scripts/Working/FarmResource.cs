using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmResource : MonoBehaviour
{
   
    public float farmTime;
    public GameObject progressBarBG;
    public ProgressBar progressBarScript;
    public InteractionScript interaction;
    public float walkingArea = 1f; 
    public bool beingMined = false;
    public int wood = 0;
    public int stone = 0;
    public int wheat = 0;
    public string workerTag = "NPC";

    private IdleScript workerIdle;
    private WorkerBuildingScript workerWorking;

    void Start()
    {
        progressBarScript.duration = farmTime;
        if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
    }

    public void InitiateBuild(){
        GetComponent<SpriteRenderer>().color = Color.gray;
        beingMined = true;
    }

	void OnMouseDown (){

        if(interaction.selectedWorker != null && beingMined == false && interaction.selectedWorker.tag == workerTag){
            workerIdle = interaction.selectedWorker.GetComponent<IdleScript>();
            workerWorking = interaction.selectedWorker.GetComponent<WorkerBuildingScript>();
            workerIdle.isIdle = false;
            workerWorking.StartWork(transform, walkingArea);
            foreach (Transform child in interaction.selectedWorker.transform)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(0.6235294f,0.6235294f,0.6235294f);
            }
            interaction.Deselect();
	        AudioManager.instance.PlaySoundEffect("Upgrade");
            progressBarBG.SetActive(true);
            InitiateBuild();
            StartCoroutine(StartBuild());
        }

	}

    IEnumerator StartBuild()
    {
        yield return new WaitForSeconds(farmTime);
        workerWorking.isWorking = false;
        workerIdle.isIdle = true;
        workerWorking.animator.SetTrigger("idle");
        ResourceManager.WoodCount += wood;
        ResourceManager.StoneCount += stone;
        ResourceManager.WheatCount += wheat;
        Destroy(gameObject);
    }

}
