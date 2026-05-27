using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightDayScript : MonoBehaviour
{
    public Light2D directionalLight;
    public float minIntensity = 0.1f;
    public float maxIntensity = 1f;
    public float darkeningSpeed = 0.5f;
    public float interval = 2f;

    private float timer = 0f;
    private bool darkening = false;

    public Transform sunTransform;
    public Transform moonTransform;

    public BGScript bg;

    public RevealController revealController;

    private float elapsedTime = 0f;

    private List<IdleScript> idleScripts = new List<IdleScript>();


    void Start()
    {
        AudioManager.instance.PlayMusic("DayMusic1");
        directionalLight.intensity = maxIntensity;
        moonTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
        sunTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            darkening = !darkening;
            if(darkening)
                StartTheNight();
            else
                StartTheDay();
        }

        float cycleProgress = elapsedTime / interval;
        float angle = Mathf.Lerp(-90f, 90f, cycleProgress); 

        if (darkening)
        {
            directionalLight.intensity -= darkeningSpeed * Time.deltaTime;
            directionalLight.intensity = Mathf.Max(directionalLight.intensity, minIntensity);
            moonTransform.rotation = Quaternion.Euler(0f, 0f, angle); 
            if(revealController.color.r >= 0.5f)
                revealController.color = new Color(revealController.color.r - (darkeningSpeed/2f) * Time.deltaTime, revealController.color.g - (darkeningSpeed/2f)  * Time.deltaTime, revealController.color.b - (darkeningSpeed/2f)  * Time.deltaTime, 1f);
        }
        else
        {
            directionalLight.intensity += darkeningSpeed * Time.deltaTime;
            directionalLight.intensity = Mathf.Min(directionalLight.intensity, maxIntensity);
            sunTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            if(revealController.color.r < 0.9f)
                revealController.color = new Color(revealController.color.r + (darkeningSpeed/2f) * Time.deltaTime, revealController.color.g + (darkeningSpeed/2f)  * Time.deltaTime, revealController.color.b + (darkeningSpeed/2f)  * Time.deltaTime, 1f);
        }
     
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= interval)
        {
            elapsedTime = 0f;
        }

        void StartTheNight(){
            EnemySpawn.instance.Spawn(interval);
            AudioManager.instance.StopMusic("DayMusic1");
            AudioManager.instance.PlayMusic("DayMusic2");

            idleScripts.Clear();
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
            foreach (GameObject npc in npcs)
            {
                IdleScript idleScript = npc.GetComponent<IdleScript>();
                if(idleScript != null)
                {
                    idleScript.GetInside();
                    idleScripts.Add(idleScript);
                }
            }
        }

        void StartTheDay(){
            AudioManager.instance.StopMusic("DayMusic2");
            AudioManager.instance.PlayMusic("DayMusic1");

            foreach (IdleScript idleScript in idleScripts)
            {
                idleScript.GoOutside();
            }
        }
    }
}
