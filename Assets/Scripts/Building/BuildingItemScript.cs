using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingItemScript : MonoBehaviour
{
    public int requiredWood = 0;
    public int requiredStone = 0;
    public int requiredWheat = 0;

    private Image renderImage;
    private Color imageColor;

    public bool itemEnabled = true;

    public bool itemSelected = false;

    void Start(){
        renderImage = GetComponent<Image>();
        imageColor = renderImage.color; 
    }
    public void DisableItem()
    {
        itemEnabled = false;
        renderImage.color = new Color(0.2f,0.2f,0.2f, imageColor.a);
    }
    public void EnableItem()
    {
        itemEnabled = true;
        renderImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, imageColor.a);
    }

    public void Highlight()
    {
        renderImage.color = new Color(imageColor.r + 7.6f, imageColor.g + 7.6f, imageColor.b + 7.6f, imageColor.a);
    }

    void Update(){
        if(requiredStone > ResourceManager.StoneCount || requiredWheat > ResourceManager.WheatCount || requiredWood > ResourceManager.WoodCount)
            DisableItem();
        else{
            EnableItem();
            if(itemSelected)
                Highlight();
        }
    }
}
