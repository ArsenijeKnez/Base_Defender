using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour
{
    public bool clicked{get;set;}

    void Start(){
        clicked  = false;
    }
   public void TextColorChangeMethod(){
    gameObject.GetComponent<Text>().color = new Color(0.78301f,0.6528249f, 0.3287202f);
   }
    public void TextColorRevertMethod(){
        if(!clicked)
              gameObject.GetComponent<Text>().color = Color.black;

   }

}
