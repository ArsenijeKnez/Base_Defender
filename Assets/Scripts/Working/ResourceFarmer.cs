using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFarmer : MonoBehaviour
{
    public int wheat = 0;
    public int stone = 0;
    public int wood = 0;
    public float walkingArea = 0f;
    private GameObject Assigned = null;
    private InteractionScript interaction;

    void Start(){
        interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
    }

    void OnMouseDown()
    {
        if(!Assigned && interaction.selectedWorker != null && interaction.selectedWorker.tag == "Farmer"){
            IdleScript workerIdle = interaction.selectedWorker.GetComponent<IdleScript>();
            WorkerBuildingScript workerWorking = interaction.selectedWorker.GetComponent<WorkerBuildingScript>();
            workerIdle.isIdle = false;
            workerWorking.StartWork(transform, walkingArea);
            Assigned = interaction.selectedWorker;
            interaction.Deselect();
	        AudioManager.instance.PlaySoundEffect("Upgrade");
            StartCoroutine(UpdateCoroutine());
        }
    }

    IEnumerator UpdateCoroutine()
    {
        while (Assigned != null)
        {
            yield return new WaitForSeconds(20f);
            if(wheat != 0)
                ResourceManager.WheatCount += wheat;
            if(stone != 0)
                ResourceManager.StoneCount += stone;
            if(wood != 0)
                ResourceManager.WoodCount += wood;
        }
    }
}
