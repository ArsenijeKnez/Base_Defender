using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTextObserver : MonoBehaviour, IResourceObserver {
    private Text resourceText;
    private string resourceType;

    public void Initialize(string type) {
        resourceText = GetComponent<Text>();
        resourceType = type;
    }

    public void UpdateResourceData(int wood, int stone, int wheat) {
        switch(resourceType){
            case "Wood":
            resourceText.text = wood.ToString();
            break;
            case "Stone":
            resourceText.text = stone.ToString();
            break;
            case "Wheat":
            resourceText.text = wheat.ToString();
            break;
        }
    }
}
