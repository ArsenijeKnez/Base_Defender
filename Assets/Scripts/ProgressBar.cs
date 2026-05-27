using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
        public float targetScaleX = 2f; // The target scale on the x-axis
    public float duration = 2f; // Duration of the scaling animation

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(ScaleX());
    }

    IEnumerator ScaleX()
    {
        float timer = 0f;
        while (timer < duration)
        {
            float scale = Mathf.Lerp(initialScale.x, targetScaleX, timer / duration);
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(targetScaleX, transform.localScale.y, transform.localScale.z);
    }
}
