using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseX : MonoBehaviour
{
    public Camera mainCamera; 

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
        }
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane; 
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
    }
}