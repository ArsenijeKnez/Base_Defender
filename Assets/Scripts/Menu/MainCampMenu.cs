using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCampMenu : MonoBehaviour
{
    public int campLevel;
    public MainCampMenuOptions menu;
    public InteractionScript interaction;


    void Start(){
        if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
    }

    void OnMouseDown () {
        if(interaction.selectedWorker == null){
            menu.SetCampLevel(campLevel);
        }
    }

}
