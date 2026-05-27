using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWallManager: MonoBehaviour
{
    public static DefenseWallManager instance = null;
    public RevealController revealController;
    private List<float> rightWalls = new List<float>();
    private List<float> leftWalls = new List<float>();
    private float farRightWall = 0.0f;

    private float farLeftWall = 0.0f;
    
    private Transform mainCamp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start() {
       mainCamp = GameObject.FindGameObjectWithTag("MainCamp").transform;
       farRightWall = mainCamp.position.x;
       farLeftWall = mainCamp.position.x;
    }
    public void AddWall(float wallPosition){
        if(mainCamp.position.x < wallPosition){
            rightWalls.Add(wallPosition);
            if(farRightWall < wallPosition)
                farRightWall = wallPosition;
        }
        else{
            leftWalls.Add(wallPosition);
            if(farLeftWall > wallPosition)
                farLeftWall = wallPosition;
        }
        if(rightWalls.Count != 0 && leftWalls.Count != 0){
            revealController.maxX = farRightWall;
            revealController.minX = farLeftWall;
        }
    }
/* 
    public void RemoveWall(float wallPosition){
        if(mainCamp.position.x < wallPosition){
            rightWalls.Remove(wallPosition);
            if(wallPosition == farRightWall){
                float newFarRight = mainCamp.position.x;
                for(int i = 0; i<= rightWalls.Count -1; i++){
                    if(rightWalls[i] > newFarRight)
                        newFarRight = rightWalls[i];
                }
                farRightWall = newFarRight;
            }
        }
        else{
           leftWalls.Remove(wallPosition);
            if(wallPosition == farLeftWall){
                float newFarLeft = mainCamp.position.x;
                for(int i = 0; i<leftWalls.Count -1; i++){
                    if(leftWalls[i] < newFarLeft)
                        newFarLeft = leftWalls[i];
                }
                farLeftWall = newFarLeft;
            }
        }
        if(rightWalls.Count != 0 && leftWalls.Count != 0){
            revealController.maxX = farRightWall;
            revealController.minX = farLeftWall;
        }
    }  */
}
