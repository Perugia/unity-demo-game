using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsText;
    public float updateInterval = 0.5f; // FPS g�ncelleme aral��� (saniye cinsinden)

    private float accum = 0f; // Toplam zaman
    private int frames = 0; // Toplam kare say�s�
    private float timeLeft;

    void Start()
    {
        if (!fpsText)
        {
            Debug.LogError("FPS Text UI eksik. FPS g�stermek i�in bir Text UI ��esi atay�n.");
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

        // Belirli bir aral�kta FPS g�ncelleme
        if (timeLeft <= 0.0)
        {
            float fps = accum / frames;
            fpsText.text = Mathf.Ceil(fps).ToString();

            // S�f�rla
            accum = 0.0F;
            frames = 0;
            timeLeft = updateInterval;
        }
    }
}
