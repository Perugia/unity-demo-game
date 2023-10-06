using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject gameMng;
    public GameObject UIScreen;
    public GameObject audioMng;
    public GameObject player;
    public int bgMusic;

    void Start()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameMng).GetComponent<GameManager>();
        }

        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if (AudioManager.instance == null)
        {
            AudioManager.instance = Instantiate(audioMng).GetComponent<AudioManager>();
        }

        if (PlayerController.instance == null)
        {
            //Debug.Log("Debug: Instantiate");
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance.transform.position = transform.position;
        }

        Invoke("FadeFromBlack", 0.5f);
        
    }

    private void FadeFromBlack()
    {
        UIFade.instance.FadeFromBlack();
        AudioManager.instance.PlayBGM(bgMusic);
    }


    void Update()
    {
        
    }
}
