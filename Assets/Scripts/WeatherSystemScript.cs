using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeatherSystemScript : MonoBehaviour
{

    public float rainMinStartTime = 5f; 
    public float rainMaxStartTime = 30f; 
    public float rainMinEventDuration = 5f; 
    public float rainMaxEventDuration = 20f; 

    public float rainCloudsDarkenAmount = 1f;
    public float rainBGDarkenAmount = 1f;
    public ParticleSystem rain;
    
    public GameObject CloudObjects;
    public Light2D GlobalLight;
    public GameObject BGObject;
    private CloudScript Clouds;
    private BGScript BG;

    void Start()
    {
        rain.Stop();
        Clouds = CloudObjects.GetComponent<CloudScript>();
        BG = BGObject.GetComponent<BGScript>();
        StartCoroutine(StartRandomEvent());
    }

    IEnumerator StartRandomEvent()
    {
        yield return new WaitForSeconds(Random.Range(rainMinStartTime, rainMaxStartTime));
        float rand = Random.Range(rainMinEventDuration, rainMaxEventDuration);
        StartRainEvent(rand);
        yield return new WaitForSeconds(rand);
        EndRainEvent();
        StartCoroutine(StartRandomEvent());
    }

    void StartRainEvent(float rand)
    {
        var main = rain.main;
        main.duration = rand;
        rain.Play();
        Clouds.DarkenSprites(rainCloudsDarkenAmount);
        GlobalLight.color = new Color(0.8113208f,0.8113208f,0.8113208f);
        BG.DarkenSprites(rainBGDarkenAmount);
        AudioManager.instance.PlaySoundEffect("Rain");
    }

    void EndRainEvent()
    {
        rain.Stop();
        Clouds.LightenSprites(rainCloudsDarkenAmount);
        GlobalLight.color = new Color(1f,1f,1f);
        BG.LightenSprites(rainBGDarkenAmount);
        AudioManager.instance.StopSoundEffect("Rain");
    }

}
