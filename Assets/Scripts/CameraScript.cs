using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
   public float moveSpeed = 5f; // The speed at which the camera moves left and right
    public float zoomSpeed = 5f; // The speed at which the camera zooms in and out
    public float minZoom = 1f; // The minimum distance of the camera from the target
    public float maxZoom = 5f; // The maximum distance of the camera from the target
    public float minCameraY = -2f; // The minimum y position of the camera
    public float maxCameraY = 5f; // The maximum y position of the camera
    public float UpDownSpeed = 3.5f; // The speed at which the camera moves up and down
    public float maxLeftPosition = -5f; // The maximum left position of the camera
    public float maxRightPosition = 5f; // The maximum right position of the camera

    public bool isMobile = true;
    public Camera PositionCamera;

    void Update()
    {
        if(isMobile){
        
        float horizontalInput = Input.GetAxis("Horizontal");
	    float newX;
        float gameSpeed = InteractionScript.GetGameSpeed();

	    if(Input.GetKey(KeyCode.LeftShift))
        	newX = transform.position.x + horizontalInput * moveSpeed * (Time.deltaTime/gameSpeed) * 2;
	    else
		    newX = transform.position.x + horizontalInput * moveSpeed * (Time.deltaTime/gameSpeed);
	
        newX = Mathf.Clamp(newX, maxLeftPosition, maxRightPosition);
 	    transform.position = new Vector3(newX, transform.position.y, transform.position.z);


        
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = Mathf.Clamp(Camera.main.fieldOfView - scrollInput * zoomSpeed, minZoom, maxZoom);
        Camera.main.fieldOfView = newZoom;
        float perspectiveZoomFactor = 1.0f / Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float perspectiveDistance = 35f / perspectiveZoomFactor;
        PositionCamera.orthographicSize = perspectiveDistance;


        
        float newY = transform.position.y - scrollInput * zoomSpeed / UpDownSpeed;
        newY = Mathf.Clamp(newY, minCameraY, maxCameraY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
