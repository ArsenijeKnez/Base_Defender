using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTextObserver : MonoBehaviour, INPCObserver {
    private Text npcText;

    public void Initialize() {
        npcText = GetComponent<Text>();
    }

    public void UpdateNPCData(Dictionary<string, int> npcs) {
        int allNpcs = 0;
        foreach(KeyValuePair<string,int> kvp in npcs){
            allNpcs += kvp.Value;
        }
        npcText.text = allNpcs.ToString();
    }
}