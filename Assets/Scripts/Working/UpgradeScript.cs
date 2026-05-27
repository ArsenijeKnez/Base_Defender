using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public GameObject current;
    public GameObject next;
    public float buildTime;
    public GameObject progressBarBG;
    public ProgressBar progressBarScript;
    public InteractionScript interaction;
    public float walkingArea = 1f; 
    public bool beingBuilt = false;
    public UpgradeScript[] GroupBuilding = null;
    public int wood = 0;
    public int stone = 0;
    public int wheat = 0;
    public string workerTag = "NPC";

    private IdleScript workerIdle;
    private WorkerBuildingScript workerWorking;
    private Color initialColor;
    private bool stillWorking = false;

    void Start()
    {
        initialColor = GetComponent<SpriteRenderer>().color;
        progressBarScript.duration = buildTime;
        if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
    }

    public void InitiateBuild(){
        GetComponent<SpriteRenderer>().color = Color.gray;
        beingBuilt = true;
    }

    public void UnitiateBuild(){
        GetComponent<SpriteRenderer>().color = initialColor;
        beingBuilt = false;
    }

    void Update(){
        if(workerWorking == null){
            stillWorking = false;
            progressBarBG.SetActive(false);
            if(GroupBuilding != null)
            {
                foreach(UpgradeScript g in GroupBuilding){
                    g.UnitiateBuild();
                }
            }
            UnitiateBuild();
        }
    }

	void OnMouseDown (){

        if(interaction.selectedWorker != null && beingBuilt == false && interaction.selectedWorker.tag == workerTag){
            if(GroupBuilding != null)
            {
                foreach(UpgradeScript g in GroupBuilding){
                    g.InitiateBuild();
                }
            }
            ResourceManager.WoodCount -= wood;
            ResourceManager.StoneCount -= stone;
            ResourceManager.WheatCount -= wheat;
            workerIdle = interaction.selectedWorker.GetComponent<IdleScript>();
            workerWorking = interaction.selectedWorker.GetComponent<WorkerBuildingScript>();
            workerIdle.isIdle = false;
            workerWorking.StartWork(transform, walkingArea);
            interaction.Deselect();
	        AudioManager.instance.PlaySoundEffect("Upgrade");
            progressBarBG.SetActive(true);
            InitiateBuild();
            stillWorking = true;
            StartCoroutine(StartBuild());
        }

	}

    IEnumerator StartBuild()
    {
        yield return new WaitForSeconds(buildTime);
        if(stillWorking){
            workerWorking.isWorking = false;
            workerIdle.isIdle = true;
            workerWorking.animator.SetTrigger("idle");
            next.SetActive(true);
            Destroy(current);
        }
    }

}
