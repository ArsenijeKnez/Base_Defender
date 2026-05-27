using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenuOptions : MonoBehaviour
{
    private GameObject infoMenuDisplay;
    private GameObject Pages;

    private GameObject currentCategory;
    private GameObject currentPage;

    void Start(){
        infoMenuDisplay = gameObject.transform.GetChild(0)?.gameObject;
        if (infoMenuDisplay == null)
        {
            Debug.LogError("Menu Display is null.");
            return; 
        }

        Pages = infoMenuDisplay.transform.GetChild(1)?.gameObject;
        if (Pages == null)
        {
            Debug.LogError("Pages is null.");
            return; 
        }

        GameObject categoryGO = Pages.transform.GetChild(0)?.gameObject;
        if (categoryGO == null)
        {
            Debug.LogError("category is null.");
            return; 
        }
        categoryGO.SetActive(true);

        GameObject pageGO = categoryGO.transform.GetChild(0)?.gameObject;
        if (pageGO == null)
        {
            Debug.LogError("page is null.");
            return;
        }
        pageGO.SetActive(true);

        currentCategory = categoryGO;
        currentPage = pageGO;
    }

    public void SwitchPage(string CategoryPage){
        string[] parts = CategoryPage.Split('/');
        int.TryParse(parts[0], out int category);
        int.TryParse(parts[1], out int page);

        currentCategory.SetActive(false);
        currentPage.SetActive(false);

        GameObject categoryGO = Pages.transform.GetChild(category)?.gameObject;
        if (categoryGO == null)
        {
            Debug.LogError("category is null.");
            return; 
        }
        categoryGO.SetActive(true);

        GameObject pageGO = categoryGO.transform.GetChild(page)?.gameObject;
        if (pageGO == null)
        {
            Debug.LogError("page is null.");
            return;
        }
        pageGO.SetActive(true);
        
        currentCategory = categoryGO;
        currentPage = pageGO; 
    }


    public void Pause(float gameTime){
        if(infoMenuDisplay.activeSelf)
        {
            infoMenuDisplay.SetActive(false);
            Time.timeScale = gameTime;
        }
        else{
            infoMenuDisplay.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public bool IsPause(){
        return infoMenuDisplay.activeSelf;
    }
}
