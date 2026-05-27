using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionScript : MonoBehaviour, INPCObserver {

    public GameObject selectedWorker = null;
    public GameObject buildMenu;
    public PauseMenuOptions pauseMenu;
    public InfoMenuOptions infoMenu;

    public BuildingSystem buildingScript;
    public GameObject buildingArea;

    private static float gameSpeed = 1;


    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Deselect();
        } 
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(infoMenu.IsPause()){
                infoMenu.Pause(gameSpeed);
            }
            pauseMenu.Pause(gameSpeed);
        } 
        if (Input.GetKeyDown(KeyCode.I) && !pauseMenu.IsPause()) {
            infoMenu.Pause(gameSpeed);
        } 
    }

    public void Select(GameObject selected){
        if(selected != null){
            selectedWorker = selected;
            if (!buildMenu.activeSelf && selected.tag == "Builder") {
                buildMenu.SetActive(true);
                buildingArea.SetActive(true);
            }
        }
    }

    public void Deselect(){
        if (selectedWorker != null) {
            if(buildMenu.activeSelf){
                buildingScript.Unselect();
                buildMenu.SetActive(false);
                buildingArea.SetActive(false);
            }
            var workerBuildingScript = selectedWorker.GetComponent<WorkerBuildingScript>();

            if (workerBuildingScript != null)
            {
                workerBuildingScript.Deselect();
            }
            selectedWorker = null;
        } 
    }

    public static void SetGameSpeed(float newSpeed){
        gameSpeed = newSpeed;
        Time.timeScale = newSpeed;
    }

    public static float GetGameSpeed(){
        return gameSpeed;
    }

    public void UpdateNPCData(Dictionary<string, int> npcs){
        if(selectedWorker != null && !NPCController.IsNPCAlive(selectedWorker)){
            Deselect();
        }
    }
}
