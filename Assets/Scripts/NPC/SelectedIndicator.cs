using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedIndicator : MonoBehaviour
{
    public InteractionScript interaction;
    private Rigidbody2D rb;
    public float speed = 100f;
    private GameObject indicator;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        indicator = gameObject.transform.GetChild(0).gameObject;
         if (interaction == null)
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<InteractionScript>();
    }
    void Update()
    {
        if(interaction.selectedWorker != null){
            /* if(interaction.selectedWorker.transform == gameObject.transform.parent)
                return;
            else
                gameObject.transform.parent = null; */

            float targetXPosition = interaction.selectedWorker.transform.position.x;
            if(!indicator.activeSelf){
                transform.position = new Vector3(targetXPosition + 0.03f, transform.position.y, transform.position.z);
                indicator.SetActive(true);
            }

            float distanceX = Mathf.Abs(transform.position.x - targetXPosition);
            if (distanceX < 0.01f){
                rb.velocity = Vector2.zero;
                //gameObject.transform.parent = interaction.selectedWorker.transform;
            }
            else{
                float currentSpeed = speed;
                if (distanceX < 1f)
                    currentSpeed = speed * distanceX;
                float direction = Mathf.Sign(targetXPosition - transform.position.x);
                rb.velocity = new Vector2(direction * currentSpeed, rb.velocity.y);
            }
            
        }
        else if(indicator.activeSelf /* || gameObject.transform.parent != null */){
            indicator.SetActive(false);
            rb.velocity = Vector2.zero;
            //gameObject.transform.parent = null;
        }
    }
}
