using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustBuilding : MonoBehaviour
{
    private Transform mainCamp;
    public bool wall = false;
    void Start()
    {
        mainCamp = GameObject.FindGameObjectWithTag("MainCamp").transform;
        if(mainCamp.position.x > transform.position.x){
            transform.Rotate(0,180,0);
        }
        if(wall){
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        }
        else{
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
