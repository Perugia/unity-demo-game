using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsText;
    public float updateInterval = 0.5f; // FPS güncelleme aralýðý (saniye cinsinden)

    private float accum = 0f; // Toplam zaman
    private int frames = 0; // Toplam kare sayýsý
    private float timeLeft;

    void Start()
    {
        if (!fpsText)
        {
            Debug.LogError("FPS Text UI eksik. FPS göstermek için bir Text UI öðesi atayýn.");
            enabled = false;
            return;
        }
        timeLeft = updateInterval;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Belirli bir aralýkta FPS güncelleme
        if (timeLeft <= 0.0)
        {
            float fps = accum / frames;
            fpsText.text = Mathf.Ceil(fps).ToString();

            // Sýfýrla
            accum = 0.0F;
            frames = 0;
            timeLeft = updateInterval;
        }
    }
}
